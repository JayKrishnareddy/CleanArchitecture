using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
   public class Branches : BaseModel
    {
        [JsonProperty(PropertyName = "branch_id")]
        public int BranchId { get; set; }
        [JsonProperty(PropertyName = "branch_name")]
        public string BranchName { get; set; }
        [JsonProperty(PropertyName = "branch_manager")]
        public string BranchManager { get; set; }
        [JsonProperty(PropertyName = "branch_number")]
        public long BranchNumber { get; set; }
        [JsonProperty(PropertyName = "branch_location")]
        public string BranchLocation { get; set; }
    }
}
