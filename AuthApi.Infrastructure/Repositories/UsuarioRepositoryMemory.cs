using System;
using System.Collections.Generic;
using System.Linq;
using AuthApi.Domain.Entities;
using AuthApi.Domain.Interfaces;

namespace AuthApi.Infrastructure.Repositories
{
    // Implementação do repositório de usuários em memória
    public class UsuarioRepositoryMemory : IUsuarioRepository
    {
        // Lista para armazenar usuários (coleção dinâmica)
        private readonly List<Usuario> _usuarios;

        // Construtor que inicializa a lista
        public UsuarioRepositoryMemory()
        {
            _usuarios = new List<Usuario>();
        }

        // Método para salvar um usuário (novo ou atualizado)
        public void Save(Usuario usuario)
        {
            // Busca usuário existente pelo ID
            var existingUsuario = _usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            if (existingUsuario != null)
            {
                // Atualiza os dados do usuário existente
                existingUsuario.Email = usuario.Email;
                existingUsuario.PasswordHash = usuario.PasswordHash;
            }
            else
            {
                // Adiciona novo usuário à lista
                _usuarios.Add(usuario);
            }
        }

        // Método para buscar um usuário pelo email
        public Usuario FindByEmail(string email)
        {
            // Retorna o primeiro usuário com o email correspondente (ignora maiúsculas/minúsculas)
            return _usuarios.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}