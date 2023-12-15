using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using System.Windows.Media;
using FinalProjectResumeMaker;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Drawing.Layout;


namespace FinalProjectWPFResumeBuilder
{
    public static class ExportToPDF
    {

        public static void exportToPDF(List<Category> category)
        {
            // Create a PDF document
            PdfDocument document = new PdfDocument();

            // Set document info
            document.Info.Title = "People PDF Document";

            // Create an empty page and set its size
            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.Letter;

            // Get XGraphics
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create fonts
            XFont fontTitle = new XFont("Verdana", 22, XFontStyle.BoldItalic);
            XFont fontBody = new XFont("Times New Roman", 12, XFontStyle.Regular);

            // Create XTextFormatter
            XTextFormatter tf = new XTextFormatter(gfx);

            // Color page
            XRect rect = new XRect(0, 0, page.Width, page.Height);
            gfx.DrawRectangle(XBrushes.BlanchedAlmond, rect);

            // Title
            rect = new XRect(0, 10, page.Width - 20, 55);
            tf.Alignment = XParagraphAlignment.Center;
            string Title = "These are my peeps";
            tf.DrawString(Title, fontTitle, XBrushes.Red, rect);

            // Add people
            string text = "";

            foreach (Category c in category)
            {
                text += String.Format("\n{0} {1} {2} {3}", c.CategoryName, c.CategoryDescription, c.Location, c.YOA);
            }

            rect = new XRect(10, 220, page.Width - 20, 220);
            tf.Alignment = XParagraphAlignment.Left;
            tf.DrawString(text, fontBody, XBrushes.Black, rect);

            // Save the document
            const string filename = "People.pdf";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files(*.pdf)|*.pdf|All files(*.*)|(*.*)";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Title = filename;
            saveFileDialog.OverwritePrompt = true;

            if (saveFileDialog.ShowDialog() == true)
            {
                document.Save(saveFileDialog.FileName);
            }
        }
    }
}
