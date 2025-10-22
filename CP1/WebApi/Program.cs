using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Antiforgery;
using System.Xml.Serialization;
using System.Text;
using System.Linq;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var list = new List<object>();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.MapPost("/", ([FromHeader(Name = "xml")] bool? xml) =>
{
    var xmlEnabled = xml ?? false;

    if (xmlEnabled)
    {
        var asStrings = list.Select(x => x?.ToString() ?? string.Empty).ToList();
        var serializer = new XmlSerializer(typeof(List<string>));
        using var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, asStrings);
        var xmlResult = stringWriter.ToString();
        return Results.Content(xmlResult, "application/xml", Encoding.UTF8);
    }

    return Results.Ok(list);
});

app.MapPut("/", ([FromForm] int quantity, [FromForm] NumberType type) =>
{
    if (quantity <= 0)
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });

    var random = new Random();

    if (type == NumberType.Int)
    {
        for (var i = 0; i < quantity; i++)
            list.Add(random.Next());
    }
    else
    {
        for (var i = 0; i < quantity; i++)
            list.Add(random.NextSingle());
    }

    return Results.Ok();
}).DisableAntiforgery();

app.MapDelete("/", ([FromForm] int quantity) =>
{
    if (quantity <= 0)
        return Results.BadRequest(new { error = "'quantity' must be higher than zero" });

    if (quantity > list.Count)
        return Results.BadRequest(new { error = "'quantity' is greater than the list size" });

    for (var i = 0; i < quantity; i++)
        list.RemoveAt(0);

    return Results.Ok();
}).DisableAntiforgery();

app.MapPatch("/", () =>
{
    list.Clear();
    return Results.Ok();
});

app.Run();

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum NumberType
{
    Int,
    Float
}

// Chatgpt



