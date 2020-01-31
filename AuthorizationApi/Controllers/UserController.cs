using AuthorizationApi.Domain.Model;
using AuthorizationApi.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private Repository repository = new Repository();
        public UserController()
        {

        }
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return repository.GetAllUsers().ToArray();
        }

    }
}
