using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TaxService.Models.TaxJar
{
    public class TaxJarCalculateTaxResponse_Model
    {
        [JsonProperty("tax")]
        public Tax Tax { get; set; }
    }
}
