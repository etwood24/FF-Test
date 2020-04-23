using Newtonsoft.Json;
using System.Collections.Generic;

namespace TradeMeAPITests.DataEntities
{
    public class CarsResponse
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Number")]
        public string Number { get; set; }
        [JsonProperty("Path")]
        public string Path { get; set; }
        [JsonProperty("Subcategories")]
        public List<Make> Makes { get; set; }
        [JsonProperty("Count")]
        public int Count { get; set; }
        [JsonProperty("HasClassifieds")]
        public bool HasClassifieds { get; set; }
        [JsonProperty("AreaOfBusiness")]
        public int AreaOfBusiness { get; set; }
        [JsonProperty("IsLeaf")]
        public bool IsLeaf { get; set; }
    }
}
