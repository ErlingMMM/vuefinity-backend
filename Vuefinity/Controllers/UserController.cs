
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using Vuefinity.Data.Exceptions;
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

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


    
        /// <summary>
        /// Creating a new exercise to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostExercise(UserPostDTO exercise)
        {
            var newExercise = await _userService.AddAsync(_mapper.Map<User>(exercise));

            return CreatedAtAction("GetExercise",
                new { id = newExercise.Id },
                _mapper.Map<UserDTO>(newExercise));
        }

       
    }
}
