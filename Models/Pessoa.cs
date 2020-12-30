using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MyValidations;

//http://zetcode.com/csharp/mysql/
// on command line type it:
// $ dotnet add package MySql.Data
// for access MySql ADO.NET framerwork. Include the package to our .NET Core project.

public class Pessoa
{    
    // public string Nome { get; set; } = "Zerezima";
    // public string Cpf { get; set; } = "00000000000";
    // public string DataNascimento { get; set; } = "17/09/1988";
    // public char Sexo { get; set; } = 'M';

    [Required(ErrorMessage = "O nome completo é obrigatório", AllowEmptyStrings = false), StringLength(100)]
    [ValidarNome(ErrorMessage = "NOME INVALIDO")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório", AllowEmptyStrings = false), StringLength(11, MinimumLength = 11)]
    [ValidarCpf(ErrorMessage = "CPF INVALIDO")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória", AllowEmptyStrings = false), DataType(DataType.Date)]
    [ValidarData(ErrorMessage = "DATA INVALIDA")]
    // [Required(AllowEmptyStrings = false), DataType(DataType.Date)]
    // [ValidarData()]
    public string DataNascimento { get; set; }

    [Required(ErrorMessage = "Sexo é obrigatório", AllowEmptyStrings = false), StringLength(1, MinimumLength = 1)]
    [ValidarSexo(ErrorMessage = "SEXO INVALIDO")]
    public string Sexo { get; set; }
}
