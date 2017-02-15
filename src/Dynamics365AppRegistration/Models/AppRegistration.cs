using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dynamics365AppRegistration.Models
{
    
    public class AppRegistration
    {
        public int id { get; set; }

        [Required]
        [Display(Name ="Tenant Id")]
        public string TenantId { get; set; }

        [Required]
        [Display(Name ="App Id")]
        public string AppId { get; set; }

        [Required]
        [Display(Name ="Dynamics 365 Company Name")]
        public string D365CompanyName { get; set; }

        [Required]
        [Display(Name ="Company Name")]
        public string CompanyName { get; set; }

        [Display(Name ="Company Address")]
        public string CompanyAddress { get; set; }

        [Display(Name = "Company Postcode")]
        public string CompanyPostcode { get; set; }

        [Display(Name = "Company City")]
        public string CompanyCity { get; set; }

        [Display(Name = "Company Country")]
        public string CompanyCountry { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Contact Email")]
        public string ContactEmail { get; set; }

        [Display(Name ="Contact Phone No.")]
        public string ContactPhoneNo { get; set; }

        [Required]
        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        
        public DateTime RegistrationDate { get; set; }

        [Required]
        [Range(1,Int16.MaxValue)]        
        [Display(Name = "Number of registered users")]
        public int NumberRegisteredUsers { get; set; }

        public AccessLevel AccessLevel { get; set; }
    }
}
