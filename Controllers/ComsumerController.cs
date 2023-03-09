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

        public int GetQuantityOfOrangeJuices(string name, int quantity)
        {
            var resmoteServiceUrl = GetResmoteServiceUrlWithNameAndQuantity(name, quantity);

            using (var client = new HttpClient())
            {
                client.SendAsync(new HttpRequestMessage(HttpMethod.Get, resmoteServiceUrl));
            }


        }

        private string GetResmoteServiceUrlWithNameAndQuantity(string name, int quantity)
        {
            return string.Format(_configuration.GetValue<string>("RemoteServiceUrl"), name, quantity);
        }
    }
}
