using Newtonsoft.Json;

namespace TradeMeAPITests.DataEntities
{
    public class Make
    {
        [JsonProperty("Name")]
        public string MakeName { get; set; }
        [JsonProperty("Number")]
        public string Number { get; set; }
        [JsonProperty("Path")]
        public string Path { get; set; }
        [JsonProperty("Count")]
        public int Count { get; set; }
        [JsonProperty("HasClassifieds")]
        public bool HasClassifieds { get; set; }
        [JsonProperty("AreaOfBusiness")]
        public string AreaOfBusiness { get; set; }
        [JsonProperty("IsLeaf")]
        public bool IsLeaf { get; set; }
    }
}