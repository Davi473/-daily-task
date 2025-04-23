namespace AuthApi.Application.DTOs
{
    // DTO para entrada de dados no login
    public class LoginDTO
    {
        // Email do usuário
        public string Email { get; set; }
        
        // Senha do usuário (em texto puro, será verificada contra o hash)
        public string Password { get; set; }
    }
}