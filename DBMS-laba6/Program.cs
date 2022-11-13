using Microsoft.Data.Sqlite;

class Progarm
{

    string table = "CREATE TABLE Users(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
        "Name TEXT NOT NULL, Age INTEGER NOT NULL)";
    static void CreateTable(string connectionString, string table)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = table;
            command.ExecuteNonQuery();
        }
        Console.WriteLine("Таблица создана");
    }

   static void CreateEntry()
    {
        using (var connection = new SqliteConnection("Data Source=usersdata.db"))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Users (Name, Age) VALUES ('Tom', 36)";
            int number = command.ExecuteNonQuery();

            Console.WriteLine($"В таблицу Users добавлено объектов: {number}");
        }
    }

    static void Update()
    {
        string sqlExpression = "UPDATE Users SET Age=20 WHERE Name='Tom'";
        using (var connection = new SqliteConnection("Data Source=usersdata.db"))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            int number = command.ExecuteNonQuery();

            Console.WriteLine($"Обновлено объектов: {number}");
        }
    }

    static void GetUsers()
    {
        string sqlExpression = "SELECT * FROM Users";
        using (var connection = new SqliteConnection("Data Source=usersdata.db"))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        var id = reader.GetValue(0);
                        var name = reader.GetValue(1);
                        var age = reader.GetValue(2);

                        Console.WriteLine($"{id} \t {name} \t {age}");
                    }
                }
            }
        }
    }

    static void GetName(string username)
    {
        using (var connection = new SqliteConnection("Data Source=usersdata.db"))
        {
            connection.Open();

            string sqlExpression = "SELECT * FROM Users WHERE (Name)=(@name)";

            SqliteCommand command = new SqliteCommand(sqlExpression, connection);
            // создаем параметр для имени
            SqliteParameter nameParam = new SqliteParameter("@name", username);
            // добавляем параметр к команде
            command.Parameters.Add(nameParam);

            int number = command.ExecuteNonQuery();
            Console.WriteLine($"Добавлено объектов: {number}");

            // вывод данных
            //command.CommandText = "SELECT * FROM Users";
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())   // построчно считываем данные
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int age = reader.GetInt32(2);

                        Console.WriteLine($"{id} \t {name} \t {age}");
                    }
                }
            }
        }
    }

    static void Delete(int id)
    {
        string sqlExpression = "DELETE  FROM Users WHERE _id=@id";
        using (var connection = new SqliteConnection("Data Source=usersdata.db"))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            SqliteParameter nameParam = new SqliteParameter("@id", id);
            // добавляем параметр к команде
            command.Parameters.Add(nameParam);

            int number = command.ExecuteNonQuery();

            Console.WriteLine($"Удалено объектов: {number}");
        }
    }
    static void Main(string[] args)
    {
        var connectionString = "Data Source=usersdata.db";
        var table = "CREATE TABLE Users(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
        "Name TEXT NOT NULL, Age INTEGER NOT NULL)";

        //https://metanit.com/sharp/adonetcore/4.2.php
        //Connect(connectionString);
        //CreateTable(connectionString, table);
        //CreateEntry();
        //Update();
        //GetName("Tom");
        GetUsers();
        Delete(1);
        GetUsers();
    }
}
