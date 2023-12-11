using System.Data;
using FastEndpoints;
using FluentValidation;
using System.Globalization;

namespace FA_PocIText.Features.Certificate.GetPDF;

sealed class Request
{
    public string certificateCode { get; set; } = string.Empty;
    public decimal certificatePrice { get; set; }
    public string certificatePoints { get; set; } = string.Empty;
}

sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.certificateCode).NotEmpty().WithMessage("Certificate code is required").MaximumLength(17).WithMessage("Max length is 17");
        RuleFor(x => x.certificatePrice).NotEmpty().WithMessage("Certificate price is required");
        RuleFor(x => x.certificatePoints).NotEmpty().WithMessage("Certificate points are required");
    }
}

sealed class Response
{
    public string Message { get; set; } = string.Empty;
}


sealed class InformationCertification
{
    private DateTime _expirationDate = DateTime.Today;
    public decimal price { get; set; }
    public string certificadeCode { get; set; } = string.Empty;
    public string points { get; set; } = string.Empty;
    private DateTime date { get { return _expirationDate; } set { _expirationDate = _expirationDate.AddDays(7); } }

    public string expirationDate
    {
        get
        {
            return $" {date.Day} de {date.ToString("MMMM", new CultureInfo("es-ES"))} del {date.Year}";
        }
    }
}