namespace Taxually.TechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VatRegistrationController : ControllerBase
    {
        TaxuallyVatRegisrationService vatRegistraionService;

        public VatRegistrationController()
        {
            vatRegistraionService = new TaxuallyVatRegisrationService();
        }

        /// <summary>
        /// Registers a company for a VAT number in a given country
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
        {
            if(await vatRegistraionService.RegisterRequest(request))
            {
                return Ok("Registered Succesfully!");
            }
            else
            {
                return BadRequest("Error during registraion!");
            }
        }
    }
}
