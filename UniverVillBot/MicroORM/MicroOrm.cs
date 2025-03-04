using System.Data;
using Npgsql;

namespace MicroORM;

public class MicroOrm(string connectionString) : IMicroOrm
{
    public async Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null, 
        CancellationToken cancellationToken = default) where T : new()
    {
        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);
            await using var command = connection.CreateCommand();
            command.CommandText = query;
            if (parameters != null)
            {
                foreach (var property in parameters.GetType().GetProperties())
                {
                    command.Parameters.AddWithValue(new NpgsqlParameter(property.Name, 
                        property.GetValue(parameters)));
                }
            }

            await using var dataReader = await command.ExecuteReaderAsync(cancellationToken);
            return Map<T>(dataReader);
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException($"Error: {exception.Message}");
        }
    }

    public async Task<int> ExecuteAsync(string query, object? parameters = null, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync(cancellationToken);
            await using var command = connection.CreateCommand();
            command.CommandText = query;
            if (parameters != null)
            {
                foreach (var property in parameters.GetType().GetProperties())
                {
                    command.Parameters.AddWithValue(new NpgsqlParameter(property.Name, 
                        property.GetValue(parameters)));
                }
            }
        
            return await command.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException($"Error: {exception.Message}");
        }
    }
    
    private IEnumerable<T> Map<T>(IDataReader dataReader) where T : new()
    {
        var results = new List<T>();
        var properties = typeof(T).GetProperties();

        while (dataReader.Read())
        {
            var item = new T();
            foreach (var property in properties)
            {
                if (dataReader[property.Name] != DBNull.Value)
                {
                    property.SetValue(item, dataReader[property.Name]);
                }
            }
            results.Add(item);
        }
        
        return results;
    }
    
    // CRUD

    public async Task<int> InsertAsync<T>(T item, string tableName, CancellationToken cancellationToken = default)
    {
        var properties = typeof(T).GetProperties();
        var columns = string.Join(",", properties.Select(p => p.Name));
        var values = string.Join(",", properties.Select(p => $"@{p.Name}"));
        
        var query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
        return await ExecuteAsync(query, item, cancellationToken);
    }

    public async Task<IEnumerable<T>> SelectAsync<T>(string tableName, string? whereCondition = null,
        object? parameters = null, CancellationToken cancellationToken = default) where T : new()
    {
        var query = $"SELECT * FROM {tableName}";

        if (whereCondition != null)
        {
            query += $" WHERE {whereCondition}";
        }
        
        return await QueryAsync<T>(query, parameters, cancellationToken);
    }

    public async Task<int> UpdateAsync(string tableName, string whereCondition,
        object parameters, CancellationToken cancellationToken = default)
    {
        var properties = parameters.GetType().GetProperties();
        var setColumns = string.Join(",", properties.Select(p => $"{p.Name} = @{p.Name}"));
        
        var query = $"UPDATE {tableName} SET {setColumns} WHERE {whereCondition}";
        return await ExecuteAsync(query, parameters, cancellationToken);
    }

    public async Task<int> DeleteAsync<T>(string tableName, string whereCondition, object? parameters = null, 
        CancellationToken cancellationToken = default)
    {
        var query = $"DELETE FROM {tableName} WHERE {whereCondition}";
        return await ExecuteAsync(query, parameters, cancellationToken);
    }
}