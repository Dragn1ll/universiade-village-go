namespace MicroORM;

public interface IMicroOrm
{
    Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null) where T : new();
    Task<int> ExecuteAsync(string query, object? parameters = null);
    Task<int> InsertAsync<T>(T item, string tableName);

    Task<IEnumerable<T>> SelectAsync<T>(string tableName, string? whereCondition = null,
        object? parameters = null) where T : new();

    Task<int> UpdateAsync<T>(string tableName, string whereCondition, object parameters);
    Task<int> DeleteAsync<T>(string tableName, string whereCondition, object? parameters = null);
}