using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrmArrighi.Data;
using CrmArrighi.Models;

namespace CrmArrighi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly CrmArrighiContext _context;

        public UsuarioController(CrmArrighiContext context)
        {
            _context = context;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios
                .Include(u => u.PessoaFisica)
                .Include(u => u.PessoaJuridica)
                .ToListAsync();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.PessoaFisica)
                .Include(u => u.PessoaJuridica)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // GET: api/Usuario/pessoas-fisicas
        [HttpGet("pessoas-fisicas")]
        public async Task<ActionResult<IEnumerable<object>>> GetPessoasFisicasParaUsuario()
        {
            var pessoasFisicas = await _context.PessoasFisicas
                .Select(p => new { p.Id, p.Nome, p.Cpf, p.Email })
                .ToListAsync();

            return pessoasFisicas;
        }

        // GET: api/Usuario/pessoas-juridicas
        [HttpGet("pessoas-juridicas")]
        public async Task<ActionResult<IEnumerable<object>>> GetPessoasJuridicasParaUsuario()
        {
            var pessoasJuridicas = await _context.PessoasJuridicas
                .Select(p => new { p.Id, p.RazaoSocial, p.NomeFantasia, p.Cnpj, p.Email })
                .ToListAsync();

            return pessoasJuridicas;
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            // Verificar se o login já existe
            var loginExistente = await _context.Usuarios
                .AnyAsync(u => u.Login == usuario.Login);
            if (loginExistente)
            {
                return BadRequest("Login já existe no sistema.");
            }

            // Verificar se o e-mail já existe
            var emailExistente = await _context.Usuarios
                .AnyAsync(u => u.Email == usuario.Email);
            if (emailExistente)
            {
                return BadRequest("E-mail já existe no sistema.");
            }

            // Validar relacionamento com pessoa
            if (usuario.TipoPessoa == "Fisica")
            {
                if (!usuario.PessoaFisicaId.HasValue)
                {
                    return BadRequest("Pessoa Física é obrigatória quando o tipo é 'Fisica'.");
                }

                var pessoaFisica = await _context.PessoasFisicas
                    .FindAsync(usuario.PessoaFisicaId.Value);
                if (pessoaFisica == null)
                {
                    return BadRequest("Pessoa Física não encontrada.");
                }

                usuario.PessoaJuridicaId = null;
            }
            else if (usuario.TipoPessoa == "Juridica")
            {
                if (!usuario.PessoaJuridicaId.HasValue)
                {
                    return BadRequest("Pessoa Jurídica é obrigatória quando o tipo é 'Juridica'.");
                }

                var pessoaJuridica = await _context.PessoasJuridicas
                    .FindAsync(usuario.PessoaJuridicaId.Value);
                if (pessoaJuridica == null)
                {
                    return BadRequest("Pessoa Jurídica não encontrada.");
                }

                usuario.PessoaFisicaId = null;
            }
            else
            {
                return BadRequest("Tipo de pessoa deve ser 'Fisica' ou 'Juridica'.");
            }

            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            // Verificar se o login já existe (exceto para o próprio usuário)
            var loginExistente = await _context.Usuarios
                .AnyAsync(u => u.Login == usuario.Login && u.Id != id);
            if (loginExistente)
            {
                return BadRequest("Login já existe no sistema.");
            }

            // Verificar se o e-mail já existe (exceto para o próprio usuário)
            var emailExistente = await _context.Usuarios
                .AnyAsync(u => u.Email == usuario.Email && u.Id != id);
            if (emailExistente)
            {
                return BadRequest("E-mail já existe no sistema.");
            }

            // Validar relacionamento com pessoa
            if (usuario.TipoPessoa == "Fisica")
            {
                if (!usuario.PessoaFisicaId.HasValue)
                {
                    return BadRequest("Pessoa Física é obrigatória quando o tipo é 'Fisica'.");
                }

                var pessoaFisica = await _context.PessoasFisicas
                    .FindAsync(usuario.PessoaFisicaId.Value);
                if (pessoaFisica == null)
                {
                    return BadRequest("Pessoa Física não encontrada.");
                }

                usuario.PessoaJuridicaId = null;
            }
            else if (usuario.TipoPessoa == "Juridica")
            {
                if (!usuario.PessoaJuridicaId.HasValue)
                {
                    return BadRequest("Pessoa Jurídica é obrigatória quando o tipo é 'Juridica'.");
                }

                var pessoaJuridica = await _context.PessoasJuridicas
                    .FindAsync(usuario.PessoaJuridicaId.Value);
                if (pessoaJuridica == null)
                {
                    return BadRequest("Pessoa Jurídica não encontrada.");
                }

                usuario.PessoaFisicaId = null;
            }
            else
            {
                return BadRequest("Tipo de pessoa deve ser 'Fisica' ou 'Juridica'.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    usuario.DataAtualizacao = DateTime.Now;
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
} 