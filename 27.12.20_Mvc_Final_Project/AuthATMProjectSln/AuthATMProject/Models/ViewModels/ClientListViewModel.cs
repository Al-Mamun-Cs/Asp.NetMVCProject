using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthATMProject.Models.ViewModels
{
    public class ClientListViewModel
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public bool IsActive { get; set; }
        public string ImageName { get; set; }
        public string ImageURL { get; set; }
        public int AccountsTypeId { get; set; }
        public string AccountsTitle { get; set; }
        public virtual AccountsType Account { get; set; }
    }
}