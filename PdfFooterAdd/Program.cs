namespace sandbox.stamper
{
    using DocumentException = iTextSharp.text.DocumentException;
    using Element = iTextSharp.text.Element;
    using Font = iTextSharp.text.Font;
    using FontFamily = iTextSharp.text.Font.FontFamily;
    using Phrase = iTextSharp.text.Phrase;
    using ColumnText = iTextSharp.text.pdf.ColumnText;
    using PdfReader = iTextSharp.text.pdf.PdfReader;
    using PdfStamper = iTextSharp.text.pdf.PdfStamper;
    using System.IO;
    using PdfFooterAdd;
    using System.Collections.Generic;


    /// 
    /// <summary>
    /// @author iText
    /// </summary>
    public class StampHeader2
    {

        public const string SRC = "resources/job_application.pdf";
        public const string DEST = "results/job_application.pdf";
        public static void Main(string[] args)
        {
            PDFUtil pdf = new PDFUtil(File.ReadAllBytes(SRC));
            pdf.AddFooter("Hey This Is me Footer!!!!!");
            pdf.ExtractPDFPage(0, 2);
            var bytes = pdf.GetPDFDataAndClose(true);
            File.WriteAllBytes(DEST, bytes);
        }
        public virtual void manipulatePdf(string src, string dest)
        {
            PdfReader reader = new PdfReader(src);
            PdfStamper stamper = new PdfStamper(reader, new System.IO.FileStream(dest, System.IO.FileMode.Create, System.IO.FileAccess.Write));
            stamper.RotateContents = false;
            Phrase header = new Phrase("Copy", new Font(Font.FontFamily.HELVETICA, 14));
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                float x = reader.GetPageSize(i).Width / 2;
                float y = reader.GetPageSize(i).GetTop(20);
                ColumnText.ShowTextAligned(stamper.GetOverContent(i), Element.ALIGN_CENTER, header, x, y, 0);
            }
            stamper.Close();
            reader.Close();
        }
    }
}
