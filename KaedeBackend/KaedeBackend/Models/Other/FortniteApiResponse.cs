using Newtonsoft.Json;

namespace KaedeBackend.Models.Other
{
    public class FortniteAPIResponse<T>
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }

    public class Cosmetic
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rarity")]
        public Type Rarity { get; set; }

        [JsonProperty("series")]
        public Series Series { get; set; }

        [JsonProperty("set")]
        public Set Set { get; set; }

        [JsonProperty("images")]
        public Images Images { get; set; }

        [JsonProperty("variants")]
        public List<Variant> Variants { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("definitionPath")]
        public string DefinitionPath { get; set; }

        [JsonProperty("type")]
        public Type Type { get; set; }
    }

    public class Variant
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }
    }

    public class Option
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public class Type
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("displayValue")]
        public string DisplayValue { get; set; }

        [JsonProperty("backendValue")]
        public string BackendValue { get; set; }
    }

    public class Series
    {
        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("backendValue")]
        public string BackendValue { get; set; }
    }

    public class Set
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("backendValue")]
        public string BackendValue { get; set; }
    }

    public class Images
    {
        [JsonProperty("smallIcon")]
        public string SmallIcon { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("featured")]
        public string Featured { get; set; }
    }

    public class AES
    {
        [JsonProperty("build")]
        public string Build { get; set; }

        [JsonProperty("mainKey")]
        public string MainKey { get; set; }

        [JsonProperty("dynamicKeys")]
        public List<DynamicKey> DynamicKeys { get; set; }
    }

    public class DynamicKey
    {
        [JsonProperty("pakFilename")]
        public string PakFilename { get; set; }

        [JsonProperty("pakGuid")]
        public string PakGuid { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }
    }

    public class Banner
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
