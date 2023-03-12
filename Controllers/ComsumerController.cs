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

            //https:/localhost:7113/WebApiLecture?name={0}&quantity={1}
            var resmoteServiceUrl = GetResmoteServiceUrlWithNameAndQuantity(name, quantity);

            HttpResponseMessage response = null;

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, resmoteServiceUrl);

                response = await client.SendAsync(request);

                //var getResponse = await client.GetAsync(resmoteServiceUrl);
            }

            var content = await response.Content.ReadAsStringAsync();

            return new JsonResult(content);
        }

        private string GetResmoteServiceUrlWithNameAndQuantity(string name, int quantity)
        {
            return string.Format(_configuration.GetValue<string>("RemoteServiceUrl"), name, quantity);
        }
    }
}
