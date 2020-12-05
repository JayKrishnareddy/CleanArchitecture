using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.ViewModels
{
   public class CommonModel
    {
        public class UserModel
        {
            [JsonProperty(PropertyName = "firstname")]
            [Required]
            public string FirstName { get; set; }
            [JsonProperty(PropertyName = "lastname")]
            [Required]
            public string LastName { get; set; }
            [JsonProperty(PropertyName = "phonenumber")]
            [Required]
            public long PhoneNumber { get; set; }
            [JsonProperty(PropertyName = "password")]
            [Required]
            public string Password { get; set; }
           [JsonProperty(PropertyName = "email")]
            [Required]
            public string Email { get; set; }
            [JsonProperty(PropertyName = "rolename")]
            [Required]
            public string RoleName { get; set; }
        }

        public class RolesModel
        {
            [JsonProperty(PropertyName = "role_id")]
            public int RoleId { get; set; }
            [JsonProperty(PropertyName = "role_name")]
            public string RoleName { get; set; }
        }
        public class LoginModel
        {
            [JsonProperty(PropertyName = "username")]
            [Required]
            public string UserName { get; set; }
            [JsonProperty(PropertyName = "password")]
            [Required]
            public string Password { get; set; }
        }
        
    }
}
