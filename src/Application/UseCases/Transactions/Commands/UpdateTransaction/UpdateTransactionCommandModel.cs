namespace Application.UseCases.Transactions.Commands.UpdateTransaction;
public class UpdateTransactionCommandModel
{
    public required Guid Id { get; set; }
    public required decimal Value { get; set; }
    public bool Status { get; set; }
    public DateTime Date { get; set; }
}
