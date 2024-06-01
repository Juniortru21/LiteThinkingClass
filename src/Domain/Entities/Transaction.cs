namespace Domain.Entities;
public class Transaction : BaseEntity
{
    public required decimal Value { get; set; }
    public bool Status { get; set; }
    public DateTime Date { get; set; }
}
