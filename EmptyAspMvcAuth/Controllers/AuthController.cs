using EmptyAspMvcAuth.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace EmptyAspMvcAuth.Controllers
{
   
    public class AuthController : Controller
    {
        private DataBaseService _db;
        private AuthService _authService;

        public AuthController(DataBaseService db, AuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        [HttpGet]
        [Route("/auth/getAll")]
        public async Task<ActionResult> GetAll()
        {
            // There is a better way to handle exceptions, please read about Unhandled Excepiton middlewares
            try
            {
                // It is pointless since you are using non-async code. This makes things actually slower. You are missing the point of the async programming
                var users = await Task.Run(() => _db.users );

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("/auth/register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerData)
        {
            // Can be used like that, but when you will have a lot of different data it will become complex
            // Please read about DTO validation attributes
            if (registerData == null)
                return BadRequest("register data is null");
            try
            {
                _authService.RegisterUser(registerData);

                return Ok("user create successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/auth/login")]
        public async Task<ActionResult> Login([FromBody] RegisterDto registerData)
        {
            if (registerData == null)
                return BadRequest("login data is null");
            try
            {
                var existingUser = _authService.LogIn(registerData);

                return Ok($"You loged in successfully your id: {existingUser.Id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
