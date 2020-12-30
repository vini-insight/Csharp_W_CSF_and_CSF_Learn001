using System;
using System.ComponentModel.DataAnnotations;

namespace MyValidations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ValidarSexoAuxAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as string; 

            if(inputValue != null)
            {

                inputValue = inputValue.ToUpper();
                
                if(inputValue.Equals("F") || inputValue.Equals("M")) // só é válido se for qualquer uma dessas opções           
                {
                    return true;
                }
                else
                {            
                    // throw new Exception("Sexo inválido: use o formato F ou f ou M ou m. sexo não pode ser vazio");
                    return false;
                }
            }
            else return true; // se inputValue for igual a NULL retorna TRUE para passar na validação.
        }        
    }
}