using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using MyValidations;

//http://zetcode.com/csharp/mysql/
// on command line type it:
// $ dotnet add package MySql.Data
// for access MySql ADO.NET framerwork. Include the package to our .NET Core project.

public class PessoaPut
{    
    // public string Nome { get; set; } = "Zerezima";
    // public string Cpf { get; set; } = "00000000000";
    // public string DataNascimento { get; set; } = "17/09/1988";
    // public char Sexo { get; set; } = 'M';

    // [StringLength(100)]
    // [ValidarNome(ErrorMessage = "NOME INVALIDO")]
    [ValidarNomeAux(ErrorMessage = "NOME INVALIDO")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório", AllowEmptyStrings = false), StringLength(11, MinimumLength = 11)]
    [ValidarCpfAux(ErrorMessage = "CPF INVALIDO")]
    public string Cpf { get; set; }

    // [DataType(DataType.Date)]
    [ValidarDataAux(ErrorMessage = "DATA INVALIDA")]
    // [Required(AllowEmptyStrings = false), DataType(DataType.Date)]
    // [ValidarDataAux()]
    public string DataNascimento { get; set; }
    
    // public char Sexo { get; set; }

    // [StringLength(1)]
    [ValidarSexoAux(ErrorMessage = "SEXO INVALIDO")]
    public string Sexo { get; set; }
}
