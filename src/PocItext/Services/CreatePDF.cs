using System;
using System.IO;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using HandlebarsDotNet;

namespace PocItext.Services
{
  public class CreatePDF
  {

    public byte[] GeneratePDF<T>(string template, T properties)
    {
      using (MemoryStream output = new MemoryStream())
      {
        string htmlContent = File.ReadAllText(template);
        var handler = Handlebars.Compile(htmlContent);
        string result = handler(properties);
        HtmlConverter.ConvertToPdf(result, output);
       return output.ToArray();
      }
    }
  }
}