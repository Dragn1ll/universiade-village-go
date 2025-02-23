namespace MicroORM;

public interface IMicroOrm
{
    Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null) where T : new();
    Task<int> ExecuteAsync(string query, object? parameters = null);
}