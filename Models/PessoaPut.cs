using System.ComponentModel.DataAnnotations;
using MyValidations;

public class PessoaPut
{
    [ValidarNome(ErrorMessage = "NOME INVALIDO")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório", AllowEmptyStrings = false), StringLength(11, MinimumLength = 11)]
    [ValidarCpf(ErrorMessage = "CPF INVALIDO")]
    public string Cpf { get; set; }

    [ValidarData(ErrorMessage = "DATA INVALIDA")]
    public string DataNascimento { get; set; }
    
    [ValidarSexo(ErrorMessage = "SEXO INVALIDO")]
    public string Sexo { get; set; }
}
