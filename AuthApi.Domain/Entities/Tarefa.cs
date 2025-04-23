namespace AuthApi.Domain.Entities
{
    // Classe que representa a entidade Tarefa no domínio
    public class Tarefa : ITarefa
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

        // Construtor que inicializa a tarefa com validações
        public Tarefa(int id, string titulo, string descricao)
        {
            // Valida se o título não é vazio ou nulo
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título não pode ser vazio.", nameof(titulo));

            Id = id;
            Titulo = titulo;
            Descricao = descricao;
            Concluida = false;
            DataCriacao = DateTime.Now;
        }

        // Sobrescreve o método ToString para exibir detalhes da tarefa
        public override string ToString()
        {
            return $"ID: {Id}, Título: {Titulo}, Descrição: {Descricao}, Concluída: {Concluida}, Criada em: {DataCriacao}";
        }
    }
}