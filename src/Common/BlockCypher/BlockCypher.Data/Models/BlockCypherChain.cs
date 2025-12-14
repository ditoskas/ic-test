using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlockCypher.Data.Models
{
    public class BlockCypherChain
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("hash")]
        public required string Hash { get; set; }
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
        [JsonPropertyName("latest_url")]
        public required string LatestUrl { get; set; }
        [JsonPropertyName("previous_hash")]
        public required string PreviousHash { get; set; }
        [JsonPropertyName("previous_url")]
        public required string PreviousUrl { get; set; }
        [JsonPropertyName("peer_count")]
        public int PeerCount { get; set; }
        [JsonPropertyName("unconfirmed_count")]
        public int UnconfirmedCount { get; set; }
        [JsonPropertyName("high_fee_per_kb")]
        public int HighFeePerKb { get; set; }
        [JsonPropertyName("medium_fee_per_kb")]
        public int MediumFeePerKb { get; set; }
        [JsonPropertyName("low_fee_per_kb")]
        public int LowFeePerKb { get; set; }
        [JsonPropertyName("last_fork_height")]
        public int? LastForkHeight { get; set; }
        [JsonPropertyName("last_fork_hash")]
        public string? LastForkHash { get; set; }
    }
}
