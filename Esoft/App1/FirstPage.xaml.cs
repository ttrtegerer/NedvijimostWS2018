using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstPage : ContentPage
    {
        string connectionString = @"Data Source=stud-mssql.sttec.yar.ru,38325;Database=user79_db;" + "User ID=user79_db;Password=user79;Connect Timeout=30;" + "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" + "MultiSubnetFailover=False";
        SqlConnection connection;
        public FirstPage()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Login.Text != null && Password.Text != null)
            {

                string command = "SELECT * FROM [X-User] inner join [X-Agent] on [X-Agent].User_id=[X-User].Id  where Login='" + Login.Text + "' and Password='" + Password.Text + "'";
                SqlCommand myCommand = new SqlCommand(command, connection);
                connection.Open();
                SqlDataReader a = myCommand.ExecuteReader();
                string Login2 = "null";
                string Par2 = "null";
                int AgentId = 0;
                while (a.Read())
                {

                    Login2 = a.GetString(4);
                    Par2 = a.GetString(5);
                    AgentId = a.GetInt32(6);

                }
                connection.Close();
                if ((Login2 == "null") || (Par2 == "null"))
                {
                    _ = DisplayAlert("Ошибка", "Неправильный логин или пароль", "ок");
                }
                else
                {

                    await Navigation.PushAsync(new EventPage());

                }
            }
            else
            {
                _ = DisplayAlert("Ошибка", "Логин или пароль не могут быть пустыми", "ок");
            }
        }
    }
}