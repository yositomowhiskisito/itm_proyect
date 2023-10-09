using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using iText.Layout;
using System.IO;
using LIBDomainEntities.Entities;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.ComTypes;
using iText.Kernel.Pdf.Xobject;
using iText.Layout.Borders;

namespace LIBPresentationContext.Implementations.Helpers.PdfHelper
{
    public class HelperPdf
    {
        public MemoryStream CreatePdf(List<object> list, string backingFile)
        {

            var stream = new MemoryStream();
            // Must have write permissions to the path folder
            PdfWriter writer = new PdfWriter(stream);

            writer.SetCloseStream(false);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Header
            Paragraph header = new Paragraph("PERSONS")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(20);

            // New line
            Paragraph newline = new Paragraph(new Text("\n"));

            document.Add(newline);
            document.Add(header);

            // Add sub-header
            Paragraph subheader = new Paragraph("LIST PERSONS")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
               .SetFontSize(15);
            document.Add(subheader);

            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            // Add paragraph1
            Paragraph paragraph1 = new Paragraph("Welcome To Xamarin.");
            document.Add(paragraph1);

            // Add image
            /*Image img = new Image(ImageDataFactory
               .Create(@"..\..\image.jpg"))
               .SetTextAlignment(TextAlignment.CENTER);
            document.Add(img);*/

            // Table
            Table table = new Table(3, false);
            iText.Layout.Element.Cell cell11 = new iText.Layout.Element.Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("Country"));
            iText.Layout.Element.Cell cell12 = new iText.Layout.Element.Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("State"));
            iText.Layout.Element.Cell cell13 = new iText.Layout.Element.Cell(1, 1)
               .SetBackgroundColor(ColorConstants.GRAY)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("City"));

            iText.Layout.Element.Cell cell21 = new iText.Layout.Element.Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("Brasil"));
            iText.Layout.Element.Cell cell22 = new iText.Layout.Element.Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("?"));
            iText.Layout.Element.Cell cell23 = new iText.Layout.Element.Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("Brasilia"));

            iText.Layout.Element.Cell cell31 = new iText.Layout.Element.Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("Estados Unidos"));
            iText.Layout.Element.Cell cell32 = new iText.Layout.Element.Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("?"));
            iText.Layout.Element.Cell cell33 = new iText.Layout.Element.Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("Washintong"));

            iText.Layout.Element.Cell cell41 = new iText.Layout.Element.Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("España"));
            iText.Layout.Element.Cell cell42 = new iText.Layout.Element.Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("?"));
            iText.Layout.Element.Cell cell43 = new iText.Layout.Element.Cell(1, 1)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("Madrid"));

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell13);
            table.AddCell(cell21);
            table.AddCell(cell22);
            table.AddCell(cell23);
            table.AddCell(cell31);
            table.AddCell(cell32);
            table.AddCell(cell33);
            table.AddCell(cell41);
            table.AddCell(cell42);
            table.AddCell(cell43);

            document.Add(newline);
            document.Add(table);

            newline = new Paragraph(new Text("\n"));
            ls = new LineSeparator(new SolidLine());
            document.Add(ls);
            // Hyper link
            Link link = new Link("click here",
               PdfAction.CreateURI("https://www.google.com"));
            Paragraph hyperLink = new Paragraph("Please ")
               .Add(link.SetBold().SetUnderline()
               .SetItalic().SetFontColor(ColorConstants.BLUE))
               .Add(" to go www.google.com.");

            document.Add(newline);
            document.Add(hyperLink);

            // Page numbers
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format("page" + i + " of " + n)),
                   559, 806, i, iText.Layout.Properties.TextAlignment.RIGHT,
                   VerticalAlignment.TOP, 0);
            }

            // Close document
            document.Close();
            return stream;
        }


        public MemoryStream CreatePdf2(ObservableCollection<Persons> list, string backingFile)
        {

            var stream = new MemoryStream();
            // Must have write permissions to the path folder
            PdfWriter writer = new PdfWriter(stream);

            writer.SetCloseStream(false);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            // Header
            Paragraph header = new Paragraph("PERSONS")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .SetFontSize(20);

            // New line
            Paragraph newline = new Paragraph(new Text("\n"));

            document.Add(newline);
            document.Add(header);

            // Add sub-header
            Paragraph subheader = new Paragraph("LIST PERSONS")
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
               .SetFontSize(15);
            document.Add(subheader);

            // Line separator
            LineSeparator ls = new LineSeparator(new SolidLine());
            document.Add(ls);

            // Add paragraph1
            Paragraph paragraph1 = new Paragraph("Welcome To Xamarin Persons.");
            document.Add(paragraph1);

            // Add image
            /*Image img = new Image(ImageDataFactory
               .Create(@"..\..\image.jpg"))
               .SetTextAlignment(TextAlignment.CENTER);
            document.Add(img);*/

            // Table
            Table table = new Table(6, false);
            iText.Layout.Element.Cell cell11 = new iText.Layout.Element.Cell(1, 1)
               .SetBackgroundColor(ColorConstants.WHITE)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("Id"));
            iText.Layout.Element.Cell cell12 = new iText.Layout.Element.Cell(1, 1)
               .SetBackgroundColor(ColorConstants.WHITE)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("SSN"));
            iText.Layout.Element.Cell cell13 = new iText.Layout.Element.Cell(1, 1)
               .SetBackgroundColor(ColorConstants.WHITE)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("Name"));
            iText.Layout.Element.Cell cell14 = new iText.Layout.Element.Cell(1, 1)
               .SetBackgroundColor(ColorConstants.WHITE)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("Born"));
            iText.Layout.Element.Cell cell15 = new iText.Layout.Element.Cell(1, 1)
               .SetBackgroundColor(ColorConstants.WHITE)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("State"));
            iText.Layout.Element.Cell cell16 = new iText.Layout.Element.Cell(1, 1)
               .SetBackgroundColor(ColorConstants.WHITE)
               .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
               .Add(new Paragraph("File"));

            table.AddCell(cell11);
            table.AddCell(cell12);
            table.AddCell(cell13);
            table.AddCell(cell14);
            table.AddCell(cell15);
            table.AddCell(cell16);

            foreach (var item in list)
            {
                ImageData rawImage = ImageDataFactory.Create(item.File);
                Image image = new Image(rawImage);
                iText.Layout.Element.Cell cell21 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                   .Add(new Paragraph(item.Id.ToString()));
                iText.Layout.Element.Cell cell22 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                   .Add(new Paragraph(item.SSN.ToString()));
                iText.Layout.Element.Cell cell23 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                   .Add(new Paragraph(item.Name));
                iText.Layout.Element.Cell cell24 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                   .Add(new Paragraph(item.Born.ToString()));
                iText.Layout.Element.Cell cell25 = new iText.Layout.Element.Cell(1, 1)
                   .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                   .Add(new Paragraph(item.State.ToString()));
                Cell cell = new Cell().Add(image.SetAutoScale(true));
                cell.SetHeight(20); cell.SetWidth(20);

                //table.AddCell(cell21.SetBorder(Border.NO_BORDER));
                table.AddCell(cell21);
                table.AddCell(cell22);
                table.AddCell(cell23);
                table.AddCell(cell24);
                table.AddCell(cell25);
                table.AddCell(cell);
            }

            document.Add(newline);
            document.Add(table);

            // Hyper link
            Link link = new Link("click here",
               PdfAction.CreateURI("https://www.itm.edu.co/"));
            Paragraph hyperLink = new Paragraph("For more information visit the page ")
               .Add(link.SetBold().SetUnderline()
               .SetItalic().SetFontColor(ColorConstants.BLUE))
               .Add(" to go www.itm.edu.co");

            document.Add(newline);
            document.Add(hyperLink);

            // Page numbers
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format("page" + i + " of " + n)),
                   559, 806, i, iText.Layout.Properties.TextAlignment.RIGHT,
                   VerticalAlignment.TOP, 0);
            }

            // Close document
            document.Close();
            return stream;
        }
    }
}