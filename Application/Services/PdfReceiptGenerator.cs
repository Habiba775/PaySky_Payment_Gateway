using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using Domain.Entities.Custom_Entities;

namespace Application.Services
{
    public class PdfReceiptGenerator
    {
        private readonly string _fontPath;

        public PdfReceiptGenerator()
        {
            _fontPath = Path.Combine(AppContext.BaseDirectory, "Fonts", "arial.ttf");
           // _logoPath = Path.Combine(AppContext.BaseDirectory, "Images", "\"C:\\Users\\dell\\Pictures\\Screenshots\\Screenshot 2025-08-10 005053.png\"");

            if (!File.Exists(_fontPath))
                throw new FileNotFoundException($"Font file not found at path: {_fontPath}");

            GlobalFontSettings.FontResolver = new CustomFontResolver(_fontPath);
        }

        public byte[] Generate(Transaction transaction)
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;

            var gfx = XGraphics.FromPdfPage(page);
            double margin = 50;

            // Colors and pens
            var borderPen = new XPen(XColors.DarkSlateBlue, 3);
            var headerBrush = new XSolidBrush(XColors.LightSteelBlue);
            var textBrush = XBrushes.Black;

            // Draw page border
            gfx.DrawRectangle(borderPen, margin / 2, margin / 2, page.Width - margin, page.Height - margin);

            // Header background
            gfx.DrawRectangle(headerBrush, margin / 2, margin / 2, page.Width - margin, 60);

            // Fonts
            var titleFont = new XFont("Arial", 24);
            var headerFont = new XFont("Arial", 12);
            var bodyFont = new XFont("Arial", 14);
            var labelFont = new XFont("Arial", 14);

            // Title text
            gfx.DrawString("Transaction Receipt", titleFont, XBrushes.DarkSlateBlue,
                new XRect(margin / 2 + 10, margin / 2, page.Width - margin, 60),
                XStringFormats.CenterLeft);

            // Draw transaction details with labels and values
            double startY = margin / 2 + 80;
            double lineHeight = 28;
            double labelX = margin;
            double valueX = margin + 150;

            void DrawLine(string label, string value, double y)
            {
                gfx.DrawString(label, labelFont, textBrush, new XPoint(labelX, y));
                gfx.DrawString(value, bodyFont, textBrush, new XPoint(valueX, y));
            }

            DrawLine("Receipt for Transaction #:", transaction.Id.ToString(), startY);
            DrawLine("Transaction Type:", transaction.transactiontype.ToString(), startY + lineHeight);

            DrawLine("Amount:", transaction.Amount.ToString("C2"), startY + 2 * lineHeight);
            DrawLine("Date:", transaction.Timestamp.ToString("yyyy-MM-dd HH:mm"), startY + 3 * lineHeight);

            // Footer
            string footer = "Thank you for using Our Payment Gateway.";
            gfx.DrawString(footer, headerFont, XBrushes.Gray,
                new XRect(margin / 2, page.Height - margin, page.Width - margin, 40),
                XStringFormats.Center);

            using var ms = new MemoryStream();
            document.Save(ms, false);
            return ms.ToArray();
        }

        // Custom font resolver for Arial.ttf
        private class CustomFontResolver : IFontResolver
        {
            private readonly byte[] _fontData;

            public CustomFontResolver(string fontFilePath)
            {
                _fontData = File.ReadAllBytes(fontFilePath);
            }

            public byte[] GetFont(string faceName)
            {
                if (faceName == "Arial#Regular")
                    return _fontData;
                return null;
            }

            public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
            {
                if (familyName.Equals("Arial", StringComparison.OrdinalIgnoreCase))
                    return new FontResolverInfo("Arial#Regular");
                return null;
            }
        }
    }
}

