using Microsoft.AspNetCore.Mvc;
using NewPdfTest.Models;
using System.Diagnostics;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
namespace NewPdfTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ExportToPdf(string url) 
        {
            //Initialize HTML to PDF converter.
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();
            //Set Blink viewport size.
            blinkConverterSettings.ViewPortSize = new Syncfusion.Drawing.Size(1280, 0);
            //Assign Blink converter settings to HTML converter.
            htmlConverter.ConverterSettings = blinkConverterSettings;
            //Convert URL to PDF document.
            PdfDocument document = htmlConverter.Convert(url);
            //Create memory stream.
            MemoryStream stream = new MemoryStream();
            //Save and close the document. 
            document.Save(stream);
            document.Close();
            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "HTML-to-PDF.pdf");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
