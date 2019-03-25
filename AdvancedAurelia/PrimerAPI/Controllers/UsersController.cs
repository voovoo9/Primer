using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Sinks.File;

namespace PrimerAPI.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    [DisableCors]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        // GET: api/Users
        [HttpGet]
        public IActionResult Get()
        {
            Log.Information("Started getting users on controller.");

            try
            {
                var results = _userRepository.GetAllUsers();

                List<UserModel> models = _mapper.Map<List<UserModel>>(results);

                Log.Information($"Finished getting {models?.Count} users on controller.");

                return Ok(models);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Log.Information($"Started getting user by {id} on controller.");

            try
            {
                var result = _userRepository.GetUserById(id);

                if (result == null) return NotFound();

                UserModel model = _mapper.Map<UserModel>(result);

                Log.Information($"Finished getting user with id: {id} on controller.");

                return Ok(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult Post([FromBody] UserModel model)
        {
            Log.Information("Started posting new user");

            try
            {
                var user = _mapper.Map<User>(model);

                if (ModelState.IsValid)
                {
                    _userRepository.AddUser(user);

                    Log.Information($"Finished posting new user {user.FirstName}");

                    return Ok(user);
                }
                else return this.StatusCode(StatusCodes.Status422UnprocessableEntity, "Model nije dobar");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserModel model)
        {
            Log.Information($"Started PUT method on user id = {id}.");

            try
            {
                var oldUser = _userRepository.GetUserById(id);
                if (oldUser == null) return NotFound("Could not find form with id = {id}");

                var user = _mapper.Map<User>(model);

                if(ModelState.IsValid)
                {
                    _userRepository.UpdateUser(id, user);

                    Log.Information($"Finished PUT method on user id = {id}.");

                    return Ok(user);
                }
                else return this.StatusCode(StatusCodes.Status422UnprocessableEntity, "Model nije dobar");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Log.Information($"Started deleting user id = {id}");

            try
            {
                var oldForm = _userRepository.GetUserById(id);
                if (oldForm == null) return NotFound();

                _userRepository.DeleteUser(id);

                Log.Information($"User id = {id} deleted.");

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Failed to delete form");
            }
        }
    }
}