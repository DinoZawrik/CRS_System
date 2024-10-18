using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRS_System
{
    public partial class SignUp : Form
    {
        private readonly string _connectionString;

        public SignUp()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        }

        private void Registr_Click(object sender, EventArgs e)
        {
            string firstName = FirstName.Text;
            string lastName = SecondName.Text;
            string email = Email.Text;
            string password = Password.Text;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    using (SqlCommand checkUserCommand = new SqlCommand(checkUserQuery, connection))
                    {
                        checkUserCommand.Parameters.AddWithValue("@Email", email);

                        int userCount = (int)checkUserCommand.ExecuteScalar();

                        if (userCount > 0)
                        {
                            MessageBox.Show("Пользователь с таким email уже существует. Пожалуйста, введите другой email.");
                        }
                        else
                        {
                            string insertQuery = "INSERT INTO Users (FirstName, LastName, Email, Password) VALUES (@FirstName, @LastName, @Email, @Password)";

                            using (SqlCommand command = new SqlCommand(insertQuery, connection))
                            {
                                command.Parameters.AddWithValue("@FirstName", firstName);
                                command.Parameters.AddWithValue("@LastName", lastName);
                                command.Parameters.AddWithValue("@Email", email);
                                command.Parameters.AddWithValue("@Password", password);
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Регистрация прошла успешно!");
                                    ClearFields();
                                    this.Close();
                                    Home home = new Home();
                                    home.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Ошибка регистрации. Пожалуйста, попробуйте снова.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при выполнении запроса к базе данных: " + ex.Message);
                
            }
        }

        private void ClearFields()
        {
            FirstName.Text = "Фамилия";
            SecondName.Text = "Имя";
            Email.Text = "Email";
            Password.Text = "Пароль";
        }

        private void BackHome_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void Auth_Click(object sender, EventArgs e)
        {
            SignIn signIn = new SignIn();
            signIn.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            SignIn signIn = new SignIn();
            signIn.Show();
            this.Close();
        }

        private void SignUp_Load(object sender, EventArgs e)
        {

        }
    }
}
