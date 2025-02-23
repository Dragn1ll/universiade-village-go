using System.Data;
using Npgsql;

namespace MicroORM;

public class MicroOrm(string connectionString) : IMicroOrm
{
    public async Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null) where T : new()
    {
        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
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

            await using var dataReader = await command.ExecuteReaderAsync();
            return Map<T>(dataReader);
        }
        catch (Exception exception)
        {
            throw new InvalidOperationException($"Error: {exception.Message}");
        }
    }

    public async Task<int> ExecuteAsync(string query, object? parameters = null)
    {
        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
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
        
            return await command.ExecuteNonQueryAsync();
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
}