namespace AuthApi.Application.DTOs
{
    // DTO para entrada e saída de dados de tarefas
    public class TarefaDTO
    {
        // Identificador único da tarefa
        public int Id { get; set; }
        
        // Título da tarefa
        public string Titulo { get; set; }
        
        // Descrição da tarefa
        public string Descricao { get; set; }
        
        // Indica se a tarefa está concluída
        public bool Concluida { get; set; }
        
        // Data de criação da tarefa
        public DateTime DataCriacao { get; set; }
    }
}