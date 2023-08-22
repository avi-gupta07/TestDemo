using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestDemo.Models
{
    public class RoleViewModel
    {
       
        public string RoleId { get; set; }

        [Display(Name = "Role Name")]
        [Required]
        public string RoleName { get; set; }
       

    }
    //public class RegisterRoleViewModel
    //{
    //    [Display(Name ="Role Name")]
    //    [Required]
    //    public string RoleName { get; set; }
    //}
}