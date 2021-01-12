using System.ComponentModel.DataAnnotations;
using Models;
using MyValidations;

public class Pessoa : RespostaHttp
{
    [Required(ErrorMessage = "O nome completo é obrigatório", AllowEmptyStrings = false), StringLength(100)]
    [ValidarNome(ErrorMessage = "NOME INVALIDO")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório", AllowEmptyStrings = false), StringLength(11, MinimumLength = 11)]
    [ValidarCpf(ErrorMessage = "CPF INVALIDO")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória", AllowEmptyStrings = false), DataType(DataType.Date)]
    [ValidarData(ErrorMessage = "DATA INVALIDA")]
    public string DataNascimento { get; set; }

    [Required(ErrorMessage = "Sexo é obrigatório", AllowEmptyStrings = false), StringLength(1, MinimumLength = 1)]
    [ValidarSexo(ErrorMessage = "SEXO INVALIDO")]
    public string Sexo { get; set; }
}
