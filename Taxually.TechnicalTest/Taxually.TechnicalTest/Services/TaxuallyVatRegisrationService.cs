using System.Text;
using System.Xml.Serialization;

namespace Taxually.TechnicalTest.Services
{
    public class TaxuallyVatRegisrationService
    {

        public async Task<bool> RegisterRequest(VatRegistrationRequest request)
        {
            switch (request.Country)
            {
                case "GB":
                    // UK has an API to register for a VAT number
                    await ApiRegister(request);
                    break;
                case "FR":
                    // France requires an excel spreadsheet to be uploaded to register for a VAT number
                    await CsVRegister(request);
                    break;
                case "DE":
                    // Germany requires an XML document to be uploaded to register for a VAT number
                    await XmlRegister(request);
                    break;
                default:
                    return false;
            }
            return true;
        }

        public async Task<bool> ApiRegister(VatRegistrationRequest request)
        {
            var httpClient = new TaxuallyHttpClient();
            await httpClient.PostAsync("https://api.uktax.gov.uk", request);
            return true;
        }

        public async Task<bool> CsVRegister(VatRegistrationRequest request)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            var excelQueueClient = new TaxuallyQueueClient();
            // Queue file to be processed
            await excelQueueClient.EnqueueAsync("vat-registration-csv", csv);
            return true;
        }

        public async Task<bool> XmlRegister(VatRegistrationRequest request)
        {
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
                serializer.Serialize(stringwriter, this);
                var xml = stringwriter.ToString();
                var xmlQueueClient = new TaxuallyQueueClient();
                // Queue xml doc to be processed
                await xmlQueueClient.EnqueueAsync("vat-registration-xml", xml);
                return true;
            }
        }
    }
}
