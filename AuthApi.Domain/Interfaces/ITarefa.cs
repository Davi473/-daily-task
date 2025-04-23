namespace AuthApi.Domain.Entities
{
    // Interface que define o contrato para a entidade Tarefa
    public interface ITarefa
    {
        // Identificador único da tarefa
        int Id { get; set; }
        
        // Título da tarefa
        string Titulo { get; set; }
        
        // Descrição da tarefa
        string Descricao { get; set; }
        
        // Indica se a tarefa está concluída
        bool Concluida { get; set; }
        
        // Data de criação da tarefa
        DateTime DataCriacao { get; set; }

        // Método para exibir detalhes da tarefa
        string ToString();
    }
}