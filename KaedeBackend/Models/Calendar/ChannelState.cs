﻿using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using KaedeBackend.Models.Calendar.States;

namespace KaedeBackend.Models.Calendar
{
    public class ChannelState
    {
        [JsonProperty("validFrom")]
        public string ValidFrom { get; set; }

        [JsonProperty("activeEvents")]
        public List<ChannelEvent> ActiveEvents { get; set; }

        [JsonProperty("state")]
        public ClientEventsState State { get; set; }
    }
}