using iTextSharp.text;
using iTextSharp.text.pdf;
using Orders.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application
{
    public class InvoiceGenerator : IInvoiceGenerator
    {
        public async Task<byte[]> GenerateInvoice(Payment payment)
        {
            try
            {
                // Create a new PDF document
                Document document = new Document();

                // Create a new MemoryStream to store the PDF data
                MemoryStream memoryStream = new MemoryStream();

                // Create a new PdfWriter to write the PDF document to the MemoryStream
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                // Open the PDF document
                document.Open();

                // Add content to the PDF document
                // ...

                // Add a blank page to the PDF document
                document.NewPage();

                // Add the invoice content to the PDF document
                Paragraph invoiceContent = new Paragraph("This is the invoice content");
                document.Add(invoiceContent);

                // Close the PDF document
                document.Close();

                // Get the PDF data from the MemoryStream
                byte[] pdfData = memoryStream.ToArray();

                // Return the PDF data
                return pdfData;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
