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
    public partial class EddetEvent : ContentPage
    {
        string connectionString = @"Data Source=stud-mssql.sttec.yar.ru,38325;Database=user79_db;" + "User ID=user79_db;Password=user79;Connect Timeout=30;" + "Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" + "MultiSubnetFailover=False";
        SqlConnection connection;
        public int AgentId;
        DatePicker datePicker;
        public EddetEvent()
        {
            InitializeComponent();
            
        }
        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
           
                label.Text = e.NewDate.ToString("dd/MM/yyyy");
        }
        async void SaveEvent(object sender, EventArgs e)
        {


            connection = new SqlConnection(connectionString);
            connection.Open();
            string query = string.Format("INSERT INTO [X-AgentEvent] (AgentId,DateTime,Type,Duration,comment) VALUES('{0}', '{1}', '{2}', '{3}', '{4}')", AgentId,label.Text, TypeEvent.Text, priceLbl.Text,Comment.Text);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();

             await Navigation.PushAsync(new EventPage());


        }
    }
}