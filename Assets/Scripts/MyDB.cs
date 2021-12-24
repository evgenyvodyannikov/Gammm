using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

static class MyDataBase
{
    private const string fileName = "larder.bytes";
    private static string DBPath;
    private static SqliteConnection connection;
    private static SqliteCommand command;

    [System.Obsolete]
    static MyDataBase()
    {
        DBPath = GetDatabasePath();
    }

    //Получение пути к БД
    [System.Obsolete]
    private static string GetDatabasePath()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);//Путь к БД
        UnpackDatabase(filePath);
        return filePath;
    }
    //Распаковка БД
    [System.Obsolete]
    private static void UnpackDatabase(string toPath)
    {
        string fromPath = Path.Combine(Application.streamingAssetsPath, fileName);//Путь до папки с БД

        WWW reader = new WWW(fromPath);//Чтение
        while (!reader.isDone) { }

        File.WriteAllBytes(toPath, reader.bytes);//Запись
    }


    /// <summary> Этот метод открывает подключение к БД. </summary>
    private static void OpenConnection()
    {
        connection = new SqliteConnection("Data Source=" + DBPath);
        command = new SqliteCommand(connection);
        connection.Open();
    }

    /// <summary> Этот метод закрывает подключение к БД. </summary>
    public static void CloseConnection()
    {
        connection.Close();
        command.Dispose();
    }

    /// <summary> Этот метод выполняет запрос query. </summary>
    /// <param name="query"> Запрос. </param>
    public static void ExecuteQueryWithoutAnswer(string query)
    {
        OpenConnection();// Этот метод открывает подключение к БД
        command.CommandText = query;
        command.ExecuteNonQuery();
        CloseConnection();// Этот метод закрывает подключение к БД.
    }

    /// <summary> Этот метод выполняет запрос query и возвращает ответ запроса. </summary>
    /// <param name="query"> Собственно запрос. </param>
    /// <returns> Возвращает значение 1 строки 1 столбца, если оно имеется. </returns>
    public static string ExecuteQueryWithAnswer(string query)
    {
        OpenConnection();// Этот метод открывает подключение к БД
        command.CommandText = query;
        var answer = command.ExecuteScalar();
        CloseConnection();// Этот метод закрывает подключение к БД.

        if (answer != null) return answer.ToString();
        else return null;
    }

    /// <summary> Этот метод возвращает таблицу, которая является результатом выборки запроса query. </summary>
    /// <param name="query"> Собственно запрос. </param>
    public static DataTable GetTable(string query)
    {
        OpenConnection();// Этот метод открывает подключение к БД

        SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);

        DataSet DS = new DataSet();
        adapter.Fill(DS);
        adapter.Dispose();

        CloseConnection();// Этот метод закрывает подключение к БД.

        return DS.Tables[0];
    }

}

