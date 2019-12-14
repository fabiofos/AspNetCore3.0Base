using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;


namespace AspNetCore3Base.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Field Required")]
        public string Name { get; set; }
        public int IsAppUser { get; set; }
        public int IsActive { get; set; }
        public string Pin { get; set; }
        public int IsDeleted { get; set; }
        public int? EnableAlerts { get; set; }
        public string CREATED_BY { get; set; }
        public string UPDATED_BY { get; set; }
        public DateTime CREATED_ON { get; set; }
        public DateTime? UPDATED_ON { get; set; }
    }
}
