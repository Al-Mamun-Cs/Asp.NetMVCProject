using System;
using System.ComponentModel.DataAnnotations;

namespace AuthATMProject.Models.ViewModels
{
    internal class CustomDateOfTimeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime <= DateTime.Now;
        }
    }
}