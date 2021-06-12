using FakeDataApplication.Entity;
using FakeDataApplication.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace FakeDataApplication.Business
{
    public class FakeBookStore : FakeBookStoreBase, IFluentBase
    {
        Random randomNumber = new Random();
        int id = 0;
        Book book;
        Book[] books;
        int _requestedData = 1; // default requested value.
        string removedWhiteSpaces, capitalizedFirstLetter;

        public FakeBookStore()
        {
            id = randomNumber.Next(0, 100);
            book = new Book();

        }

        public FakeBookStore(int requestedData)
        {
            _requestedData = requestedData;
            CheckRequestedData(_requestedData);
            id = randomNumber.Next(0, 100);
            books = InitializeArray<Book>(requestedData);
        }
        
        public FakeBookStore FakeBook()
        {
            return this;
        } 

        public FakeBookStore FakeBookAndAuthor()
        {
            MessageToTheUser(MethodBase.GetCurrentMethod().Name);
            if (_requestedData > 1)
            {
                var resultArr = GetDataList<BookAuthor>(_requestedData);
                for (int i = 0; i < resultArr.Length; i++)
                {
                    var cleanString = resultArr[i].book_author.Split("\r\n");
                    books[i].Author = cleanString[0];
                    books[i].Title = resultArr[i].book;
                }
                return this;
            }

            int totalData = GetDataLength("BookAuthor");
            id = randomNumber.Next(1, totalData);
            var result = GetData<BookAuthor>(id);

            book.Author = result.book_author;
            book.Title = result.book;

            return this;
        }

        public string CreateAsJSON(string fileFolder)
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true
            };


            var fileName = $"{fileFolder}\\FakeData{DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()}.json";
            var s = "";
            if (_requestedData > 1)
                s = JsonSerializer.Serialize(books, options);
            else
                s = JsonSerializer.Serialize(book, options);

            Console.WriteLine($"***************************\nJSON file includes {_requestedData} {this.GetType().Name} created at {fileFolder}\n***************************");

            File.WriteAllText(fileName, s);
            return s;

        }

        public void CreateAsXML(string folderName)
        {
            throw new NotImplementedException();
        }
    }
}
