using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFooterAdd
{
    public class PDFUtil
    {
        #region Members

        private PdfReader _reader;
        private MemoryStream _memStream;
        private PdfStamper _pdfStamper;

        #endregion

        #region Constructor / Init

        public PDFUtil(byte[] pdfData)
        {
            Init(pdfData);
        }

        public PDFUtil(string pdfFilePath)
        {
            Init(pdfFilePath);
        }

        public void AddFooter(string FooterText)
        {
            Phrase Footer = new Phrase(FooterText, new Font(Font.FontFamily.HELVETICA, 14));
            for (int i = 1; i <= _reader.NumberOfPages; i++)
            {
                float x = _reader.GetPageSize(i).Width / 2;
                float y = _reader.GetPageSize(i).GetTop(20);
                ColumnText.ShowTextAligned(_pdfStamper.GetOverContent(i), Element.ALIGN_CENTER, Footer, x, y, 0);
            }
            ////originalWriter.PageEvent = new PdfEvents();
        }

        private void Init(byte[] pdfData)
        {
            this._reader = GetPDFReader(pdfData);
            this._memStream = new MemoryStream();
            this._pdfStamper = new PdfStamper(this._reader, this._memStream);
        }

        private void Init(string pdfFilePath)
        {
            this._reader = GetPDFReader(pdfFilePath);
            this._memStream = new MemoryStream();
            this._pdfStamper = new PdfStamper(this._reader, this._memStream);
        }

        #endregion
    }
}
