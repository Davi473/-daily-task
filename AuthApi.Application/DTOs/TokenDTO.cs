namespace AuthApi.Application.DTOs
{
    // DTO para saída do token JWT após login
    public class TokenDTO
    {
        // Token JWT gerado
        public string Token { get; set; }
        
        // Data de expiração do token
        public DateTime Expires { get; set; }
    }
}