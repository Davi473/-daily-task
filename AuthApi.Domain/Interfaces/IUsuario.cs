namespace AuthApi.Domain.Entities
{
    // Interface que define o contrato para a entidade Usuário
    public interface IUsuario
    {
        // Identificador único do usuário
        int Id { get; set; }
        
        // Email do usuário
        string Email { get; set; }
        
        // Hash da senha
        string PasswordHash { get; set; }
    }
}