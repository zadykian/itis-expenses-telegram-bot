using Infrastructure;
using MvcWebLibrary;
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
        public IActionResult CreateNewUser(User user)
        {
            if (dbContext.Users.Find(user.Login) == null)
            {
                dbContext.Users.Add(user);
                return Ok();
            }
            else
            {
                return Forbidden();
            }
        }

        [HttpPost]
        public IActionResult UpdateUserPassword(UserLoginPassword userLoginPassword)
        {
            var userToUpdate = dbContext.Users.Find(userLoginPassword.Login);
            if (userToUpdate == null)
            {
                return BadRequest();
            }
            userToUpdate.UpdatePassword(userLoginPassword.Password);
            dbContext.Users.Update(userToUpdate);
            return Ok();
        }
    }
}
