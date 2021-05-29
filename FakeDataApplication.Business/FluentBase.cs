using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;
using FakeDataApplication.Entity;
using System.Threading.Tasks;
using FakeDataApplication.Entity.Abstract;

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

        internal string ReplaceTurkishCharacter(string text)
        {
            text = text.Replace("İ", "I");
            text = text.Replace("ı", "i");
            text = text.Replace("Ğ", "G");
            text = text.Replace("ğ", "g");
            text = text.Replace("Ö", "O");
            text = text.Replace("ö", "o");
            text = text.Replace("Ü", "U");
            text = text.Replace("ü", "u");
            text = text.Replace("Ş", "S");
            text = text.Replace("ş", "s");
            text = text.Replace("Ç", "C");
            text = text.Replace("ç", "c");
            return text;
        }
        internal IName[] MergeAndShuffleNames(IName[][] names)
        {
            Random randomNumber = new Random();
            List<IName> newList = new List<IName>();
            for (int i = 0; i < names.Length; i++)
            {
                for (int j = 0; j < names[i].Length; j++)
                {
                    newList.Add(names[i][j]);
                }
            }

            var length = newList.Count;
            List<IName> shuffledList = new List<IName>();
            for (int i = 0; i < length; i++)
            {
                var d = randomNumber.Next(0, newList.Count);
                shuffledList.Add(newList[d]);
                newList.Remove(newList[d]);
            }
            return shuffledList.ToArray();
        }
        internal string CreateFullAdress(Neighborhood neighborhood, Street street, District district, Province province)
        {
            Random randomNumber = new Random();
            string fullAddress = $"{neighborhood.neighborhood} Mah. {street.street} No:{randomNumber.Next(1, 100)} Dr:{randomNumber.Next(1, 20)} {district.district} / {province.province} ";
            return fullAddress;
        }
    }
}
