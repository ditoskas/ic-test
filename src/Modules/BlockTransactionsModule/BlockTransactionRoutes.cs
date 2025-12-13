using BlockTransactionsModule.Features.GetChains;
using BlockTransactionsModule.Features.GetTransaction;
using Carter;
using IcTest.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BlockTransactionsModule
{
    public class BlockTransactionRoutes : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder builder)
        {
            builder.MapGet(
                BlockTransactionUrls.CryptoTransactionsHistory,
                async (
                    [FromRoute] string coinId,
                    [FromRoute] string chainId,
                    [FromQuery] int? page,
                    [FromQuery] int? pageSize,
                    ISender sender) =>
                {
                    int currentPage = page.GetValueOrDefault(PaginationDefaults.DefaultPageNumber);
                    int currentPageSize = pageSize.GetValueOrDefault(PaginationDefaults.DefaultPageSize);

                    return await sender.Send(new GetTransactionQuery(coinId, chainId, currentPage, currentPageSize));
                })
                .WithName("GetTransactionHistory")
                .Produces<GetTransactionResult>(StatusCodes.Status200OK)
                .WithSummary("Get transaction history")
                .WithDescription("Get transaction history");

            builder.MapGet(
                    BlockTransactionUrls.CryptoChains,
                    async (ISender sender) => await sender.Send(new GetChainsQuery()))
                .WithName("GetBlockChains")
                .Produces<GetChainsResult>(StatusCodes.Status200OK)
                .WithSummary("Get available block chains")
                .WithDescription("Get available block chains");
        }
    }
}
