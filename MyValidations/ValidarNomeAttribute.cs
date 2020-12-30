using System;
using System.ComponentModel.DataAnnotations;

namespace MyValidations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ValidarNomeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as string;

            if(inputValue.Contains(" ")) // se um Nome tem espaços é porque tem pelo menos duas palavras na string
            {                    
                return true;
            }
            else
            {
                // throw new Exception("digite o Nome completo com espaços entre eles");
                return false;
            }
        }        
    }
}