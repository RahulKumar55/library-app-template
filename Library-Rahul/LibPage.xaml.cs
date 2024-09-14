using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Library_Rahul
{
    
    public partial class LibPage : ContentPage
    {
        string user = "";
        List<Book> books;
        public LibPage(string username)
        {
            InitializeComponent();
            user = username;
            welcomeTxt.Text = "Welcome " + user + "!";

            Task.Run(async () =>
            {
                books = await App.MyDb.GetAllBooks();
                lvBooks.ItemsSource = books;
            });
        }

        private void lvBooks_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            bookAvailabilityTxt.IsVisible = true;
            Book tappedBook = (Book)e.Item;
            if (!tappedBook.Borrowed)
            {
                bookAvailabilityTxt.Text = $"{tappedBook.Title} is available.";
            }else if (tappedBook.Borrower.Equals(user))
            {
                bookAvailabilityTxt.Text = "You have this book checked out";
            }
            else
            {
                bookAvailabilityTxt.Text = $"Sorry, {tappedBook.Title} is already checked out by {tappedBook.Borrower}";
            }
        }

        async private void Checkout_Clicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            var book = item.CommandParameter as Book;
            if (!book.Borrowed)
            {
                book.Borrower = user;
                book.Borrowed = true;
                await App.MyDb.UpdateBook(book);
                await DisplayAlert("Success", "Done!", "OK");
            }
            else if (book.Borrower.Equals(user))
            {
                await DisplayAlert("Error", "This book is already checked out by you", "OK");
            }
            else
            {
                await DisplayAlert("Error", $"This book is already checked out by {book.Borrower}", "OK");
            }
            bookAvailabilityTxt.IsVisible = false;

        }

        async private void Return_Clicked(object sender, EventArgs e)
        {
            var item = sender as MenuItem;
            var book = item.CommandParameter as Book;
            if (!book.Borrowed)
            {
                await DisplayAlert("Error", "This book cannot be returned.", "OK");
                
            }
            else if (book.Borrowed && book.Borrower.Equals(user))
            {
                book.Borrower = "";
                book.Borrowed = false;
                await App.MyDb.UpdateBook(book);
                await DisplayAlert("Success", "Done!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "This book cannot be returned by you.", "OK");
            }
            bookAvailabilityTxt.IsVisible = false;
        }
    }
}