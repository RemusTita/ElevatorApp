using System.Data;
using System.Data.SQLite;

namespace ElevatorApp.Data
{
    public class Database
    {
        private readonly string dbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "ElevatorLog.db"
        );
        private string ConnectionString => $"Data Source={dbPath};Version=3;";

        // DataTable kept in memory during runtime
        private DataTable? eventsTable;

        public Database()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                CreateTable();
            }

            InitializeDataTable();
            LoadExistingData();
        }

        // Create Events table if it doesn't exist
        private void CreateTable()
        {
            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            var cmd = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Events (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Elevator TEXT,
                    Event TEXT,
                    Floor TEXT,
                    Time TEXT
                )", conn);
            cmd.ExecuteNonQuery();
        }

        // Initialize in-memory DataTable structure
        private void InitializeDataTable()
        {
            eventsTable = new DataTable("Events");
            eventsTable.Columns.Add("ID", typeof(int));
            eventsTable.Columns.Add("Elevator", typeof(string));
            eventsTable.Columns.Add("Event", typeof(string));
            eventsTable.Columns.Add("Floor", typeof(string));
            eventsTable.Columns.Add("Time", typeof(string));

            eventsTable.Columns["ID"]!.AutoIncrement = true;
            eventsTable.Columns["ID"]!.AutoIncrementSeed = 1;
            eventsTable.Columns["ID"]!.AutoIncrementStep = 1;
        }

        // Load existing data from database into memory
        private void LoadExistingData()
        {
            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Events", conn);
            adapter.Fill(eventsTable);

            // Set auto-increment seed to next available ID
            if (eventsTable.Rows.Count > 0)
            {
                int maxId = eventsTable.AsEnumerable().Max(row => row.Field<int>("ID"));
                eventsTable.Columns["ID"]!.AutoIncrementSeed = maxId + 1;
            }
        }

        // Log event to in-memory DataTable (not persisted until UpdateDatabase is called)
        public void LogEvent(string elevator, string eventType, string floor)
        {
            DataRow newRow = eventsTable.NewRow();
            newRow["Elevator"] = elevator;
            newRow["Event"] = eventType;
            newRow["Floor"] = floor;
            newRow["Time"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            eventsTable.Rows.Add(newRow);
        }

        // Persist in-memory DataTable changes to database
        public void UpdateDatabase()
        {
            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();

            var adapter = new SQLiteDataAdapter("SELECT * FROM Events", conn);
            var builder = new SQLiteCommandBuilder(adapter);

            adapter.Update(eventsTable);
        }

        // Get copy of all events from memory
        public DataTable GetAllEvents()
        {
            return eventsTable.Copy();
        }

        // Get total record count from memory
        public int GetRecordCount()
        {
            return eventsTable.Rows.Count;
        }
    }
}