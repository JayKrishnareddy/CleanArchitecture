using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
   public class User : BaseModel
    {
        [JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "phone_number")]
        public long PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "role_id")]
        public int RoleId { get; set; }
        public virtual UserRoles UserRoles { get; set; }
    }
}
