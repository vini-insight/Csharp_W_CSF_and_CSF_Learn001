using System;
using System.ComponentModel.DataAnnotations;

namespace MyValidations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ValidarSexoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var inputValue = value as string; 

            if(inputValue != null)
            {

                inputValue = inputValue.ToUpper();
                
                if(inputValue.Equals("F") || inputValue.Equals("M"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return true;
        }        
    }
}