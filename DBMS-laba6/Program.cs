using Microsoft.Data.Sqlite;
using static System.Net.Mime.MediaTypeNames;

class Progarm
{

    string table = "CREATE TABLE Clients(_Id INTEGER NOT NULL CONSTRAINT PK_Clients " +
        "PRIMARY KEY AUTOINCREMENT,Name TEXT NULL,Surname TEXT NULL,PhoneNumber TEXT NULL,Adress TEXT NULL)";


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
        using (var connection = new SqliteConnection("Data Source=dbtest.db"))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Clients (Name, Surname, PhoneNumber, Adress) " +
                "VALUES ('Tom', 'Jenkins', '+798335889', 'Pushkina 28')";
            int number = command.ExecuteNonQuery();

            Console.WriteLine($"В таблицу Clients добавлено объектов: {number}");
        }
    }

    static void Update()
    {
        string sqlExpression = "UPDATE Clients  SET Name='Bob' WHERE  Id=3";
        using (var connection = new SqliteConnection("Data Source=dbtest.db"))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(sqlExpression, connection);

            int number = command.ExecuteNonQuery();

            Console.WriteLine($"Обновлено объектов: {number}");
        }
    }

    static void GetClients()
    {
        string sqlExpression = "SELECT * FROM Clients";
        using (var connection = new SqliteConnection("Data Source=dbtest.db"))
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
                        var surname = reader.GetValue(2);
                        var phone = reader.GetValue(3);
                        var adress = reader.GetValue(4);

                        Console.WriteLine($"{id} \t {name} \t {surname} \t {phone} \t {adress} ");
                    }
                }
            }
        }
    }

    static void GetName(string username)
    {
        using (var connection = new SqliteConnection("Data Source=dbtest.db"))
        {
            connection.Open();

            string sqlExpression = "SELECT * FROM Clients WHERE (Name)=(@name)";

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
        using (var connection = new SqliteConnection("Data Source=dbtest.db"))
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
        var connectionString = "Data Source=dbtest.db";
        //var table = "CREATE TABLE Users(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, " +
        //"Name TEXT NOT NULL, Age INTEGER NOT NULL)";
        var table = "CREATE TABLE Clientssss(_Id INTEGER NOT NULL CONSTRAINT PK_Clients PRIMARY KEY AUTOINCREMENT,Name TEXT NULL,Surname TEXT NULL,PhoneNumber TEXT NULL,Adress TEXT NULL)";
        //https://metanit.com/sharp/adonetcore/4.2.php

        //CreateTable(connectionString, table);

        //CreateEntry();
        //Update();
        //GetName("Tom");
        GetClients();
        //Delete(1);
        //GetUsers();
    }
}
