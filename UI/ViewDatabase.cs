using ElevatorApp.Data;
using System.Data;

namespace ElevatorApp
{
    public partial class ViewDatabase : Form
    {
        private readonly Database database;

        public ViewDatabase(Database db)
        {
            InitializeComponent();
            database = db;
            LoadData();

            // Buttons Event Handlers - Clicks Effects
            this.btnClose.MouseEnter += (s, e) => btnClose.BackColor = Color.FromArgb(200, 35, 51);
            this.btnClose.MouseLeave += (s, e) => btnClose.BackColor = Color.FromArgb(220, 53, 69);
        }

        private void LoadData()
        {
            try
            {
                // Get data from memory
                DataTable events = database.GetAllEvents();
                dataGridView.DataSource = events;

                if (dataGridView.Columns.Count > 0)
                {
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (dataGridView.Columns.Contains("ID"))
                        dataGridView.Columns["ID"].FillWeight = 15;
                    if (dataGridView.Columns.Contains("Elevator"))
                        dataGridView.Columns["Elevator"].FillWeight = 25;
                    if (dataGridView.Columns.Contains("Event"))
                        dataGridView.Columns["Event"].FillWeight = 20;
                    if (dataGridView.Columns.Contains("Floor"))
                        dataGridView.Columns["Floor"].FillWeight = 15;
                    if (dataGridView.Columns.Contains("Time"))
                        dataGridView.Columns["Time"].FillWeight = 25;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}",
                    "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}