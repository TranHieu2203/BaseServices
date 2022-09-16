using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Core.DTO;
using Service.Interface;
namespace BaseServices.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]

    public class CustommerControler : ControllerBase
    {
        private readonly ICustommerService _service;

        public CustommerControler(ICustommerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<string> GetAll()
        {
            var s = await _service.GetCustommersAsync();
            return JsonConvert.SerializeObject(s);
        }

        [HttpPost]
        public async Task<string> CreateAsynce(CustommerDTO custommer)
        {
            try
            {
                await _service.InsertAsync(custommer);
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error";
            }
        }
        [HttpGet]
        public async Task<string> GetByID(int id)
        {
            var s = await _service.GetById(id);
            return JsonConvert.SerializeObject(s);
        }
    }
}
