using System;
using System.Configuration;
using System.Windows.Forms;
using static CRS_System.SignIn;

namespace CRS_System
{
    public partial class Home : Form
    {
        private readonly string _connectionString;

        public Home()
        {
            InitializeComponent();
            _connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
        }

        private void SignUp_Click(object sender, EventArgs e)
        {
            var signUpForm = new SignUp();
            signUpForm.Show();
            Hide();
        }

        private void SignIn_Click(object sender, EventArgs e)
        {
            var signInForm = new SignIn();
            signInForm.Show();
            Hide();
        }

        private void Booking_Click(object sender, EventArgs e)
        {
            if (GlobalData.UserId != 0)
            {
                var bookingForm = new Booking(_connectionString);
                bookingForm.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Авторизуйтесь, пожалуйста.");
            }
        }

        private void ManageBooking_Click(object sender, EventArgs e)
        {
            if (GlobalData.UserId != 0)
            {
                Close();
                var manageBookingForm = new ManageBooking(GlobalData.UserId);
                manageBookingForm.Show();
            }
            else
            {
                MessageBox.Show("Авторизуйтесь, пожалуйста.");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
