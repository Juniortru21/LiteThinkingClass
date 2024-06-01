namespace Application.UseCases.Common.Results
{
    public class Result<T>(T data, ResultType resultType, params string[] errors)
    {
        public ResultType ResultType { get; } = resultType;

        public IEnumerable<string> Errors { get; } = errors;

        public T Data { get; } = data;
    }
}
