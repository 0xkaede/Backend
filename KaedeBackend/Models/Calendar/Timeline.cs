using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using KaedeBackend.Models.Calendar;

namespace FortniteDotNet.Models.FortniteService.Calendar
{
    public class Timeline
    {
        [JsonProperty("channels")]
        public Dictionary<string, TimelineChannel> Channels { get; set; }

        [JsonProperty("currentTime")]
        public string CurrentTime { get; set; }

        [JsonProperty("cacheIntervalMins")]
        public double CacheIntervalMinutes { get; set; }
    }
}