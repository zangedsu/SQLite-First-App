using Microsoft.Data.Sqlite;

//строка подключения к БД
string connectionString = "Data Source=Data/firstsqlight.db";

//соединение с БД + создание таблицы
using (SqliteConnection connection = new SqliteConnection(connectionString))
{
    //открываем соединение
    connection.Open();

    //создаём новую команду
    SqliteCommand command = new SqliteCommand();
    command.Connection = connection;
    command.CommandText = "CREATE TABLE Users(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, Age INTEGER NOT NULL)";

    //Чтобы выполнить команду, необходимо применить один из методов SqliteCommand
    //ExecuteNonQuery: выполняет sql-выражение и возвращает количество измененных записей. Подходит для sql-выражений INSERT, UPDATE, DELETE, CREATE.
    //ExecuteReader(): выполняет sql-выражение и возвращает считанные из таблицы строки. Подходит для sql-выражения SELECT.
    //ExecuteScalar(): выполняет sql-выражение и возвращает одно скалярное значение, например, число. Подходит для sql-выражения SELECT в паре с
    //одной из встроенных функций SQL, как например, Min, Max, Sum, Count.
    command.ExecuteNonQuery();

    Console.WriteLine("Таблица Users создана");
}

//соединение с БД + добавление данных
using (SqliteConnection connection = new SqliteConnection(connectionString))
{
    //открываем соединение
    connection.Open();

    //ДОБАВЛЕНИЕ ДАННЫХ:
    //Для вставки объекта используется sql-выражение INSERT, которое имеет следующий синтаксис:
    //INSERT INTO название_таблицы (столбец1, столбец2, столбецN) VALUES ( значение1, значение2, значениеN)

    SqliteCommand command = new SqliteCommand();
    command.Connection = connection;
    command.CommandText = "INSERT INTO Users (Name, Age) VALUES ('Tom', 36)";
    //десь метод ExecuteNonOuery() возвращает число затронутых строк (в данном случае добавленных в таблицу объектов). Хотя нам необязательно
    //возвращать результат метода, но данный результат может использоваться в качестве проверки, что операция, в частности, добавление, прошла успешно.
    int number = command.ExecuteNonQuery();
    Console.WriteLine($"В таблицу Users добавлено объектов: {number}");
}

//соединение с БД + обновление данных
using (SqliteConnection connection = new SqliteConnection(connectionString))
{
    //открываем соединение
    connection.Open();

    //ОБНОВЛЕНИЕ ОБЪЕКТОВ:
    //Для обновления применяется sql-команда UPDATE, которое имеет следующий синтаксис:
    //UPDATE название_таблицы
    //SET столбец1=значение1, столбец2=значение2, столбецN=значениеN
    //WHERE некоторый_столбец=некоторое_значение

    string sqlExpression = "UPDATE Users SET Age=20 WHERE Name='Tom'";
    SqliteCommand command = new SqliteCommand(sqlExpression, connection);

    int number = command.ExecuteNonQuery();

    Console.WriteLine($"Обновлено объектов: {number}");
}

//соединение с БД + удаление данных
using (SqliteConnection connection = new SqliteConnection(connectionString))
{
    //открываем соединение
    connection.Open();

    //УДАЛЕНИЕ:
    //Удаление производится с помощью sql-выражения DELETE, которое имеет следующий синтаксис:
    //DELETE FROM таблица
    //WHERE столбец = значение

    string sqlExpression = "DELETE  FROM Users WHERE Name='Tom'";
    SqliteCommand command = new SqliteCommand(sqlExpression, connection);

    int number = command.ExecuteNonQuery();

    Console.WriteLine($"Удалено объектов: {number}");
}

//Добавление данных через консоль
Console.WriteLine("Введите имя:");
string name = Console.ReadLine();

Console.WriteLine("Введите возраст:");
int age = Int32.Parse(Console.ReadLine());

string sqlExpression1 = $"INSERT INTO Users (Name, Age) VALUES ('{name}', {age})";

//соединение с БД + добавление данных из консоли
using (SqliteConnection connection = new SqliteConnection(connectionString))
{
    //открываем соединение
    connection.Open();

    SqliteCommand command = new SqliteCommand(sqlExpression1, connection);

    int number = command.ExecuteNonQuery();

    Console.WriteLine($"Добавлено объектов: {number}");
}

//Чтение из базы данных
using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();

    SqliteCommand command = new SqliteCommand("SELECT * FROM Users", connection);
    using (SqliteDataReader reader = command.ExecuteReader())
    {
        if (reader.HasRows) // если есть данные
        {
            while (reader.Read())   // построчно считываем данные
            {
                var id = reader.GetValue(0);
                var nameOut = reader.GetValue(1);
                var ageOut = reader.GetValue(2);

                Console.WriteLine($"{id} \t {nameOut} \t {ageOut}");
            }
        }
    }
}
    Console.ReadKey();
