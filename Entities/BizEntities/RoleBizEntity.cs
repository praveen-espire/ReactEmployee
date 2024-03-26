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
    public partial class RoleBizEntity : BaseEntity
    {

        [JsonProperty("RoleId")]
        public int RoleId { get; set; }

        [JsonProperty("RoleName")]
        [DisplayName("Role Name")]
        [Required(ErrorMessage = "Role Name is required.")]
        public string RoleName { get; set; }

        public ICollection<EmployeeBizEntity> Employees { get; set; }

    }
}
