using UserService.Models.DTOs;
using UserService.Models.Entities;
using UserService.Repositories.Interfaces;
using UserService.Services.Interfaces;
using UserService.Extensions;

namespace UserService.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _logger = logger;
        }

        public async Task<UserResponseDto> Register(UserCreateDto userCreateDto)
        {
            if (await _userRepository.UserExists(userCreateDto.Username))
            {
                _logger.LogWarning("Registration attempt with existing username: {Username}", userCreateDto.Username);
                throw new InvalidOperationException("Username already exists");
            }

            if (await _userRepository.EmailExists(userCreateDto.Email))
            {
                _logger.LogWarning("Registration attempt with existing email: {Email}", userCreateDto.Email);
                throw new InvalidOperationException("Email already exists");
            }

            var user = userCreateDto.ToUserEntity(_passwordHasher);
            await _userRepository.AddUser(user);

            _logger.LogInformation("New user registered with ID: {UserId}", user.Id);
            return user.ToUserResponseDto();
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsername(loginDto.Username);

            if (user == null || !_passwordHasher.VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                _logger.LogWarning("Failed login attempt for username: {Username}", loginDto.Username);
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            if (!user.IsActive)
            {
                _logger.LogWarning("Attempt to login to inactive account: {Username}", loginDto.Username);
                throw new UnauthorizedAccessException("Account is inactive");
            }

            _logger.LogInformation("User logged in: {Username}", loginDto.Username);
            return _jwtTokenGenerator.GenerateToken(user);
        }

        public async Task<string> GeneratePasswordResetToken(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            // Implementar lógica de generación de token de reseteo
            return Guid.NewGuid().ToString();
        }

    }
}