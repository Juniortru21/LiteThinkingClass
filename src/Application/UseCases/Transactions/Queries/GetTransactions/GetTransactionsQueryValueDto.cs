namespace Application.UseCases.Transactions.Queries.GetTransactions;
public class GetTransactionsQueryValueDto
{
    public Guid Id { get; set; }
    public required decimal Value { get; set; }
    public bool Status { get; set; }
    public DateTime Date { get; set; }
}
