using System;
using System.IdentityModel.Tokens.Jwt; // Para JwtSecurityTokenHandler
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens; // Para SymmetricSecurityKey, SigningCredentials
using Microsoft.Extensions.Configuration; // Para IConfiguration
using BCrypt.Net;
using AuthApi.Domain.Entities;
using AuthApi.Domain.Interfaces;
using AuthApi.Application.DTOs;

namespace AuthApi.Application.Services
{
    // Serviço que gerencia registro e login de usuários
    public class AuthService
    {
        // Repositório de usuários
        private readonly IUsuarioRepository _usuarioRepository;
        
        // Chave secreta para assinatura do JWT
        private readonly string _jwtSecret;
        
        // Tempo de expiração do token em minutos
        private readonly int _jwtExpirationInMinutes;

        // Construtor com injeção de dependências
        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _jwtSecret = configuration["Jwt:Secret"];
            _jwtExpirationInMinutes = int.Parse(configuration["Jwt:ExpirationInMinutes"]); // Lê 21600 (15 dias)
        }

        // Método para registrar um novo usuário
        public void Register(RegisterDTO registerDTO)
        {
            // Verifica se o email já está registrado
            var existingUser = _usuarioRepository.FindByEmail(registerDTO.Email);
            if (existingUser != null)
                throw new InvalidOperationException("Email já registrado.");

            // Gera o hash da senha usando BCrypt
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);
            
            // Cria um novo usuário com ID gerado
            var usuario = new Usuario(GenerateId(), registerDTO.Email, passwordHash);
            
            // Salva o usuário no repositório
            _usuarioRepository.Save(usuario);
        }

        // Método para realizar login e retornar um token JWT
        public TokenDTO Login(LoginDTO loginDTO)
        {
            // Busca o usuário pelo email
            var usuario = _usuarioRepository.FindByEmail(loginDTO.Email);
            
            // Verifica se o usuário existe e se a senha está correta
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password, usuario.PasswordHash))
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            // Gera o token JWT
            var token = GenerateJwtToken(usuario);
            
            // Retorna o token e sua data de expiração
            return new TokenDTO
            {
                Token = token,
                Expires = DateTime.UtcNow.AddMinutes(_jwtExpirationInMinutes) // 15 dias
            };
        }

        // Método privado para gerar o token JWT
        private string GenerateJwtToken(Usuario usuario)
        {
            // Define as claims do token (informações do usuário)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
            };

            // Cria a chave de assinatura com base no segredo
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Cria o token JWT
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtExpirationInMinutes), // 15 dias
                signingCredentials: creds);

            // Serializa o token para string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Método privado para gerar IDs incrementais (simulação em memória)
        private int GenerateId()
        {
            // Retorna 1 se não houver usuários, ou o próximo ID
            return _usuarioRepository.FindByEmail("any") == null ? 1 : _usuarioRepository.FindByEmail("any").Id + 1;
        }
    }
}