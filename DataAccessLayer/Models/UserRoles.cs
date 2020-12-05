using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
   public class UserRoles
    {
        [JsonProperty(PropertyName = "role_id")]
        public int RoleId { get; set; }
        [JsonProperty(PropertyName = "role_name")]
        public string RoleName { get; set; }
        [JsonProperty(PropertyName = "is_active")]
        public bool IsActive { get; set; }
        public virtual User User { get; set; }
    }
}
