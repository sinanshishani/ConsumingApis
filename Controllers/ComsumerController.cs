using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumingApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComsumerController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ComsumerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<JsonResult> GetQuantityOfOrangeJuices(string name, int quantity)
        {
            var resmoteServiceUrl = GetResmoteServiceUrlWithNameAndQuantity(name, quantity);

            HttpResponseMessage response = null;

            using (var client = new HttpClient())
            {
                response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, resmoteServiceUrl));

                var getResponse = await client.GetAsync(resmoteServiceUrl);
            }

            return new JsonResult(await response.Content.ReadAsStringAsync());
        }

        private string GetResmoteServiceUrlWithNameAndQuantity(string name, int quantity)
        {
            return string.Format(_configuration.GetValue<string>("RemoteServiceUrl"), name, quantity);
        }
    }
}
