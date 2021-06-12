using System;
using System.Data.SqlClient;
using Dapper;
using System.Linq;

namespace FakeDataApplication.Business
{
    public class FluentBase
    {
        static string conStr = "Server=tcp:fakedataapp.database.windows.net,1433;Initial Catalog=FakeData;Persist Security Info=False;User ID=aleynardvnlr;Password=3798bba-;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        /// <summary>
        /// Creates list of data of requested data table.
        /// </summary>
        /// <typeparam name="T">Requested data table.</typeparam>
        /// <returns></returns>
        public static T[] GetDataList<T>(int requestedData)
        {
            var command = $"select top {requestedData} * from {typeof(T).Name} order by newid()";
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                T[] result = connection.QueryAsync<T>(command).Result.ToArray();
                connection.Close();
                return result;
            }
        }

        /// <summary>
        /// Returns just one data.
        /// </summary>
        /// <typeparam name="T">Requested table.</typeparam>
        /// <param name="id">Requested random "id" of data</param>
        /// <returns></returns>
        public static T GetData<T>(int id)
        {
            var command = $"select * from {typeof(T).Name} where id = {id}";
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                T result = connection.QueryFirstAsync<T>(command).Result;
                connection.Close();
                return result;
            }
        }

        internal int GetDataLength(string tableName)
        {
            var command = $"select * from {tableName}";
            int total;
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                var result = connection.Query(command);
                total = result.Count();
            }

            return total;
        }
        internal T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }

        internal string RemoveFromWhiteSpaces(string name)
        {
            string newString = string.Empty;

            for (int i = 0; i < name.Length; i++)
            {
                if(Char.IsWhiteSpace(name[i]))
                {
                    continue;
                }

                newString = newString + name[i];
            }

            return newString;
        }

        internal string CapitalizeFirstLetterOfString(string name)
        {
            var lowerString = name.ToLower();
            var capitalizeOfFirstLetter = "";
            for (int i = 0; i < lowerString.Length; i++)
            {
                if (i == 0)
                    capitalizeOfFirstLetter = capitalizeOfFirstLetter + lowerString[i].ToString().ToUpper();
                else
                    capitalizeOfFirstLetter = capitalizeOfFirstLetter + lowerString[i];
            }

            return capitalizeOfFirstLetter;
        }


        internal void MessageToTheUser(string funcName)
        {
            Console.WriteLine($"Creating {funcName}...");
        }

        internal void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        internal void CheckRequestedData(int requestedData)
        {
            if (requestedData == 0 || requestedData > 1000)
            {
                Console.WriteLine("The amount of data requested cannot be equal to 0 or greater than 1000.");
                Environment.Exit(0);
            }
        }

    }
}
