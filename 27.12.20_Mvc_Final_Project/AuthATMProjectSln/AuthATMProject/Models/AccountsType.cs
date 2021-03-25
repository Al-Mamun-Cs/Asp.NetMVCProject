using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthATMProject.Models
{
    public class AccountsType
    {
        public AccountsType()
        {
            this.Clients = new HashSet<Client>();
        }
        public int AccountsTypeId { get; set; }
        public string AccountsTitle { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}