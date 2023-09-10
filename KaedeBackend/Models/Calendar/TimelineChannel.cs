using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace KaedeBackend.Models.Calendar
{
    public class TimelineChannel
    {
        [JsonProperty("states")]
        public List<ChannelState> States { get; set; }

        [JsonProperty("cacheExpire")]
        public string CacheExpire { get; set; }
    }
}