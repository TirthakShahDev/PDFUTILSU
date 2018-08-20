using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFooterAdd
{
    public class PdfEvents : PdfPageEventHelper
    {
        protected ElementList header;
        protected ElementList footer;
        public PdfEvents()
        {
            header = XMLWorkerHelper.ParseToElementList(HEADER, null);
            footer = XMLWorkerHelper.ParseToElementList(FOOTER, null);
        }
        

        public static String HEADER =
    "<table width=\"100%\" border=\"0\"><tr><td>Header</td><td align=\"right\">Some title</td></tr></table>";
        public static String FOOTER =
            "<table width=\"100%\" border=\"0\"><tr><td>Footer</td><td align=\"right\">Some title</td></tr></table>";


        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            ColumnText ct = new ColumnText(writer.DirectContent);
            ct.SetSimpleColumn(new Rectangle(36, 832, 559, 810));
            foreach (IElement e in header)
            {
                ct.AddElement(e);
            }
            ct.Go();
            ct.SetSimpleColumn(new Rectangle(36, 10, 559, 32));
            foreach (IElement e in footer)
            {
                ct.AddElement(e);
            }
            ct.Go();
            base.OnEndPage(writer, document);
        }
    }
}
