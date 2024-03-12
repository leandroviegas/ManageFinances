using ManageFinances.Entities;
using ManageFinances.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageFinances.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository repository)
        {
            this.userRepository = repository;
        }

        // GET: api/<UserController>
        [HttpGet]
        public List<User> Get()
        {
            return userRepository.GetUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return userRepository.Get(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public User Post([FromBody] User user)
        {
           return userRepository.Create(user);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public User Put([FromBody] User user)
        {
           return userRepository.Update(user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            return userRepository.Delete(id);
        }
    }
}
