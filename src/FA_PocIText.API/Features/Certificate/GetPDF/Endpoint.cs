using System.Reflection;
using FastEndpoints;
using PocItext.Services;

namespace FA_PocIText.Features.Certificate.GetPDF;

sealed class Endpoint : Endpoint<Request>
{
    public override void Configure()
    {
        Get("/certificate/PDF");
        AllowAnonymous();
    }

    public override Task HandleAsync(Request req, CancellationToken ct)
    {
        string templatesDirectory = "Templates";
        string fileName = "template.html";
        string templatePath = Path.Combine(templatesDirectory, fileName);

        if (!File.Exists(templatePath))
        {
            AddError("Certificate doesn't exist, please search a backend member to add the template");
        }
        ThrowIfAnyErrors();

        CreatePDF pdf = new CreatePDF();
        var pdfBytes = pdf.GeneratePDF(templatePath, new InformationCertification
        {
            certificadeCode = req.certificateCode,
            points = req.certificatePoints,
            price = req.certificatePrice
        });

        return SendBytesAsync(pdfBytes, "certificate.pdf", "application/pdf");
    }
}