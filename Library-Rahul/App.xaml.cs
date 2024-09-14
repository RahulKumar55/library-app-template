using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xamarin.Forms.Xaml;

namespace Library_Rahul
{
    public partial class App : Application
    {
        // create an instance of the MyDatabase class
        // implement it as a singleton
        private static MyDatabase db;
        public static MyDatabase MyDb
        {
            get
            {
                if (db == null)
                {
                    db = new MyDatabase();
                }
                return db;
            }
        }

        public App()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                List<Book> books = new List<Book>();
                books = await MyDb.GetAllBooks();
                if(books.Count == 0)
                {
                    books.Add(new Book("The Lord of the Rings", "J.R.R. Tolkien"));
                    books.Add(new Book("Harry Potter", "J.K. Rowling"));
                    books.Add(new Book("Sword of Destiny", "Andrzej Sapkowski"));
                    books.Add(new Book("A Game of Thrones", "George R.R. Martin"));
                    books.ForEach(async (book) => await MyDb.AddBook(book));
                }
            });

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {           
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
