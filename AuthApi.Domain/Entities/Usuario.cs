namespace AuthApi.Domain.Entities
{
    // Classe que representa a entidade Usuário no domínio
    public class Usuario : IUsuario
    {
        // Identificador único do usuário
        public int Id { get; set; }
        
        // Email do usuário
        public string Email { get; set; }
        
        // Hash da senha (armazenado de forma segura)
        public string PasswordHash { get; set; }

        // Construtor que inicializa o usuário com validações
        public Usuario(int id, string email, string passwordHash)
        {
            // Valida se o email não é vazio ou nulo
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não pode ser vazio.", nameof(email));
            
            // Valida se o hash da senha não é vazio ou nulo
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Hash da senha não pode ser vazio.", nameof(passwordHash));

            Id = id;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}