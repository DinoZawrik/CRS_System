using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using static CRS_System.Booking;
using static CRS_System.SignIn;

namespace CRS_System
{
    public partial class Pay : Form
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;

        private readonly Flight selectedFlight;
        private readonly List<Button> seatButtons = new List<Button>();
        private Button selectedSeat = null;
        private string selectedSeatText = "";

        public Pay(Flight selectedFlight)
        {
            InitializeComponent();
            this.selectedFlight = selectedFlight;
            InitializeSeatLayout();
            DisplayFlightInformation();
        }

        private void InitializeSeatLayout()
        {
            const int rows = 2;
            const int cols = 6;
            const int btnWidth = 50;
            const int btnHeight = 50;

            int panelHeight = ClientSize.Height;
            int topOffset = panelHeight / 2;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Button btnSeat = new Button();
                    string seatNumber = $"{row + 1}-{(char)('A' + col)}";
                    btnSeat.Text = seatNumber;
                    btnSeat.Width = btnWidth;
                    btnSeat.Height = btnHeight;
                    btnSeat.Left = 50 + col * (btnWidth + 10);
                    btnSeat.Top = topOffset + 50 + row * (btnHeight + 10);
                    btnSeat.BackColor = IsSeatAvailable(seatNumber) ? Color.LightGreen : Color.LightPink;
                    btnSeat.Click += SeatButton_Click;

                    seatButtons.Add(btnSeat);
                    Controls.Add(btnSeat);
                }
            }
        }

        private void SeatButton_Click(object sender, EventArgs e)
        {
            Button btnSeat = (Button)sender;

            if (!IsSeatAvailable(btnSeat.Text))
            {
                MessageBox.Show("Место забронировано. Выберите другое место.");
                return;
            }

            if (selectedSeat != null && btnSeat != selectedSeat)
            {
                selectedSeat.BackColor = Color.LightGreen;
            }

            selectedSeat = btnSeat;
            selectedSeatText = btnSeat.Text;
            btnSeat.BackColor = Color.LightPink;
        }

        private bool IsSeatAvailable(string seatNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Booking WHERE FlightNumber = @FlightNumber AND SeatNumber = @SeatNumber";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FlightNumber", selectedFlight.FlightNumber);
                    command.Parameters.AddWithValue("@SeatNumber", seatNumber);

                    int count = (int)command.ExecuteScalar();
                    return count == 0;
                }
            }
        }

        private void DisplayFlightInformation()
        {
            richTextBox1.Text = $"Номер рейса: {selectedFlight.FlightNumber}, " +
                             $"Город отправления: {selectedFlight.DepartureCity}, " +
                             $"Город назначения: {selectedFlight.DestinationCity}\n" +
                             $"Время отправления: {selectedFlight.DepartureTime}, " +
                             $"Время прибытия: {selectedFlight.ArrivalTime}\n" +
                             $"Цена: {selectedFlight.Price}";
        }

        private void btnPay_Click_1(object sender, EventArgs e)
        {
            if (selectedSeat != null)
            {
                if (IsSeatAvailable(selectedSeatText))
                {
                    if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                        string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                        string.IsNullOrWhiteSpace(AdresTextBox.Text) ||
                        string.IsNullOrWhiteSpace(CardTextBox.Text) ||
                        string.IsNullOrWhiteSpace(PasportTextBox.Text))
                    {
                        MessageBox.Show("Заполните все поля!");
                        return;
                    }

                    int userId = GlobalData.UserId;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "INSERT INTO Booking (UserId, FlightNumber, SeatNumber, FirstName, LastName, Address, CardNumber, PassportNumber) " +
                                       "VALUES (@UserId, @FlightNumber, @SeatNumber, @FirstName, @LastName, @Address, @CardNumber, @PassportNumber)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@UserId", userId);
                            command.Parameters.AddWithValue("@FlightNumber", selectedFlight.FlightNumber);
                            command.Parameters.AddWithValue("@SeatNumber", selectedSeatText);
                            command.Parameters.AddWithValue("@FirstName", FirstNameTextBox.Text);
                            command.Parameters.AddWithValue("@LastName", LastNameTextBox.Text);
                            command.Parameters.AddWithValue("@Address", AdresTextBox.Text);
                            command.Parameters.AddWithValue("@CardNumber", CardTextBox.Text.Substring(0, Math.Min(CardTextBox.Text.Length, 16)));
                            command.Parameters.AddWithValue("@PassportNumber", PasportTextBox.Text);

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Оплата прошла успешно. Спасибо за покупку!");
                    Close();
                    Home home = new Home();
                    home.Show();
                }
                else
                {
                    MessageBox.Show("Место уже забронировано.");
                }
            }
            else
            {
                MessageBox.Show("Выбирите место для бронирования.");
            }
        }

        private void Paydeny_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            Close();
        }

        private void Pay_Load(object sender, EventArgs e)
        {

        }
    }
}
