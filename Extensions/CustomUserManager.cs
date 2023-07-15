using FirstProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace FirstProject.Extensions
{
    public class CustomUserManager : UserManager<User>
    {
        private readonly ILogger<CustomUserManager> _logger;
        public CustomUserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<CustomUserManager> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer,
                  errors, services, logger)
        {
            _logger = logger;
        }

        public override async Task<IdentityResult> CreateAsync(User user)
        {
            try
            {
                user.NormalizedEmail = NormalizeEmail(user.Email);
                user.NormalizedUserName = NormalizeName(user.UserName);

                return Users.FirstOrDefault(x => x.Id == user.Id) == null ? await base.CreateAsync(user) : await base.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                // Handle the exception, log the error, or perform any necessary error handling.
                _logger.LogError($"Error creating or updating _user: {ex.Message}");
                return IdentityResult.Failed(new IdentityError { Description = "Failed to create/update user" });
            }
        }
    }
}
