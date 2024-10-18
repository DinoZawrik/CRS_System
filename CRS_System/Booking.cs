using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CRS_System
{
    public partial class Booking : Form
    {
        private readonly FlightRepository _flightRepository;
        
        public Booking(string connectionString)
        {
            InitializeComponent();
            _flightRepository = new FlightRepository(connectionString);
            PopulateFlights();

            dataGridView1.CellContentClick -= dataGridView1_CellContentClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
        }

        public class Flight
        {
            public string FlightNumber { get; set; }
            public string DepartureCity { get; set; }
            public string DestinationCity { get; set; }
            public DateTime DepartureTime { get; set; }
            public DateTime ArrivalTime { get; set; }
            public decimal Price { get; set; }
        }

        public class FlightRepository
        {
            public string ConnectionString { get; set; }

            public FlightRepository(string connectionString)
            {
                ConnectionString = connectionString;
            }

            public List<Flight> GetAllFlights()
            {
                List<Flight> flights = new List<Flight>();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Flights";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Flight flight = new Flight
                                {
                                    FlightNumber = reader["FlightNumber"].ToString(),
                                    DepartureCity = reader["DepartureCity"].ToString(),
                                    DestinationCity = reader["DestinationCity"].ToString(),
                                    DepartureTime = Convert.ToDateTime(reader["DepartureTime"]),
                                    ArrivalTime = Convert.ToDateTime(reader["ArrivalTime"]),
                                    Price = Convert.ToDecimal(reader["Price"])
                                };
                                flights.Add(flight);
                            }
                        }
                    }
                }

                return flights;
            }
        }

        private void PopulateFlights()
        {
            List<Flight> flights = _flightRepository.GetAllFlights();

            dataGridView1.AutoGenerateColumns = false;

            // Добавляем столбцы
            dataGridView1.Columns.Add("FlightNumber", "Номер рейса");
            dataGridView1.Columns.Add("DepartureCity", "Город отправления");
            dataGridView1.Columns.Add("DestinationCity", "Город прибытия");
            dataGridView1.Columns.Add("DepartureTime", "Время отправления");
            dataGridView1.Columns.Add("ArrivalTime", "Время прибытия");
            dataGridView1.Columns.Add("Price", "Цена");

            // Устанавливаем соответствие
            dataGridView1.Columns["FlightNumber"].DataPropertyName = "FlightNumber";
            dataGridView1.Columns["DepartureCity"].DataPropertyName = "DepartureCity";
            dataGridView1.Columns["DestinationCity"].DataPropertyName = "DestinationCity";
            dataGridView1.Columns["DepartureTime"].DataPropertyName = "DepartureTime";
            dataGridView1.Columns["ArrivalTime"].DataPropertyName = "ArrivalTime";
            dataGridView1.Columns["Price"].DataPropertyName = "Price";

            // Добавляем необходимое количество строк
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(flights.Count);

            // Установить значение номера рейса для соответствующей ячейки
            int rowIndex = 0;
            foreach (Flight flight in flights)
            {
                dataGridView1.Rows[rowIndex].Cells["FlightNumber"].Value = flight.FlightNumber;
                rowIndex++;
            }

            // Добавить кнопку с номером рейса
            DataGridViewButtonColumn flightNumberButtonColumn = new DataGridViewButtonColumn();
            flightNumberButtonColumn.HeaderText = "Забронировать рейс";
            flightNumberButtonColumn.Text = "Забронировать";
            flightNumberButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(flightNumberButtonColumn);

            dataGridView1.DataSource = flights;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].HeaderText == "Забронировать рейс")
            {
                if (dataGridView1.Rows[e.RowIndex].DataBoundItem is Flight selectedFlight)
                {
                    BookSelectedFlight(selectedFlight);
                }
            }
        }

        private void BookSelectedFlight(Flight selectedFlight)
        {
            Pay payForm = new Pay(selectedFlight);
            this.Close();
            payForm.ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Home home = new Home();
            home.Show();
            
        }

        private void Booking_Load(object sender, EventArgs e)
        {

        }
    }
}
