namespace Application.UseCases.Transactions.Queries.GetTransactions;
public class GetTransactionsQueryDto
{
    public IEnumerable<GetTransactionsQueryValueDto>? Transactions { get; set; }
}
