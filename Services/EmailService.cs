namespace PTirta.Services
{
    public interface IEmailService
    {
        Task SendPasswordResetEmailAsync(string email, string token, string resetLink);
    }

    public class MockEmailService : IEmailService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<MockEmailService> _logger;

        public MockEmailService(IWebHostEnvironment env, ILogger<MockEmailService> logger)
        {
            _env = env;
            _logger = logger;
        }

        public async Task SendPasswordResetEmailAsync(string email, string token, string resetLink)
        {
            var message = $"Reset token for {email}: {token}";
            _logger.LogInformation(message);

            var logDir = Path.Combine(_env.WebRootPath, "logs");
            Directory.CreateDirectory(logDir);
            
            var logFile = Path.Combine(logDir, "reset-tokens.txt");
            var expiry = DateTime.UtcNow.AddMinutes(15);
            var line = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} | {email} | {token} | expires: {expiry:yyyy-MM-dd HH:mm:ss}\n";
            
            await File.AppendAllTextAsync(logFile, line);
        }
    }
}
