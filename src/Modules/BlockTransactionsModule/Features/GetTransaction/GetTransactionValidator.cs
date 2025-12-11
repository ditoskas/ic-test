using FluentValidation;

namespace BlockTransactionsModule.Features.GetTransaction
{
    public class GetTransactionValidator : AbstractValidator<GetTransactionQuery>
    {
        public GetTransactionValidator()
        {
            RuleFor(x => x.CoinId).NotEmpty();
            RuleFor(x => x.ChainId).NotEmpty();
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}
