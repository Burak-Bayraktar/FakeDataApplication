using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;
using FakeDataApplication.Entity;

namespace FakeDataApplication.Business
{
    public class FluentBase
    {
        static string conStr = @"Server=localhost\SQLEXPRESS;Database=FakeData;Trusted_Connection=True;";

        public static T GetData<T>(int id)
        {
            var command = $"select * from {typeof(T).Name} where id = {id}";
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                T result = connection.Query<T>(command).SingleOrDefault();
                connection.Close();
                return result;
            }
        }

        //internal List<T> GetDataList<T>(T tableName)
        //{

        //}

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

        internal string CreateFullAdress(Neighborhood neighborhood, Street street, District district, Province province)
        {
            Random randomNumber = new Random();
            string fullAddress = $"{neighborhood.neighborhood} Mah. {street.street} No:{randomNumber.Next(1, 100)} Dr:{randomNumber.Next(1, 20)} {district.district} / {province.province} ";
            return fullAddress;
        }

    }
}
