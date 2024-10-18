using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRS_System
{
    public partial class ManageBooking : Form
    {
        private int userId;
        private string connectionString;

        public ManageBooking(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

            InitializeDataGridView();
            LoadActiveBookings();
        }

        private void InitializeDataGridView()
        {
            dataGridView1.Columns.AddRange(
                new DataGridViewTextBoxColumn { Name = "BookingId", HeaderText = "Id бронирования" },
                new DataGridViewTextBoxColumn { Name = "FlightNumber", HeaderText = "Номер полёта" },
                new DataGridViewTextBoxColumn { Name = "SeatNumber", HeaderText = "Посадочное место" },
                new DataGridViewTextBoxColumn { Name = "FirstName", HeaderText = "Имя" },
                new DataGridViewTextBoxColumn { Name = "LastName", HeaderText = "Фамилия" },
                new DataGridViewTextBoxColumn { Name = "Address", HeaderText = "Адрес" }
            );
        }

        private void LoadActiveBookings()
        {
            dataGridView1.Rows.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT BookingId, FlightNumber, SeatNumber, FirstName, LastName, Address FROM Booking WHERE UserId = @UserId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader["BookingId"], reader["FlightNumber"], reader["SeatNumber"], reader["FirstName"], reader["LastName"], reader["Address"]);
                        }
                    }
                }
            }
        }

        private void DeleteBook_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int bookingId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Booking WHERE BookingId = @BookingId";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BookingId", bookingId);
                        command.ExecuteNonQuery();
                    }
                }

                LoadActiveBookings();
                MessageBox.Show("Booking deleted successfully.");
            }
            else
            {
                MessageBox.Show("Please select a booking to delete.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Home home = new Home();
            home.Show();
        }

        private void ManageBooking_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
