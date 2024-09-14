using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Library_Rahul
{
    public partial class MainPage : ContentPage
    {
        Dictionary<string, string> users = new Dictionary<string, string>();
        public MainPage()
        {
            InitializeComponent();

            users.Add("peter", "1234");
            users.Add("mary", "0000");
        }

        async private void loginBtn_Clicked(object sender, EventArgs e)
        {
            if(usernameTxt.Text.Length == 0 && passwordTxt.Text.Length == 0)
            {
                errorTxt.Text = "ERROR: Username or password cannot be empty";
                errorTxt.IsVisible = true;
            }
            else
            {
                if (!users.ContainsKey(usernameTxt.Text.Trim()))
                {
                    errorTxt.Text = "ERROR: Wrong username or password";
                    errorTxt.IsVisible = true;
                }else if (!passwordTxt.Text.Trim().Equals(users[usernameTxt.Text.Trim()]))
                {
                    errorTxt.Text = "ERROR: Wrong username or password";
                    errorTxt.IsVisible = true;
                }
                else
                {
                    await Navigation.PushAsync(new LibPage(usernameTxt.Text.Trim()));
                    usernameTxt.Text = "";
                    passwordTxt.Text = "";
                    errorTxt.Text = "";
                    errorTxt.IsVisible = false;
                    
                }
            }
        }
    }
}
