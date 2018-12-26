using Infrastructure;
using MvcWebLibrary;
using System.Linq;
using Core;

namespace Application
{
    public class SecutiryController : DbAccessController
    {
        public SecutiryController(ApplicationContext dbContext)
            : base(dbContext)
        {
        }

        [HttpPost]
        public IActionResult CreateNewUser(User user, Channel channel)
        {
            if (dbContext.Users.Find(user.SecretLogin) == null)
            {
                dbContext.Users.Add(user);
                dbContext.Channels.Add(channel);
                return Ok();
            }
            else
            {
                return Forbidden();
            }
        }

        [HttpGet]
        public IActionResult CheckIfUserExists(User user)
        {
            var userToAuthenticate = dbContext.Users.Find(user.SecretLogin);
            if (userToAuthenticate == null)
            {
                return Unauthorized();
            }
            return Ok();
        }


        public IActionResult AddChannelIfNotExists(Channel channel)
        {
            if (dbContext.Channels.Find(channel.Id) == null)
            {
                dbContext.Channels.Add(channel);
            }
            return Ok();
        }
    }
}
