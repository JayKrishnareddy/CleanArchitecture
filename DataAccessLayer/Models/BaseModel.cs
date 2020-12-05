using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
   public class BaseModel
    {
        [JsonProperty(PropertyName = "is_active")]  
        public bool IsActive { get; set; }
        [JsonProperty(PropertyName = "created_date")]
        public DateTime CreatedDate { get; set; }
        [JsonProperty(PropertyName = "modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
