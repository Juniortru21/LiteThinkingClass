namespace Application.UseCases.Transactions.Queries.GetTransaction;
public class GetTransactionQueryDto
{
    public Guid Id { get; set; }
    public required decimal Value { get; set; }
    public bool Status { get; set; }
    public DateTime Date { get; set; }
}
