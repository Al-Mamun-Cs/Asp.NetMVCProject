using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthATMProject.Models
{
    public class Grade
    {
        public Grade()
        {
            this.Players = new HashSet<Player>();
        }
        [Key]
        public int GradeId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}