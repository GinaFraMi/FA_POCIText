using iText.Html2pdf;
using HandlebarsDotNet;
using iText.Layout.Font;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Font;

namespace PocItext.Services
{
  public class CreatePDF
   {

    private FontProvider CreateFonts()
    {

      string directory = "Fonts";
      var executionPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
      var pathString = Path.Combine(executionPath!, directory);
      var fontPaths = Directory.GetFiles(pathString, "*.ttf");
      var fonts = fontPaths.Select(path => FontProgramFactory.CreateFont(path, PdfEncodings.IDENTITY_H, true)).ToList();
      FontProvider fontProvider = new DefaultFontProvider();
      foreach (var fontPath in fontPaths)
      {
        fontProvider.AddFont(fontPath);
      }
      return fontProvider;
    }

    private byte[] PDF (string content)
    {
      using (MemoryStream output = new MemoryStream())
      {
      ConverterProperties properties = new ConverterProperties();
      properties.SetFontProvider(CreateFonts());
      HtmlConverter.ConvertToPdf(content, output, properties);
      return output.ToArray();
      }
    }

    public byte[] GeneratePDF<T>(string htmlContent, T obj)
    {

        var handler = Handlebars.Compile(htmlContent);
        string result = handler(obj);
        return PDF(result);
    }

    public byte[] GeneratePDF(string htmlContent)
    {
      return PDF(htmlContent);
    }
  }
}