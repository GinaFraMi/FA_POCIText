using iText.Html2pdf;
using HandlebarsDotNet;
using iText.Layout.Font;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Font;

namespace PocItext.Services;

public interface ICreatePDF {
  public byte[] GeneratePDF<T>(string htmlContent, T obj);
  public byte[] GeneratePDF(string htmlContent);
}
