using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BachelorOppgave.Controllers.TeacherModels;
using BachelorOppgave.Data;
using BachelorOppgave.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BachelorOppgave.Controllers
{
    [Route("api/[controller]")] //api/teacher
    [ApiController]
    [Authorize]
    [EnableCors("CORS")]
    public class TeacherController : Controller
    {
        private readonly TeacherRepository _teacherRepository;

        public TeacherController(TeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        [HttpGet] //api/teachers
        [AllowAnonymous]
        //to get a list of teachers
        public IEnumerable<Teacher> GetTeacher()
        {
            return _teacherRepository.FetchTeacher();
        }

        //to get one specific teacher
        [HttpGet("{teacherID}")]
        [AllowAnonymous]
        public IActionResult FetchTeacherByID(int teacherID)
        {
            var teacher = _teacherRepository.FetchTeacherByID(teacherID);

            if (teacher != null)
            {
                return Ok(teacher);
            }

            return NotFound(new { Message = $"Teacher with id {teacherID} is not available." });
        }

        //to create teacher
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTeacher(CreateTeacher createTeacher)
        {
            var result = _teacherRepository.CreateTeacher(createTeacher);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        //to update teacher
        [HttpPut("{teacherID}")]
        public IActionResult UpdateTeacher(int teacherID, UpdateTeacher updateTeacher)
        {
            //Admin can change every teacher, but teacher can only modify their own data.
            //Run check, in case you are not admin, to see if we own the teacher item we try to modify.
            if(!base.User.IsInRole("Admin"))
            {
                //Get personId from claims:
                var personId = base.User.Claims
                    .Single(claim => claim.Type == ClaimTypes.Name)
                    .Value;

                //Check the teacher in database that we want to change
                var teacherItem = _teacherRepository.FetchTeacherByID(teacherID);

                //If teacher is not the logged in person, then we are not allowed to modify the teacher item we don't own.
                if (teacherItem?.personID.ToString() != personId)
                {
                    throw new UnauthorizedAccessException();
                }
            }

            updateTeacher.teacherID = teacherID;
            var result = _teacherRepository.UpdateTeacher(updateTeacher);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        //to delete teacher
        [HttpDelete("{teacherID}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTeacher(int teacherID)
        {
            var result = _teacherRepository.DeleteTeacher(teacherID);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }
    }
}
