using System.Text;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger middlewares
app.UseSwagger();
app.UseSwaggerUI();

// ===== Helpers =====
static string[] SplitWords(string text) =>
    text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

static string JoinWords(IEnumerable<string> words) =>
    string.Join(' ', words);

static IResult JsonOk(string ori, string @new) =>
    Results.Json(new { ori, @new }, statusCode: 200);

static IResult Bad(string message) =>
    Results.Json(new { error = message }, statusCode: 400);

// Header "xml": true/false o 1/0 (opcional)
static bool WantXml(string? headerValue)
{
    if (string.IsNullOrWhiteSpace(headerValue)) return false;
    var v = headerValue.Trim();
    return v.Equals("true", StringComparison.OrdinalIgnoreCase) || v == "1";
}

static IResult XmlOk(string ori, string @new)
{
    // Serializa en XML (UTF-16 como en el ejemplo)
    var result = new XmlResult { Ori = ori, New = @new };
    var ns = new XmlSerializerNamespaces();
    ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
    ns.Add("xsd", "http://www.w3.org/2001/XMLSchema");

    var serializer = new XmlSerializer(typeof(XmlResult));
    using var ms = new MemoryStream();
    using (var writer = new StreamWriter(ms, Encoding.Unicode, leaveOpen: true))
    {
        serializer.Serialize(writer, result, ns);
    }
    ms.Position = 0;
    return Results.File(ms.ToArray(), "application/xml; charset=utf-16");
}

// ==================== Endpoints ====================

// GET / → Redirect a Swagger UI
app.MapGet("/", () => Results.Redirect("/swagger"))
   .WithName("Root");

// POST /include/{position}?value=...
// text en Form (IncludeForm); xml en Header (mapeado)
app.MapPost("/include/{position:int}",
(
    [FromRoute] int position,
    [FromQuery] string? value,
    [FromHeader(Name = "xml")] string? xml,
    [FromForm] IncludeForm form
) =>
{
    if (position < 0) return Bad("'position' must be 0 or higher");
    if (string.IsNullOrWhiteSpace(form.text)) return Bad("'text' cannot be empty");
    if (string.IsNullOrEmpty(value)) return Bad("'value' cannot be empty");

    var words = SplitWords(form.text).ToList();
    if (position >= words.Count) words.Add(value);
    else words.Insert(position, value);

    var newText = JoinWords(words);
    return WantXml(xml) ? XmlOk(form.text, newText) : JsonOk(form.text, newText);
})
.WithName("Include")
.DisableAntiforgery();   // <—— evita el 500 por antiforgery

// PUT /replace/{length}?value=...
// text en Form (ReplaceForm); xml en Header
app.MapPut("/replace/{length:int}",
(
    [FromRoute] int length,
    [FromQuery] string? value,
    [FromHeader(Name = "xml")] string? xml,
    [FromForm] ReplaceForm form
) =>
{
    if (length <= 0) return Bad("'length' must be higher than 0");
    if (string.IsNullOrWhiteSpace(form.text)) return Bad("'text' cannot be empty");
    if (string.IsNullOrEmpty(value)) return Bad("'value' cannot be empty");

    var replaced = SplitWords(form.text).Select(w => w.Length == length ? value : w);
    var newText = JoinWords(replaced);
    return WantXml(xml) ? XmlOk(form.text, newText) : JsonOk(form.text, newText);
})
.WithName("Replace")
.DisableAntiforgery();   // <—— evita el 500 por antiforgery

// DELETE /erase/{length}
// text en Form (EraseForm); xml en Header
app.MapDelete("/erase/{length:int}",
(
    [FromRoute] int length,
    [FromHeader(Name = "xml")] string? xml,
    [FromForm] EraseForm form
) =>
{
    if (length <= 0) return Bad("'length' must be higher than 0");
    if (string.IsNullOrWhiteSpace(form.text)) return Bad("'text' cannot be empty");

    var filtered = SplitWords(form.text).Where(w => w.Length != length);
    var newText = JoinWords(filtered);
    return WantXml(xml) ? XmlOk(form.text, newText) : JsonOk(form.text, newText);
})
.WithName("Erase")
.DisableAntiforgery();   // <—— evita el 500 por antiforgery

app.Run();


// ================ tipos DESPUÉS de app.Run() ================
public record IncludeForm([FromForm] string text);
public record ReplaceForm([FromForm] string text);
public record EraseForm([FromForm] string text);

[XmlRoot("Result")]
public class XmlResult
{
    public string Ori { get; set; } = "";
    public string New { get; set; } = "";
}


