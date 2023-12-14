using iText.Html2pdf;
using HandlebarsDotNet;
using iText.Html2pdf.Resolver.Font;
using iText.Layout.Font;
using iText.IO.Font;

namespace PocItext.Services
{
  public class CreatePDF
  {

    public byte[] GeneratePDF<T>(string template, T obj)
    {
      using (MemoryStream output = new MemoryStream())
      {
        string htmlContent = File.ReadAllText(template);
        var handler = Handlebars.Compile(htmlContent);
        string result = handler(obj);

        string directory = "Fonts";
        var fontPaths = Directory.GetFiles(directory, "*.ttf");
        var fonts = fontPaths.Select(path => FontProgramFactory.CreateFont(path, PdfEncodings.IDENTITY_H, true)).ToList();


        ConverterProperties properties = new ConverterProperties();
        FontProvider fontProvider = new DefaultFontProvider();
        foreach (var fontPath in fontPaths)
        {
          fontProvider.AddFont(fontPath);
        }
        properties.SetFontProvider(fontProvider);
        HtmlConverter.ConvertToPdf(result, output, properties);
        
        return output.ToArray();
      }
    }
  }
}