using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Dto;

namespace ToDo.Web.Identity
{
    public class ToDoUserStore : IUserPasswordStore<User>, IUserEmailStore<User>
    {
        private readonly ToDoDbContext _context;

        public ToDoUserStore(ToDoDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Add(user);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError() {Description = $"Could not create user {user.Username}."});
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            var userFromDb = await _context.User.FindAsync(user.Id);
            _context.Remove(userFromDb);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError() {Description = $"Could not delete user {user.Username}."});
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.User.SingleOrDefaultAsync(u => u.Id.Equals(Guid.Parse(userId)), cancellationToken);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _context.User.SingleOrDefaultAsync(u => u.Username.Equals(normalizedUserName),
                cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return user.Username;
        }

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return user.Id.ToString();
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return user.Username;
        }

        public async Task SetNormalizedUserNameAsync(User user, string normalizedName,
            CancellationToken cancellationToken)
        {
            user.Username = normalizedName;
        }

        public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Update(user);
            var affectedRows = await _context.SaveChangesAsync(cancellationToken);
            return affectedRows > 0
                ? IdentityResult.Success
                : IdentityResult.Failed(new IdentityError() {Description = $"Could not update user {user.Username}."});
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return user.PasswordHash;
        }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return !string.IsNullOrWhiteSpace(user.PasswordHash);
        }

        public async Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
        }

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await _context.User.SingleOrDefaultAsync(u => u.Username.Equals(normalizedEmail),
                cancellationToken);
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return user.Username;
        }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return true;
        }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return user.Username;
        }

        public async Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            user.Username = email;
        }

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
        }

        public async Task SetNormalizedEmailAsync(User user, string normalizedEmail,
            CancellationToken cancellationToken)
        {
            user.Username = normalizedEmail;
        }
    }
}