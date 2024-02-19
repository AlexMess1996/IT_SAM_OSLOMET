using BachelorOppgave.Controllers.PersonsModels;
using BachelorOppgave.Data;
using BachelorOppgave.Data.Models;
using BachelorOppgave.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BachelorOppgave.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CORS")]
    public class PersonsController : ControllerBase
    {
        private readonly PersonRepository _personRepository;
        private readonly PersonService _personService;

        public PersonsController(PersonRepository personRepository, PersonService personService)
        {
            _personRepository = personRepository;
            _personService = personService;
        }

        //to get list of persons
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_personRepository.FetchPersons());
        }

        //to get one specific person
        [HttpGet("{personID}")]
        public IActionResult FetchPersonByID(int personID)
        {
            var person = _personRepository.FetchPersonByID(personID);

            if (person != null)
            {
                return Ok(person);
            }

            return NotFound(new { Message = $"Person with id {personID} is not available." });
        }

        //to create person
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreatePerson(CreatePerson person)
        {

            var result = _personRepository.CreatePerson(person);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        //to update person
        [HttpPut("{personID}")]
        public IActionResult UpdatePerson(int personID, UpdatePerson updatePerson)
        {
            updatePerson.personID = personID;
            var result = _personRepository.UpdatePerson(updatePerson);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }
        //to delete person
        [HttpDelete("{personID}")]
        public IActionResult DeletePerson(int personID)
        {
            var result = _personRepository.DeletePerson(personID);
            if (result > 0)
            {
                return Ok(result);
            }

            return StatusCode(500, new { Message = "Some error happened." });
        }

        //to authenticate person
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var person = _personService.Authenticate(model.Username, model.Password);

            if (person == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            return Ok(person);
        }
    }
}