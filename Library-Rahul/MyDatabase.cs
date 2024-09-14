using System;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace Library_Rahul
{
    public class MyDatabase
    {
        readonly SQLiteAsyncConnection dbConnection;

        public MyDatabase()
        {
            // - specifying the name of the database file
            string databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "Book.db");
            Console.WriteLine($"++++++ Database path: {databasePath}");

            // - specify "where" on the device the file will be saved
            dbConnection = new SQLiteAsyncConnection(databasePath);

            // Create the table (based on the TodoItem)
            dbConnection.CreateTableAsync<Book>().Wait();

            // Done!
            Console.WriteLine($"++++++ Database table created!");
            
        }


        // CRUD operations

        //Add book
        public async Task<int> AddBook(Book book)
        {
            // INSERT INTO TodoItems (...,...,..,)
            int numRowsAdded = await dbConnection.InsertAsync(book);
            return numRowsAdded;
        }

        //get all books
        public async Task<List<Book>> GetAllBooks() {
            List<Book> allBooks = await dbConnection.Table<Book>().ToListAsync();
            return allBooks;
        }

        //get Book by Id
        public async Task<Book> GetBook(int id)
        {
            return await dbConnection.GetAsync<Book>(id);
        }

        //update book
        public async Task<int> UpdateBook(Book book)
        {
            return await dbConnection.UpdateAsync(book);
        }

        //delete book
        public async Task<int> DeleteBookById(int id)
        {
            return await dbConnection.DeleteAsync<Book>(id);
        }
    }
}
