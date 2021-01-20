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
                            return false;
                        }
                    }
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