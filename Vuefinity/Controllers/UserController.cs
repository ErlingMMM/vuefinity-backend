
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Vuefinity.Data.Exceptions;
using Microsoft.Extensions.Logging;
using Vuefinity.Data.Models;
using Vuefinity.Services.Users;
using Vuefinity.Data.DTO.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vuefinity.Controllers
{
    [ApiController]
    [Route("api/v1/User")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiConventionType(typeof(DefaultApiConventions))]

    //These files define the API endpoints, their routes, and the actions to be taken for each endpoint.
    //Controllers interact with services to perform business logic.
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }


          /// <summary>
        /// Gets a list of all users.
        /// </summary>
        /// <returns>A list of user objects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                var userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
                return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing GetAllUsers.");

                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>
        /// Creating a new user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(UserPostDTO user)
        {
            var newUser = await _userService.AddAsync(_mapper.Map<User>(user));

            return CreatedAtAction("GetUser",
                new { id = newUser.Id },
                _mapper.Map<UserDTO>(newUser));
        }

       
    }
}
