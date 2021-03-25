using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthATMProject.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DoB { get; set; }
        public string Team { get; set; }
        public decimal Salary { get; set; }
        public int GradeId { get; set; }
        public virtual Grade Customer { get; set; }
    }
}