using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthATMProject.Models.ViewModels
{
    public class CreateClientViewModel
    {
        public int ClientId { get; set; }
        [Required(ErrorMessage = "Client Name Required*")]
        [DataType(DataType.Text)]
        [Display(Name = "Client Name")]
        [StringLength(40, ErrorMessage = "Client Name Maximum 40 character*")]
        public string ClientName { get; set; }
        [CustomDateOfTime(ErrorMessage="Date of Birth must be less than or equal to Today's Date")]
        [Required(ErrorMessage = "Enter your Date of Birth")]
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please enter your Email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your Phone Number")]
        public int Phone { get; set; }
        public bool IsActive { get; set; }
        public string ImageName { get; set; }
        public string ImageURL { get; set; }
        public int AccountsTypeId { get; set; }
        public string AccountsTitle { get; set; }
        public virtual AccountsType Account { get; set; }
        public List<AccountsType> AcList { get; set; }
        public IEnumerable<SelectListItem> AccountList
        {
            get { return new SelectList(AcList, "Id", "AccountsTitle"); }
        }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}