using BlockTransactionsModule.Features.GetTransaction;
using Carter;
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
            builder.MapGet(BlockTransactionUrls.BlockTransactionsHistory,
                async ([FromRoute] string coinId, [FromRoute] string chainId, ISender sender) =>
                {
                    return await sender.Send(new GetTransactionQuery(coinId, null, 1, 10));
                })
                .WithName("GetTransactionHistory")
                .Produces<GetTransactionResult>(StatusCodes.Status200OK)
                .WithSummary("Get transaction history")
                .WithDescription("Get transaction history");
        }
    }
}
