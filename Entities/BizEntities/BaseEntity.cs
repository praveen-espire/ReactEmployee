using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.BizEntities
{
    public class BaseEntity
    {

        [JsonProperty("IsActive")]
        public bool IsActive { get; set; }

        [DisplayName("Reported On")]
        [JsonProperty("CreatedOn")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Required]
        [JsonProperty("CreatedBy")]
        public string CreatedBy { get; set; }
        [DisplayName("Resolved On")]
        [JsonProperty("UpdatedOn")]
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        [JsonProperty("UpdatedBy")]
        public string UpdatedBy { get; set; }

    }
}
