using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TaxServiceProvider.TaxJar.Models
{
    public class TaxJarCalculateTaxResponse_Model
    {
        [JsonProperty("tax")]
        public Tax Tax { get; set; }
    }
}
