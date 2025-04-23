using System;
using System.Collections.Generic;
using System.Linq;
using AuthApi.Domain.Entities;
using AuthApi.Domain.Interfaces;

namespace AuthApi.Infrastructure.Repositories
{
    // Implementação do repositório de tarefas em memória
    public class TarefaRepositoryMemory : ITarefaRepository
    {
        // Lista para armazenar tarefas
        private readonly List<Tarefa> _tarefas;

        // Construtor que inicializa a lista
        public TarefaRepositoryMemory()
        {
            _tarefas = new List<Tarefa>();
        }

        // Salva uma tarefa (nova ou atualizada)
        public void Save(Tarefa tarefa)
        {
            var existingTarefa = _tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
            if (existingTarefa != null)
            {
                existingTarefa.Titulo = tarefa.Titulo;
                existingTarefa.Descricao = tarefa.Descricao;
                existingTarefa.Concluida = tarefa.Concluida;
                existingTarefa.DataCriacao = tarefa.DataCriacao;
            }
            else
            {
                _tarefas.Add(tarefa);
            }
        }

        // Busca uma tarefa pelo título
        public Tarefa FindByTitulo(string titulo)
        {
            return _tarefas.FirstOrDefault(t => t.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        }

        // Retorna todas as tarefas
        public List<Tarefa> GetAll()
        {
            return _tarefas;
        }

        // Deleta uma tarefa pelo ID
        public void Delete(int id)
        {
            var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa != null)
            {
                _tarefas.Remove(tarefa);
            }
        }
    }
}