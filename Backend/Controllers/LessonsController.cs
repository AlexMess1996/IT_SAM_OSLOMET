using BachelorOppgave.Controllers.LessonsModels;
using BachelorOppgave.Data;
using BachelorOppgave.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BachelorOppgave.Controllers
{
    [Route("api/module/{moduleID}/[controller]")] //api/module/{moduleID}/Lessons
    [ApiController]
    [Authorize]
    [EnableCors("CORS")]
    public class LessonsController : Controller
    {
        private readonly LessonRepository _lessonRepository;

        public LessonsController(LessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }
        [HttpGet] //api/lessons
        [AllowAnonymous]
        //to get a list of lessons
        public IActionResult GetLessons()
        {
            return Ok(_lessonRepository.FetchLessons());
        }

        //to get one specific lesson
        [HttpGet("{lessonID}")]
        [AllowAnonymous]
        public IActionResult FetchLessonByID(int moduleID, int lessonID)
        {
            var lesson = _lessonRepository.FetchLessonByID(lessonID);

            if (lesson != null)
            {
                return Ok(lesson);
            }

            return NotFound(new { Message = $"Lesson with id {lessonID} is not available." });
        }

        //to create lesson
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateLesson(int moduleID, CreateLesson createLesson)
        {
            createLesson.moduleID = moduleID;
            var result = _lessonRepository.CreateLesson(createLesson);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        //to update lesson
        [HttpPut("{lessonID}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateLesson(int moduleID, int lessonID, Lesson lesson)
        {
            lesson.moduleID = moduleID;
            lesson.lessonID = lessonID;
            var result = _lessonRepository.UpdateLesson(lesson);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        //to delete lesson
        [HttpDelete("{lessonID}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteLesson(int moduleID, int lessonID)
        {
            var result = _lessonRepository.DeleteLesson(lessonID);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }
    }
}
