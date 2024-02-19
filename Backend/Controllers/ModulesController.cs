using System.Collections.Generic;
using BachelorOppgave.Controllers.ModulesModels;
using BachelorOppgave.Data;
using BachelorOppgave.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BachelorOppgave.Controllers
{
    [Route("api/[controller]")] //api/modules
    [ApiController]
    [Authorize]
    [EnableCors("CORS")]
    public class ModulesController : Controller
    {
        private readonly ModuleRepository _moduleRepository;

        public ModulesController(ModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        [HttpGet] //api/modules
        //to get a list of modules
        [AllowAnonymous]
        public IEnumerable<Module> GetModules()
        {
            return _moduleRepository.FetchModules();
        }

        //to get one specific module
        [HttpGet("{moduleID}")]
        [AllowAnonymous]
        public IActionResult FetchModuleByID(int moduleID)
        {
            var module = _moduleRepository.FetchModuleByID(moduleID);

            if (module != null)
            {
                return Ok(module);
            }

            return NotFound(new { Message = $"Module with id {moduleID} is not available." });
        }

        //to create module
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateModule(CreateModule createModule)
        {
            var result = _moduleRepository.CreateModule(createModule);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        //to update module
        [HttpPut("{moduleID}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateModule(int moduleID, UpdateModule updateModule)
        {
            updateModule.moduleID = moduleID;
            var result = _moduleRepository.UpdateModule(updateModule);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        //to delete module
        [HttpDelete("{moduleID}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteModule(int moduleID)
        {
            var result = _moduleRepository.DeleteModule(moduleID);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

    }
}