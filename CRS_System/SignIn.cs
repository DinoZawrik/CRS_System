using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRS_System
{
    public partial class SignIn : Form
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

        public static class GlobalData
        {
            public static int UserId { get; set; }
        }

        public SignIn()
        {
            InitializeComponent();
        }

        private void BackHome_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void Sign_Click(object sender, EventArgs e)
        {
            string email = Email.Text;
            string password = Pass.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT Id FROM Users WHERE Email = @Email AND Password = @Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int userId = Convert.ToInt32(result);
                        GlobalData.UserId = userId;
                        MessageBox.Show("Авторизация прошла успешно!");
                    }
                    else
                    {
                        MessageBox.Show("Неправильный email или пароль!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при подключении к базе данных: " + ex.Message);
            }
        }

        private void Registr_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }


        private void label2_Click(object sender, EventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }

        private void SignIn_Load(object sender, EventArgs e)
        {

        }
    }
}
