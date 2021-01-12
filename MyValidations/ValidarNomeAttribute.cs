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

            if(inputValue != null)
            {
                if(inputValue.Contains(" ")) // se um Nome tem espaços é porque tem pelo menos duas palavras na string
                {                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return true; // se inputValue for igual a NULL retorna TRUE para passar na validação
        }        
    }
}