using Microsoft.AspNetCore.Mvc;

namespace Simple_User_Management_API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Simple_User_Management_API.Models.UserApi.Models;

    namespace UserApi.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class UsersController : ControllerBase
        {
            private readonly List<User> _users;

            public UsersController(List<User> users)
            {
                _users = users;

                // seed sample data if empty
                if (!_users.Any())
                {
                    _users.AddRange(new[]
                    {
                    new User{ FullName="Ahmed Ali", Email="ahmed@example.com", Age = 29 },
                    new User{ FullName="Sara Mohamed", Email="sara@example.com", Age = 25 }
                });
                }
            }

            // GET: api/users
            [HttpGet]
            public ActionResult<IEnumerable<User>> GetAll() => Ok(_users);

            // GET: api/users/{id}
            [HttpGet("{id:guid}")]
            public ActionResult<User> Get(Guid id)
            {
                var u = _users.FirstOrDefault(x => x.Id == id);
                if (u == null) return NotFound();
                return Ok(u);
            }

            // POST: api/users
            [HttpPost]
            public ActionResult<User> Create([FromBody] UserDto dto)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                // basic uniqueness check
                if (_users.Any(x => x.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase)))
                    return Conflict(new { message = "Email already in use." });

                var user = new User
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    Age = dto.Age
                };
                _users.Add(user);
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }

            // PUT: api/users/{id}
            [HttpPut("{id:guid}")]
            public ActionResult Update(Guid id, [FromBody] UserDto dto)
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = _users.FirstOrDefault(x => x.Id == id);
                if (user == null) return NotFound();

                // email uniqueness (exclude current)
                if (_users.Any(x => x.Email.Equals(dto.Email, StringComparison.OrdinalIgnoreCase) && x.Id != id))
                    return Conflict(new { message = "Email already in use." });

                user.FullName = dto.FullName;
                user.Email = dto.Email;
                user.Age = dto.Age;

                return NoContent();
            }

            // DELETE: api/users/{id}
            [HttpDelete("{id:guid}")]
            public ActionResult Delete(Guid id)
            {
                var user = _users.FirstOrDefault(x => x.Id == id);
                if (user == null) return NotFound();
                _users.Remove(user);
                return NoContent();
            }
        }
    }

}
