﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore3Base.Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        [Required(ErrorMessage = "Field Required")]
        public string Description { get; set; }
    }
}