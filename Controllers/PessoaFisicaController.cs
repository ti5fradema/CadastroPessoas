using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrmArrighi.Data;
using CrmArrighi.Models;

namespace CrmArrighi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaFisicaController : ControllerBase
    {
        private readonly CrmArrighiContext _context;

        public PessoaFisicaController(CrmArrighiContext context)
        {
            _context = context;
        }

        // GET: api/PessoaFisica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaFisica>>> GetPessoasFisicas()
        {
            return await _context.PessoasFisicas.Include(p => p.Endereco).ToListAsync();
        }

        // GET: api/PessoaFisica/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaFisica>> GetPessoaFisica(int id)
        {
            var pessoaFisica = await _context.PessoasFisicas
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pessoaFisica == null)
            {
                return NotFound();
            }

            return pessoaFisica;
        }

        // GET: api/PessoaFisica/responsaveis-tecnicos
        [HttpGet("responsaveis-tecnicos")]
        public async Task<ActionResult<IEnumerable<object>>> GetResponsaveisTecnicos()
        {
            var responsaveis = await _context.PessoasFisicas
                .Include(p => p.Endereco)
                .Select(p => new {
                    p.Id,
                    p.Nome,
                    p.Cpf,
                    p.Email,
                    p.Sexo,
                    p.DataNascimento,
                    p.EstadoCivil,
                    p.Telefone1,
                    p.Telefone2,
                    p.EnderecoId,
                    Endereco = new {
                        p.Endereco.Id,
                        p.Endereco.Cidade,
                        p.Endereco.Bairro,
                        p.Endereco.Logradouro,
                        p.Endereco.Cep,
                        p.Endereco.Numero,
                        p.Endereco.Complemento
                    }
                })
                .ToListAsync();

            return responsaveis;
        }

        // POST: api/PessoaFisica
        [HttpPost]
        public async Task<ActionResult<PessoaFisica>> PostPessoaFisica(PessoaFisica pessoaFisica)
        {
            if (ModelState.IsValid)
            {
                _context.PessoasFisicas.Add(pessoaFisica);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPessoaFisica), new { id = pessoaFisica.Id }, pessoaFisica);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/PessoaFisica/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoaFisica(int id, PessoaFisica pessoaFisica)
        {
            if (id != pessoaFisica.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pessoaFisica.DataAtualizacao = DateTime.Now;
                    _context.Update(pessoaFisica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaFisicaExists(pessoaFisica.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/PessoaFisica/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoaFisica(int id)
        {
            var pessoaFisica = await _context.PessoasFisicas.FindAsync(id);
            if (pessoaFisica == null)
            {
                return NotFound();
            }

            // Verificar se a pessoa física é responsável técnico de alguma pessoa jurídica
            var isResponsavelTecnico = await _context.PessoasJuridicas
                .AnyAsync(pj => pj.ResponsavelTecnicoId == id);

            if (isResponsavelTecnico)
            {
                return BadRequest("Não é possível excluir esta pessoa física pois ela é responsável técnico de uma ou mais pessoas jurídicas.");
            }

            _context.PessoasFisicas.Remove(pessoaFisica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaFisicaExists(int id)
        {
            return _context.PessoasFisicas.Any(e => e.Id == id);
        }
    }
}
