using BachelorOppgave.Data;
using BachelorOppgave.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using BachelorOppgave.Controllers.AdminModels;

namespace BachelorOppgave.Controllers
{
    [Route("api/[controller]")] //api/admin
    [ApiController]
    [Authorize(Roles = "Admin")]
    [EnableCors("CORS")]
    public class AdminController : Controller
    {
        private readonly AdminRepository _adminRepository;

        public AdminController(AdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        [HttpGet] //api/admin
        //to get a list of admins
        public IEnumerable<Admin> GetAdmins()
        {
            return _adminRepository.FetchAdmin();
        }

        //to get one specific admin
        [HttpGet("{adminID}")]
        public IActionResult FetchAdminByID(int adminID)
        {
            var admin = _adminRepository.FetchAdminByID(adminID);

            if (admin != null)
            {
                return Ok(admin);
            }

            return NotFound(new { Message = $"Module with id {adminID} is not available." });
        }

        //to create admin
        [HttpPost]
        public IActionResult CreateAdmin(CreateAdmin createAdmin)
        {
            var result = _adminRepository.CreateAdmin(createAdmin);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        //to update admin
        [HttpPut("{adminID}")]
        public IActionResult UpdateAdmin(int adminID, UpdateAdmin updateAdmin)
        {
            updateAdmin.adminID = adminID;
            var result = _adminRepository.UpdateAdmin(updateAdmin);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        //to delete admin
        [HttpDelete("{adminID}")]
        public IActionResult DeleteAdmin(int adminID)
        {
            var result = _adminRepository.DeleteAdmin(adminID);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }
    }
}
