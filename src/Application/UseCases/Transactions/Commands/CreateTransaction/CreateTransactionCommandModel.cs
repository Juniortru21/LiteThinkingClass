namespace Application.UseCases.Transactions.Commands.CreateTransaction;
public class CreateTransactionCommandModel
{
    public required decimal Value { get; set; }
    public bool Status { get; set; }
    public DateTime Date { get; set; }
}
