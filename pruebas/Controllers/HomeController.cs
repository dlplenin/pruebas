using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using pruebas.Models;
using Wkhtmltopdf.NetCore;
using PdfSharp.Pdf.IO;
using System.Drawing.Text;
using System.Drawing;
using PdfSharp.Fonts;

namespace pruebas.Controllers
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
            GeneratePDF2();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void GeneratePDF()
        {
            var customFamilies = new PrivateFontCollection();

            customFamilies.AddFontFile(".\\Drift.ttf");
            var family = customFamilies.Families[0];

            var myfont = new Font(family, 40, FontStyle.Regular, GraphicsUnit.Point);

            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            //XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);
            XFont font = new XFont(myfont.FontFamily, 20, XFontStyle.BoldItalic);

            // Draw the text
            // Draw the text
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
              new XRect(0, 0, page.Width, page.Height),
              XStringFormats.Center);

            const string filename = "HelloWorld.pdf";
            document.Save(filename);
        }
        
        public void GeneratePDF2()
        {
            // That's all it takes to register your own fontresolver
            GlobalFontSettings.FontResolver = new DemoFontResolver();

            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Drift", 20, XFontStyle.BoldItalic);

            // Draw the text
            gfx.DrawString("Hello, World!", font, XBrushes.Black,
              new XRect(0, 0, page.Width, page.Height),
              XStringFormats.Center);

            const string filename = "HelloWorld.pdf";
            document.Save(filename);
        }
    }
}
