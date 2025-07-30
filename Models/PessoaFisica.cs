using System.ComponentModel.DataAnnotations;

namespace CrmArrighi.Models
{
    public class PessoaFisica
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(200, ErrorMessage = "O nome deve ter no máximo 200 caracteres")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O e-mail é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [StringLength(150, ErrorMessage = "O e-mail deve ter no máximo 150 caracteres")]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(100, ErrorMessage = "O codinome deve ter no máximo 100 caracteres")]
        public string? Codinome { get; set; }
        
        [Required(ErrorMessage = "O sexo é obrigatório")]
        public string Sexo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        
        [Required(ErrorMessage = "O estado civil é obrigatório")]
        public string EstadoCivil { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O CPF é obrigatório")]
        [StringLength(14, ErrorMessage = "O CPF deve ter 14 caracteres")]
        public string Cpf { get; set; } = string.Empty;
        
        [StringLength(20, ErrorMessage = "O RG deve ter no máximo 20 caracteres")]
        public string? Rg { get; set; }
        
        [StringLength(20, ErrorMessage = "A CNH deve ter no máximo 20 caracteres")]
        public string? Cnh { get; set; }
        
        [Required(ErrorMessage = "O telefone principal é obrigatório")]
        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres")]
        public string Telefone1 { get; set; } = string.Empty;
        
        [StringLength(15, ErrorMessage = "O telefone deve ter no máximo 15 caracteres")]
        public string? Telefone2 { get; set; }
        
        // Relacionamento com Endereço
        public int EnderecoId { get; set; }
        public Endereco Endereco { get; set; } = null!;
        
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataAtualizacao { get; set; }
    }
} 