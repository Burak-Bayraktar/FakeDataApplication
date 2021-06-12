using FakeDataApplication.Entity;
using FakeDataApplication.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeDataApplication.Business
{
    public class FakePersonBase : FluentBase
    {
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
        internal string CreateFullAddress(Neighborhood neighborhood, Street street, District district, Province province)
        {
            Random randomNumber = new Random();
            string fullAddress = $"{neighborhood.neighborhood} Mah. " +
                                 $"{street.street} No:{randomNumber.Next(1, 100)} " +
                                 $"Dr:{randomNumber.Next(1, 20)} {district.district} / {province.province} ";
            return fullAddress;
        }

    }
}
