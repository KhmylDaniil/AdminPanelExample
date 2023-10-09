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

        /// <summary>
        /// ctor for <see cref="LoginService"/>
        /// </summary>
        /// <param name="passwordService">Password service</param>
        /// <param name="jwtService">Jwt service</param>
        /// <param name="appDbContext">Db context</param>
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
                ?? throw new RequestValidationException(Constants.IncorrectLoginOrPassword);

            if (!_passwordService.ComparePasswordHashes(dbPasswordHash: user.Password, password: command.Password))
                throw new RequestValidationException(Constants.IncorrectLoginOrPassword);

            var token = _jwtService.GenerateToken(user);

            return token;
        }
    }
}
