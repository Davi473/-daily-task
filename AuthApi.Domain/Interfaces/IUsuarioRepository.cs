using AuthApi.Domain.Entities; // Importa o namespace da classe Usuario

namespace AuthApi.Domain.Interfaces
{
    // Interface que define o contrato para o repositório de usuários
    public interface IUsuarioRepository
    {
        // Método para salvar um usuário (novo ou atualizado)
        void Save(Usuario usuario);
        
        // Método para buscar um usuário pelo email
        Usuario FindByEmail(string email);
    }
}