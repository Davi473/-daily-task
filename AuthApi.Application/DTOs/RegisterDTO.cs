namespace AuthApi.Application.DTOs
{
    // DTO para entrada de dados no registro de usuário
    public class RegisterDTO
    {
        // Email do usuário
        public string Email { get; set; }
        
        // Senha do usuário (em texto puro, será hasheada)
        public string Password { get; set; }
    }
}