﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Service.DTO;
using Service.Service;
namespace BaseServices.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]

    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _service;

        public ProjectController(ProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<string> GetAll()
        {
            var s = await  _service.GetProjectAsync();
            return JsonConvert.SerializeObject(s);
        }

        [HttpPost]
        public async Task<string> CreateAsync(ProjectDTO project)
        {
            try
            {
                await _service.InsertAsync(project);
                await _service.CompletedAsync();

                return "OK";
            }
            catch( Exception ex)
            {
                return "DDEOS";
            }
        }
        [HttpGet]
        public async Task<string> GetById(int id)
        {
            var s = await _service.GetById(id);
            return JsonConvert.SerializeObject(s);
        }
    }
}