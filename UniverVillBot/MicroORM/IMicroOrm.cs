namespace MicroORM;

public interface IMicroOrm
{
    Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null, 
        CancellationToken cancellationToken = default) where T : new();
    Task<int> ExecuteAsync(string query, object? parameters = null, CancellationToken cancellationToken = default);
    Task<int> InsertAsync<T>(T item, string tableName, CancellationToken cancellationToken = default);

    Task<IEnumerable<T>> SelectAsync<T>(string tableName, string? whereCondition = null,
        object? parameters = null, CancellationToken cancellationToken = default) where T : new();

    Task<int> UpdateAsync(string tableName, string whereCondition, object parameters, 
        CancellationToken cancellationToken = default);
    Task<int> DeleteAsync<T>(string tableName, string whereCondition, object? parameters = null, 
        CancellationToken cancellationToken = default);
}