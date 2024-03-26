using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using System;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Entities.BizEntities
{
    public partial class EmployeeBizEntity
    {
        public long EmployeeId { get; set; }
        public int EmployeeNumber { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }
        public DateTime DateJoined { get; set; }
        public short? Extension { get; set; }
        public int? RoleId { get; set; }
        [JsonProperty("RoleName")]
        [DisplayName("Role Name")]
        public string RoleName { get; set; }

        public RoleBizEntity Role { get; set; }
    }
}
