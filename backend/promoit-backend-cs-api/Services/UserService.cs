//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using promoit_backend_cs_api.Data;
//using promoit_backend_cs_api.Models;
//using promoit_backend_cs_api.ModelsDTO;

//namespace promoit_backend_cs.Services
//{
//    public class UserService
//    {

//        private readonly promo_itContext _context;
//        private readonly ILogger<UserService> _logger;
//        private readonly ExistsService _existsService;
//        private readonly PasswordHasher<UserDTO> _passwordHasher;

//        public UserService(promo_itContext db, ILogger<UserService> logger, ExistsService existsService)
//        {
//            _context = db;
//            _logger = logger;
//            _existsService = existsService;

//        }

//        public async Task<IEnumerable<UserDTO>> GetAllUsers()
//        {
//            return await _context.Users.Where(x => x.StatusId == 1)
//                                       .Select(x => DTOService.UserToDTO(x))
//                                       .ToListAsync();
//        }

//        public async Task<object> GetUsersWithRoles()
//        {
//            var listOfUsersWithRoles = new List<object>();
//            try
//            {
//                var listOfUsers = await _context.Users.Include(u => u.Role)
//                                                      .Select(u => new
//                                                      {
//                                                          u.Id,
//                                                          u.FirstName, 
//                                                          u.LastName,
//                                                          u.Login,
//                                                          u.Password,
//                                                          Role = u.Role.RoleName
//                                                      })
//                                                      .ToListAsync();
//                if (listOfUsers == null)
//                {
//                    throw new Exception($"Cannot find any users");
//                }
//                foreach (var user in listOfUsers )
//                {
//                    listOfUsersWithRoles.Add(user);
//                }
//                return listOfUsersWithRoles;
//            }
//            catch(Exception exception)
//            {
//                _logger.LogError(exception, $"Error getting users");
//                throw new Exception($"Error getting users", exception);
//            }
//        }

//        public async Task<UserDTO> GetUserById(int? id)
//        {
//            try
//            {
//                var user = await _context.Users.Where(user => user.Id == id)
//                                               .FirstOrDefaultAsync();
//                if (user == null)
//                {
//                    throw new Exception($"Cannot find user with {id}");
//                }
//                return DTOService.UserToDTO(user);

//            }
//            catch (Exception exception)
//            {
//                _logger.LogError(exception, $"Error getting user with ID {id}");
//                throw new Exception($"Error getting user with ID {id}", exception);
//            }
//        }

//        public async Task<UserDTO> CreateUser(UserDTO userDTO)
//        {
//            var newUser = new User
//            {
//                FirstName = userDTO.FirstName,
//                LastName = userDTO.LastName,
//                Login = userDTO.Login,
//               /* Password = userDTO.Password,*/
//                Password = _passwordHasher.HashPassword(userDTO, userDTO.Password),

//                RoleId = userDTO.RoleId,
//                CreateDate= DateTime.Now,
//                UpdateDate= DateTime.Now,
//                CreateUserId= userDTO.CreateUserId,
//                UpdateUserId= userDTO.UpdateUserId,
//                StatusId= 1
//            };
//            try
//            {
//                _context.Users.Add(newUser);
//                await _context.SaveChangesAsync();
//                return DTOService.UserToDTO(newUser);

//            }
//            catch (Exception exception)
//            {
//                _logger.LogError(exception, $"Error creating a new user");
//                throw new Exception("Cannot create a user", exception);
//            }
//        }

//        public async Task<UserDTO> EditUser(int id, UserDTO user)
//        {
//            var existingUser = await _context.Users.FindAsync(id);

//            if (existingUser == null)
//            {
//                throw new Exception($"There is no such user");
//            }
//            if (id != user.Id)
//            {
//                throw new Exception($"The user with the ID {id} was not found");
//            }

//                existingUser.FirstName = user.FirstName;
//                existingUser.LastName = user.LastName;
//                existingUser.Login = user.Login;
//                existingUser.Password = user.Password;
//                existingUser.RoleId = user.RoleId;
//                existingUser.StatusId = user.StatusId;
//                existingUser.UpdateDate = DateTime.Now;

//            try
//            {
//               // _context.Update(existingUser);
//                await _context.SaveChangesAsync();

//            }
//            catch (DbUpdateConcurrencyException exception)
//            {
//                if (!_existsService.UserExists(user.Id))
//                {
//                    throw new Exception($"The user with the ID {id} does not exist!");
//                }
//                else
//                {

//                    _logger.LogError(exception, $"Error editting user with ID {id}");

//                    throw new Exception($"Error editting user with ID {id}", exception);
//                }
//            }
//            return DTOService.UserToDTO(existingUser);
//        }

//        public async Task<UserDTO> DeleteUser(int id)
//        {
//            var existingUser = await _context.Users.FindAsync(id);

//            if (existingUser == null)
//            {
//                throw new Exception($"There is no such user");
//            }

//            existingUser.StatusId = 2;
//            existingUser.UpdateDate = DateTime.Now;

//            try
//            {
//                // _context.Update(existingUser);
//                await _context.SaveChangesAsync();

//            }
//            catch (DbUpdateConcurrencyException exception)
//            {
//                 _logger.LogError(exception, $"Error deleting user with ID {id}");

//                throw new Exception($"Error deleting user with ID {id}", exception);
//            }
//            return DTOService.UserToDTO(existingUser);
//        }

//    }
//}
