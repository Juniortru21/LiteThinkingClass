namespace Application.Common.Interfaces;
public interface IStoredProcedure<T>
    where T : notnull
{
    Task<IEnumerable<T>> ExecuteStoredProcedureQuery(string query);
    Task<T> ExecuteStoredProcedureQueryFirst(string query);
    Task<int> ExecuteStoredProcedureCommand(string query);
}
