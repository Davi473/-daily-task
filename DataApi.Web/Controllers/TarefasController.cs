using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthApi.Application.DTOs;
using AuthApi.Application.Services;

namespace DataApi.Web.Controllers
{
    // Controlador para gerenciar tarefas, protegido por autenticação JWT
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        // Serviço de tarefas injetado
        private readonly TarefaService _tarefaService;

        // Construtor com injeção de dependência
        public TarefasController(TarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        // Cria uma nova tarefa
        [HttpPost]
        public IActionResult Create([FromBody] TarefaDTO tarefaDTO)
        {
            try
            {
                _tarefaService.CreateTarefa(tarefaDTO);
                return CreatedAtAction(nameof(GetByTitulo), new { titulo = tarefaDTO.Titulo }, tarefaDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Obtém uma tarefa pelo título
        [HttpGet("{titulo}")]
        public IActionResult GetByTitulo(string titulo)
        {
            var tarefa = _tarefaService.GetTarefaByTitulo(titulo);
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        // Atualiza uma tarefa existente
        [HttpPut("{titulo}")]
        public IActionResult Update(string titulo, [FromBody] TarefaDTO tarefaDTO)
        {
            tarefaDTO.Titulo = titulo; // Garante que o título não seja alterado
            var success = _tarefaService.UpdateTarefa(tarefaDTO);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // Deleta uma tarefa pelo ID
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _tarefaService.DeleteTarefa(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // Obtém todas as tarefas
        [HttpGet]
        public IActionResult GetAll()
        {
            var tarefas = _tarefaService.GetAllTarefas();
            return Ok(tarefas);
        }
    }
}