using Infrastructure;
using MvcWebLibrary;
using System.Linq;
using Core;

namespace Application
{
    public class SecurityController : DbAccessController
    {
        public SecurityController(ApplicationContext dbContext)
            : base(dbContext)
        {
        }

        [HttpGet]
        public IActionResult CheckIfUserExists(User user)
        {
            var userToAuthenticate = dbContext.Users.Find(user.SecretLogin);
            dbContext.SaveChanges();
            if (userToAuthenticate == null)
            {
                return Unauthorized();
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateNewUser(Channel channel)
        {
            if (dbContext.Users.Find(channel.User.SecretLogin) == null)
            {
                dbContext.Users.Add(channel.User);
                dbContext.Channels.Add(channel);
                dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return Forbidden();
            }
        }

        [HttpPost]
        public IActionResult AddChannelIfNotExists(Channel channel)
        {
            if (dbContext.Channels.Find(channel.Id) == null)
            {
                dbContext.Channels.Add(channel);
                dbContext.SaveChanges();
            }
            return Ok();
        }
    }
}
