using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.Json.Serialization;

namespace BlockCypher.Data.Models
{
    public class BlockCypherBlockHash
    {
        [JsonPropertyName("hash")]
        public required string Hash { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("chain")]
        public required string Chain { get; set; }
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        [JsonPropertyName("fees")]
        public long Fees { get; set; }
        [JsonPropertyName("size")]
        public int? Size { get; set; }
        [JsonPropertyName("vsize")]
        public int? Vsize { get; set; }
        [JsonPropertyName("ver")]
        public int Ver { get; set; }
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
        [JsonPropertyName("received_time")]
        public DateTime ReceivedTime { get; set; }
        [JsonPropertyName("coinbase_addr")]
        public string CoinbaseAddr { get; set; } = string.Empty;
        [JsonPropertyName("relayed_by")]
        public string RelayedBy { get; set; } = string.Empty;
        [JsonPropertyName("bits")]
        public long Bits { get; set; }
        [JsonPropertyName("nonce")]
        public long Nonce { get; set; }
        [JsonPropertyName("n_tx")]
        public int NTx { get; set; }
        [JsonPropertyName("prev_block")]
        public string PrevBlock { get; set; } = string.Empty;
        [JsonPropertyName("mrkl_root")]
        public string MrklRoot { get; set; } = string.Empty;
        [JsonPropertyName("txids")]
        public List<string> Txids { get; set; } = new List<string>();
        [JsonPropertyName("depth")]
        public int Depth { get; set; }
        [JsonPropertyName("prev_block_url")]
        public string PrevBlockUrl { get; set; } = string.Empty;
        [JsonPropertyName("tx_url")]
        public string TxUrl { get; set; } = string.Empty;
        [JsonPropertyName("next_txids")]
        public string? NextTxids { get; set; }
    }
}
