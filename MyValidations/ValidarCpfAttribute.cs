using System;
using System.ComponentModel.DataAnnotations;

namespace MyValidations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ValidarCpfAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as string;

            if(inputValue != null)
            {
                if(inputValue.Length == 11)
                {
                    char[] chars = inputValue.ToCharArray();
                    for (int i = 0; i < chars.Length; i++)
                    {
                        if (!char.IsDigit(chars[i]))
                        {
                            // throw new Exception("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");
                            return false;
                        }
                    }
                    return true;
                }
                else
                {            
                    // throw new Exception("CPF só tem 11 dígitos. você digitou mais (ou menos) do que 11");
                    return false;
                }
            }
            else return true; // se inputValue for igual a NULL retorna TRUE para passar na validação
        }        
    }
}