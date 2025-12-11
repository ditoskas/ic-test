using IcTest.Shared.CQRS;

namespace BlockTransactionsModule.Features.GetTransaction
{
    public class GetTransactionHandler : IQueryHandler<GetTransactionQuery, GetTransactionResult>
    {
        public async Task<GetTransactionResult> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            return new GetTransactionResult(null);
        }
    }
}
