namespace Application.Common.Results;
public class ResultException
{
    public required string PropertyName { get; set; }
    public required string ErrorMessage { get; set; }
}
