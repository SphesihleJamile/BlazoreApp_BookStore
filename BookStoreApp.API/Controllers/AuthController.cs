using BookStoreApp.API.Repositories.Abstract;
using BookStoreApp.API.Responses;
using BookStoreApp.API.Static;
using BookStoreApp.API.Validations.UserValidations;
using BookStoreApp.API.ViewModels.AuthorViewModels;
using BookStoreApp.API.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserRepository _userRepository;

        public AuthController(ILogger<AuthController> logger,
                                IUserRepository userRepository)
        {
            this._logger = logger;
            this._userRepository = userRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserCreateVM userVM)
        {
            _logger.LogInformation("Registration Attempt For User - {Email}", userVM.Email);
            try
            {
                var validationResponse = await new UserCreateVMValidator().ValidateAsync(userVM);
                if (!validationResponse.IsValid)
                {
                    _logger.LogWarning("User Validation Failed : Endpoint - {Register} | UserVM - {@userVM}", nameof(Register), userVM);
                    List<string> errors = new List<string>();
                    foreach (var error in validationResponse.Errors)
                        errors.Add(error.ErrorMessage);
                    return BadRequest(error: errors);
                }

                var result = await _userRepository.CreateUserAsync(userVM);

                if(result != null)
                {
                    _logger.LogWarning("Failed To Register User : Endpoint - {Register} | UserVM - {@userVM}", nameof(Register), userVM);
                    return BadRequest(result);
                }

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error Registering User in {nameof(Register)}");
                return Problem(Messages.Error500Message, statusCode: 500);
                //return StatusCode(500, Messages.Error500Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Login(UserLoginVM userLogin)
        {
            _logger.LogInformation("Login Attempt For User - {Username}", userLogin.Username);
            try
            {
                var validationResponse = await new UserLoginVMValidator().ValidateAsync(userLogin);
                if(!validationResponse.IsValid)
                {
                    _logger.LogWarning("User Validation Failed : Endpoint - {Login} | UserLogin - {@userVM}", nameof(Login), userLogin);
                    List<string> errors = new List<string>();
                    foreach (var error in validationResponse.Errors)
                        errors.Add(error.ErrorMessage);
                    return BadRequest(error: errors);
                }

                var loginResult = await _userRepository.LoginUserAsync(userLogin);

                if (loginResult.Length == 1)
                {
                    _logger.LogWarning("User Login Failed for User {@userLogin}", userLogin);
                    return Unauthorized(userLogin);
                }

                var authResponse = (AuthResponse)loginResult[1];

                return Accepted(authResponse);

            }catch(Exception ex)
            {
                _logger.LogError(ex, $"Error Loggin In User in {nameof(Login)}");
                return Problem(Messages.Error500Message, statusCode: 500);
            }
        }
    }
}
