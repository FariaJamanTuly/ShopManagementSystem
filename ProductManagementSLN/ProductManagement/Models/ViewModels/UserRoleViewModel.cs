using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagement.Models.ViewModels
{
    public class UserRoleViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public int UserId { get; set; }
        public string UseName { get; set; }
    }
}