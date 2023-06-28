using FirstProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FirstProject.Extensions
{
    public class CustomUserManager : UserManager<User>
    {
        public CustomUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer,
                  errors, services, logger)
        {
        }

        public override async Task<IdentityResult> CreateAsync(User user)
        {
            try
            {
                IdentityResult result;
                // Perform any additional operations before calling the base CreateAsync, if needed
                user.NormalizedEmail = NormalizeEmail(user.Email);
                user.NormalizedUserName = NormalizeName(user.UserName);
                if (Users.FirstOrDefault(x => x.Id == user.Id) == null)
                {
                    result = await base.CreateAsync(user);
                }
                else
                {
                    result = await base.UpdateAsync(user);
                }
                //var result = await base.CreateAsync(user);
                return result;
            }
            catch (Exception ex)
            {
                // Handle the exception, log the error, or perform any necessary error handling
                Console.WriteLine($"Error creating or updating user: {ex.Message}");
                return IdentityResult.Failed(new IdentityError { Description = "User creation failed" });
            }
        }
    }
}
