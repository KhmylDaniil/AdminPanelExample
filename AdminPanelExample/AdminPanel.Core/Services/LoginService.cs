using AdminPanel.Core.Contracts.Login;
using AdminPanel.Core.Exceptions;
using AdminPanel.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Core.Services
{
    /// <summary>
    /// Login service
    /// </summary>
    public class LoginService : ILoginService
    {
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IAppDbContext _appDbContext;

        public LoginService(IPasswordService passwordService, IJwtService jwtService, IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _jwtService = jwtService;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="command">login user command</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>token</returns>
        public async Task<Token> LoginUserAsync(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(x => x.Email == command.Email, cancellationToken)
                ?? throw new RequestValidationException("Password or login are incorrect.");

            if (!_passwordService.ComparePasswordHashes(dbPasswordHash: user.Password, password: command.Password))
                throw new RequestValidationException("Password or login are incorrect.");

            var token = _jwtService.GenerateToken(user);

            return token;
        }
    }
}
