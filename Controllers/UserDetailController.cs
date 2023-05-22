using Microsoft.AspNetCore.Mvc;
using eShopApi.Models;
using eShopApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailController : Controller
    {
        private readonly UserDetailService _userDetailService;

        public UserDetailController(UserDetailService userDetailService)
        {
            _userDetailService = userDetailService;
        }

        // POST api/userdetail
        [HttpPost]
        public async Task<ActionResult<string>> SaveUserDetail(UserDetail userDetail)
        {
            var response = await _userDetailService.SaveUserDetailAsync(userDetail);
            return Ok(response);
        }


        // This method handles login requests and generates a JWT token if the user is authenticated
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login model)
        {
            // Get the user details from the database using the email ID
            var user = await _userDetailService.GetUserByEmailAsync(model.EmailId);

            // Check if the user exists and the password is correct
            if (user != null && model.Password == user.Password)
            {
                // Create a new token descriptor with the user ID as the subject and an expiration time of 15 minutes
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim("UserId", user.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(120),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ZdYM000OLlMQG6VVVp1OH7Xarp7gHuw1qvUC5dcGt3SNM")), SecurityAlgorithms.HmacSha256Signature)
                };

                // Create a new instance of the JwtSecurityTokenHandler class and generate the security token
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                // Write the token to a string and return it as part of the response
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
            {
                // If the user is not authenticated, return a bad request response with an error message
                return BadRequest(new { message = "Email or Password is incorrect." });
            }
        }


        // GET api/userdetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetAllUserDetails()
        {
            var userDetails = await _userDetailService.GetAllUserDetailsAsync();
            return Ok(userDetails);
        }


        // GET api/userdetail/5
        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDetail>> GetUserDetail(int userId)
        {
            var userDetail = await _userDetailService.GetUserDetailAsync(userId);
            if (userDetail == null)
            {
                return NotFound();
            }
            return userDetail;
        }

        // GET api/userdetail/email/{emailId}
        [HttpGet("email/{emailId}")]
        public async Task<ActionResult<UserDetail>> GetUserByEmail(string emailId)
        {
            var userDetail = await _userDetailService.GetUserByEmailAsync(emailId);
            if (userDetail == null)
            {
                return NotFound();
            }
            return userDetail;
        }


        // PUT api/userdetail
        [HttpPut("{userId}")]
        public async Task<ActionResult<string>> UpdateUserDetail(int userId, UserDetail userDetail)
        {
            if (userId != userDetail.UserId)
            {
                return BadRequest();
            }
            var response = await _userDetailService.UpdateUserDetailAsync(userDetail);
            return Ok(response);
        }

        // DELETE api/userdetail/5
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserDetail(int userId)
        {
            try
            {
                var response = await _userDetailService.DeleteUserDetailAsync(userId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting user detail with id {userId}: {ex.Message}\n");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the user detail.");
            }
        }





    }

}