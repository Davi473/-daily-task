using AuthApi.Domain.Entities;

namespace AuthApi.Domain.Interfaces
{
    // Interface que define o contrato para o repositório de tarefas
    public interface ITarefaRepository
    {
        // Salva uma tarefa (nova ou atualizada)
        void Save(Tarefa tarefa);
        
        // Busca uma tarefa pelo título
        Tarefa FindByTitulo(string titulo);
        
        // Retorna todas as tarefas
        List<Tarefa> GetAll();
        
        // Deleta uma tarefa pelo ID
        void Delete(int id);
    }
}