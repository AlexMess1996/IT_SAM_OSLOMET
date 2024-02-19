using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BachelorOppgave.Data.Models;
using BachelorOppgave.Helpers;
using BachelorOppgave.Data;

namespace BachelorOppgave.Services
{
    public class PersonService
    {
        private readonly AppSettings _appSettings;
        private readonly PersonRepository _personRepository;

        public PersonService(IOptions<AppSettings> appSettings, PersonRepository personRepository)
        {
            _appSettings = appSettings.Value;
            _personRepository = personRepository;
        }

        public Person Authenticate(string username, string password)
        {
            var person = _personRepository
                .FetchPersons() //TODO: Change to get single person. Hash should be done before because of timing attacks.
                .SingleOrDefault(personItem => personItem.username == username && personItem.password == password);

            // return null if user not found
            if (person == null)
                return null;

            // en liste med verdier for forskjellige attributter til en person som "signeres"
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.personID.ToString()) };

            if (person.adminID != null)
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            if (person.teacherID != null)
                claims.Add(new Claim(ClaimTypes.Role, "Teacher"));

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            person.Token = tokenHandler.WriteToken(token);

            return person;
        }
    }
}
