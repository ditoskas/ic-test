using FluentValidation;

namespace BlockTransactionsModule.Features.GetTransaction
{
    public class GetTransactionValidator : AbstractValidator<GetTransactionQuery>
    {
        public GetTransactionValidator()
        {
            RuleFor(x => x.CoinId).NotEmpty();
            RuleFor(x => x.ChainId).NotEmpty();
        }
    }
}
