using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthATMProject.Models
{
    public class AcHolder
    {
        [Key]
        public int AcHolderId { get; set; }
        public string AcHolderName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DoB { get; set; }
        public int Amount { get; set; }
        public int AcTitleId { get; set; }
        public virtual AcTitle acTitle { get; set; }
    }
}