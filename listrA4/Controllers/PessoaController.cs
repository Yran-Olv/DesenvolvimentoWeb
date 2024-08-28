using Microsoft.AspNetCore.Mvc;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private static List<Pessoa> pessoas = new List<Pessoa>();

        [HttpPost]
        public IActionResult AdicionarPessoa([FromBody] Pessoa pessoa)
        {
            if (pessoas.Any(p => p.Cpf == pessoa.Cpf))
            {
                return BadRequest("Já existe uma pessoa com este CPF.");
            }

            pessoas.Add(pessoa);
            return CreatedAtAction(nameof(BuscarPessoaPorCpf), new { cpf = pessoa.Cpf }, pessoa);
        }

        [HttpPut("{cpf}")]
        public IActionResult AtualizarPessoa(string cpf, [FromBody] Pessoa pessoaAtualizada)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada.");
            }

            pessoa.Nome = pessoaAtualizada.Nome;
            pessoa.Peso = pessoaAtualizada.Peso;
            pessoa.Altura = pessoaAtualizada.Altura;

            return NoContent();
        }

        [HttpDelete("{cpf}")]
        public IActionResult RemoverPessoa(string cpf)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada.");
            }

            pessoas.Remove(pessoa);
            return NoContent();
        }

        [HttpGet]
        public IActionResult BuscarTodasAsPessoas()
        {
            return Ok(pessoas);
        }

        [HttpGet("{cpf}")]
        public IActionResult BuscarPessoaPorCpf(string cpf)
        {
            var pessoa = pessoas.FirstOrDefault(p => p.Cpf == cpf);
            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada.");
            }

            return Ok(pessoa);
        }

        [HttpGet("imc-bom")]
        public IActionResult BuscarPessoasComIMCBom()
        {
            var pessoasComIMCBom = pessoas.Where(p => p.CalcularIMC() >= 18 && p.CalcularIMC() <= 24).ToList();
            return Ok(pessoasComIMCBom);
        }

        [HttpGet("nome/{nome}")]
        public IActionResult BuscarPessoaPorNome(string nome)
        {
            var pessoasEncontradas = pessoas.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
            return Ok(pessoasEncontradas);
        }
    }
}
