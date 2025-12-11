using System.Reflection;
using BlockCypher.Data.Models;
using IcTest.Data.Models;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace IcTest.Infrastructure.Extensions
{
    public static class MapsterConfig
    {
        public static void RegisterCustomMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<BlockCypherBlockHash, BlockHash>.NewConfig()
                .Map(dest => dest.Hash, src => src.Hash)
                .Map(dest => dest.Height, src => src.Height)
                .Map(dest => dest.Chain, src => src.Chain)
                .Map(dest => dest.Total, src => src.Total)
                .Map(dest => dest.Fees, src => src.Fees)
                .Map(dest => dest.Size, src => src.Size)
                .Map(dest => dest.Vsize, src => src.Vsize)
                .Map(dest => dest.Ver, src => src.Ver)
                .Map(dest => dest.Time, src => src.Time)
                .Map(dest => dest.ReceivedTime, src => src.ReceivedTime)
                .Map(dest => dest.CoinbaseAddr, src => src.CoinbaseAddr)
                .Map(dest => dest.RelayedBy, src => src.RelayedBy)
                .Map(dest => dest.Bits, src => src.Bits)
                .Map(dest => dest.Nonce, src => src.Nonce)
                .Map(dest => dest.NTx, src => src.NTx)
                .Map(dest => dest.PrevBlock, src => src.PrevBlock)
                .Map(
                    dest => dest.Txids,
                    src => src.Txids.Count == 0
                        ? null
                        : src.Txids.Select(txid => new BlockTransaction
                        {
                            Hash = txid
                        }).ToList()
                )
                .Map(dest => dest.Depth, src => src.Depth)
                .Map(dest => dest.PrevBlockUrl, src => src.PrevBlockUrl)
                .Map(dest => dest.TxUrl, src => src.TxUrl)
                .Map(dest => dest.NextTxids, src => src.NextTxids);


            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
