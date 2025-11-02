using System.Data;
using System.Data.SQLite;

namespace ElevatorApp.Data
{
    public class Database
    {
        private readonly string _dbPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "ElevatorLog.db"
        );
        private string ConnectionString => $"Data Source={_dbPath};Version=3;";

        // DataTable kept in memory during runtime
        private DataTable? _eventsTable;

        public Database()
        {
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);
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
            _eventsTable = new DataTable("Events");
            _eventsTable.Columns.Add("ID", typeof(int));
            _eventsTable.Columns.Add("Elevator", typeof(string));
            _eventsTable.Columns.Add("Event", typeof(string));
            _eventsTable.Columns.Add("Floor", typeof(string));
            _eventsTable.Columns.Add("Time", typeof(string));

            _eventsTable.Columns["ID"]!.AutoIncrement = true;
            _eventsTable.Columns["ID"]!.AutoIncrementSeed = 1;
            _eventsTable.Columns["ID"]!.AutoIncrementStep = 1;
        }

        // Load existing data from database into memory
        private void LoadExistingData()
        {
            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();
            var adapter = new SQLiteDataAdapter("SELECT * FROM Events", conn);
            adapter.Fill(_eventsTable);

            // Set auto-increment seed to next available ID
            if (_eventsTable.Rows.Count > 0)
            {
                int maxId = _eventsTable.AsEnumerable().Max(row => row.Field<int>("ID"));
                _eventsTable.Columns["ID"]!.AutoIncrementSeed = maxId + 1;
            }
        }

        // Log event to in-memory DataTable (not persisted until UpdateDatabase is called)
        public void LogEvent(string elevator, string eventType, string floor)
        {
            DataRow newRow = _eventsTable.NewRow();
            newRow["Elevator"] = elevator;
            newRow["Event"] = eventType;
            newRow["Floor"] = floor;
            newRow["Time"] = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            _eventsTable.Rows.Add(newRow);
        }

        // Persist in-memory DataTable changes to database
        public void UpdateDatabase()
        {
            using var conn = new SQLiteConnection(ConnectionString);
            conn.Open();

            var adapter = new SQLiteDataAdapter("SELECT * FROM Events", conn);
            var builder = new SQLiteCommandBuilder(adapter);

            adapter.Update(_eventsTable);
        }

        // Get copy of all events from memory
        public DataTable GetAllEvents()
        {
            return _eventsTable.Copy();
        }

        // Get total record count from memory
        public int GetRecordCount()
        {
            return _eventsTable.Rows.Count;
        }
    }
}