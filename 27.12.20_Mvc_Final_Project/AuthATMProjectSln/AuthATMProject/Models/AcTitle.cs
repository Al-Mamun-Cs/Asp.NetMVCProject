using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthATMProject.Models
{
    public class AcTitle
    {
        public AcTitle()
        {
            this.AcHolders = new HashSet<AcHolder>();
        }
        [Key]
        public int AcTitleId { get; set; }
        public string AccountType { get; set; }
        public virtual ICollection<AcHolder> AcHolders { get; set; }
    }
}