using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ManagementSystemDbContext _context;
        public UserRepository(ManagementSystemDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync() 
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            var login = new Login
            { 
                FkuserId = user.UserId, 
                UserEmail = user.UserEmail,
                Password = user.Password,
                CreatedAt = DateTime.Now,
            };

            _context.Logins.Add(login);
            await _context.SaveChangesAsync();                          
        }
        public async Task<Login?> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Logins
                .FirstOrDefaultAsync(u => u.UserEmail == email && u.Password == password);
        }
 
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user !=null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
        public async Task AddLoginAsync(Login login)
        {
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();
        }
    }
}
