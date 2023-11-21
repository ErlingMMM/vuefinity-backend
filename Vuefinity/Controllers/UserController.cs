
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Vuefinity.Data.Exceptions;
using Microsoft.Extensions.Logging;
using Vuefinity.Data.Models;
using Vuefinity.Services.Users;
using Vuefinity.Data.DTO.User;
using Vuefinity.Data.DTO.Score;
using Microsoft.EntityFrameworkCore;

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
        /// Get a spesific users from database using their id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            try
            {
                return Ok(_mapper
                    .Map<UserDTO>(
                        await _userService.GetByIdAsync(id)));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
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

        /// <summary>
        /// Listing top 10 users by score
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("top10")]
        public async Task<ActionResult<ICollection<User>>> GetTop10UsersByScore()
        {
            var top10Users = await _userService.Users
                .OrderByDescending(u => u.Score)
                .Take(10)
                .ToListAsync();

            return Ok(top10Users);
        }

        /// <summary>
        ///Update user score if the new score is higher than the old score.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}/updateScore")]
        public async Task<ActionResult<UserDTO>> UpdateUserScore(int id, [FromBody] UpdateUserScoreDTO updateUserScoreDTO)
        {
            try
            {
                var existingUser = await _userService.GetByIdAsync(id);

                // Update the user's score
                existingUser.Score = updateUserScoreDTO.NewScore;

                // Save the changes to the database
                await _userService.UpdateAsync(existingUser);

                // Return the updated user information
                return Ok(_mapper.Map<UserDTO>(existingUser));
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the score for user with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
