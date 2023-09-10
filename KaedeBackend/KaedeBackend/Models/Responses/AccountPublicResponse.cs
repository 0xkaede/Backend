﻿using Newtonsoft.Json;

namespace KaedeBackend.Models.Responses
{
    public class AccountPublicResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("externalAuths")]
        public List<string> ExternalAuths { get; set; } = new List<string>();
    }
}
