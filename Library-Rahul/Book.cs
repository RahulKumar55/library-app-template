using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Library_Rahul
{
    public class Book
    {
        [PrimaryKey, AutoIncrement] 
        public int ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool Borrowed { get; set; }
        public string Borrower { get; set; }

        public Book()
        {
            this.Borrowed = false;
            this.Borrower = "";
        }

        public Book(string title, string author)
        {
            this.Title = title;
            this.Author = author;
            this.Borrowed = false;
            this.Borrower = "";
        }
    }
}
