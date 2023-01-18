//using Microsoft.AspNetCore.Mvc;
//using promoit_backend_cs.Services;
//using promoit_backend_cs_api.Models;
//using promoit_backend_cs_api.ModelsDTO;

//namespace promoit_backend_cs_api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        private readonly UserService _userService;

//        public UsersController(UserService userService)
//        {
//            _userService = userService;
//        }

//        // GET: api/Users
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
//        {
//            var allUsers = await _userService.GetAllUsers();
//            return Ok(allUsers);
//        }

//        // GET: api/Users/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<User>> GetUser(int id)
//        {
//            var userById = await _userService.GetUserById(id);
//            return Ok(userById);
//        }

//        [HttpGet("/api/Users/UsersWithRoles")]
//        public async Task<ActionResult<User>> GetUsersWithRoles()
//        {
//            var listOfUsersWithRoles = await _userService.GetUsersWithRoles();
//            return Ok(listOfUsersWithRoles);
//        }

//        // PUT: api/Users/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutUser(int id, UserDTO user)
//        {
//            var editedUser = await _userService.EditUser(id, user); 
           
//           // _context.Entry(user).State = EntityState.Modified;
//            return NoContent();
//        }

//        // POST: api/Users
//        [HttpPost]
//        public async Task<ActionResult<User>> PostUser(UserDTO user)
//        {
//            var newUser = new UserDTO();

//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            newUser = await _userService.CreateUser(user);
//            return CreatedAtAction("GetUser", new { id = newUser.Id }, newUser);
//        }

//        // DELETE: api/Users/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteUser(int id)
//        {
//            await _userService.DeleteUser(id);
//            return NoContent();
//        }

//    }
//}
