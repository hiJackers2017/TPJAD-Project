using AuthorizationApi.Domain;
using AuthorizationApi.Domain.Authorization;
using AuthorizationApi.Domain.Model;
using AuthorizationApi.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {

        // PUT api/Authentication/login
        /// <summary>
        /// This endpoint checks the entered credentials from a person.
        /// </summary>
        /// <param name="authenticationCredential">Enter a valid AuthenticationCredential model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPut("login")]
        public ActionResult Put([FromBody] AuthenticationCredential authenticationCredential)
        {
            AuthenticationCredential authenticationCredentialTest = authenticationCredential;
            LoginAuthentication loginAuthentication = new LoginAuthentication(new Repository(), authenticationCredential);
            JwtToken token = loginAuthentication.Handle();

            if (token == null)
            {
                return Unauthorized();
            }

            AuthenticationViewModel viewModel = new AuthenticationViewModel()
            {
                Value = token.ToString()
            };
            return Json(viewModel);
        }

        // PUT api/Authentication/logout
        /// <summary>
        /// This endpoint deletes the token from cookies.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut("logout")]
        public ActionResult Put()
        {
            return Ok();
        }

        // PUT api/Authentication/login/check
        /// <summary>
        /// This endpoint checks the validity of a token.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut("login/check")]
        public ActionResult LoginCheck()
        {
            return Ok();
        }
    }
}
