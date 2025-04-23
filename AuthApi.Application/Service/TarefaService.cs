using AuthApi.Domain.Entities;
using AuthApi.Domain.Interfaces;
using AuthApi.Application.DTOs;

namespace AuthApi.Application.Services
{
    // Serviço que gerencia operações com tarefas
    public class TarefaService
    {
        // Repositório de tarefas
        private readonly ITarefaRepository _tarefaRepository;

        // Construtor com injeção de dependência
        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        // Cria uma nova tarefa
        public void CreateTarefa(TarefaDTO tarefaDTO)
        {
            var tarefa = new Tarefa(tarefaDTO.Id, tarefaDTO.Titulo, tarefaDTO.Descricao);
            _tarefaRepository.Save(tarefa);
        }

        // Obtém uma tarefa pelo título
        public TarefaDTO GetTarefaByTitulo(string titulo)
        {
            var tarefa = _tarefaRepository.FindByTitulo(titulo);
            if (tarefa == null)
                return null;

            return new TarefaDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Concluida = tarefa.Concluida,
                DataCriacao = tarefa.DataCriacao
            };
        }

        // Atualiza uma tarefa existente
        public bool UpdateTarefa(TarefaDTO tarefaDTO)
        {
            var tarefa = _tarefaRepository.FindByTitulo(tarefaDTO.Titulo);
            if (tarefa == null)
                return false;

            tarefa.Descricao = tarefaDTO.Descricao;
            tarefa.Concluida = tarefaDTO.Concluida;
            _tarefaRepository.Save(tarefa);
            return true;
        }

        // Deleta uma tarefa pelo ID
        public bool DeleteTarefa(int id)
        {
            var tarefa = _tarefaRepository.GetAll().FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
                return false;

            _tarefaRepository.Delete(id);
            return true;
        }

        // Obtém todas as tarefas
        public List<TarefaDTO> GetAllTarefas()
        {
            var tarefas = _tarefaRepository.GetAll();
            return tarefas.Select(t => new TarefaDTO
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Concluida = t.Concluida,
                DataCriacao = t.DataCriacao
            }).ToList();
        }
    }
}