using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Linq;

namespace MVC.Controllers;

public class HomeController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(TheModel model)
    {
        ViewBag.Valid = ModelState.IsValid;
        if (ViewBag.Valid)
        {
            var charArray = model.Phrase!.Where(c => !char.IsWhiteSpace(c)).ToList();
            charArray.ForEach(c =>
            {
                if (!model.Counts!.ContainsKey(c))
                {
                    model.Counts[c] = 0;
                }
                model.Counts[c]++;
                model.Lower += c.ToString().ToLower();
                model.Upper += c.ToString().ToUpper();
            });
        }
        return View(model);
    }
}

// CHAT GPT