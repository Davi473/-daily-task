using Microsoft.AspNetCore.Mvc;
using AuthApi.Application.DTOs;
using AuthApi.Application.Services;

namespace AuthApi.Web.Controllers
{
    // Controlador para endpoints de autenticação
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Serviço de autenticação injetado
        private readonly AuthService _authService;

        // Construtor com injeção de dependência
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // Endpoint para registro de usuário
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                // Chama o serviço para registrar o usuário
                _authService.Register(registerDTO);
                return Ok("Usuário registrado com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                // Retorna erro se o email já está registrado
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // Retorna erro para validações de entrada
                return BadRequest(ex.Message);
            }
        }

        // Endpoint para login de usuário
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                // Chama o serviço para realizar login e obter token
                var token = _authService.Login(loginDTO);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Retorna erro para credenciais inválidas
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                // Retorna erro para validações de entrada
                return BadRequest(ex.Message);
            }
        }
    }
}