using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BkgSvApp
{
    public static class DB
    {
        private class InsertElem
        {
            public long ID { get; set; }
            public string Wins { get; set; }
            public string WinningStreak { get; set; }
            public string LongestWinningStreak { get; set; }
            public string Loses { get; set; }
            public string Abandons { get; set; }
            public long OpponentID { get; set; }
            public string Result { get; set; }

            public InsertElem(long iD, string wins, string winningStreak, string longestWinningStreak, string loses, string abandons, long opponentID, string result)
            {
                ID = iD;
                Wins = wins;
                WinningStreak = winningStreak;
                LongestWinningStreak = longestWinningStreak;
                Loses = loses;
                Abandons = abandons;
                OpponentID = opponentID;
                Result = result;
            }
        }

        private const string SERVER = "192.168.0.111";
        private const string PORT_DATABASE = "3306";
        private const string DATABASE_BACKGAMMON = "backgammon";
        private const string USER = "bkgm";
        private const string PASSWORD = "iB8tg7TcheAVKOiT";
        private static readonly MySqlConnection connection = new MySqlConnection($"server={SERVER};port={PORT_DATABASE};database={DATABASE_BACKGAMMON};user={USER};password={PASSWORD};");

        private static readonly List<InsertElem> InsertElems = new List<InsertElem>();

        private static Thread InsertThread = new Thread(Insert) { IsBackground = true };

        public static void Update(long ID, string Wins, string WinningStreak, string LongestWinningStreak, string Loses, string Abandons, long OpponentID, string Result)
        {
            InsertElems.Add(new InsertElem(ID, Wins, WinningStreak, LongestWinningStreak, Loses, Abandons, OpponentID, Result));
            if (!InsertThread.IsAlive)
            {
                InsertThread = new Thread(Insert) { IsBackground = true };
                InsertThread.Start();
                //Console.WriteLine("UPDATING DB SCORE AND MATCHES");
            }
        }

        private static void Insert()
        {
            connection.OpenAsync();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = $"UPDATE usersscore SET Wins = '{InsertElems[0].Wins}'," +
                                                        $"WinningStreak = '{InsertElems[0].WinningStreak}'," +
                                                        $"LongestWinningStreak = '{InsertElems[0].LongestWinningStreak}'," +
                                                        $"Loses = '{InsertElems[0].Loses}'," +
                                                        $"Abandons = '{InsertElems[0].Abandons}'" +
                                                        $"where IdUser = '{InsertElems[0].ID}'";
            command.ExecuteNonQueryAsync();
            
            command.Parameters.AddWithValue("@date", DateTime.Now);
            command.CommandText = $"INSERT INTO usersmatches VALUES ('{ InsertElems[0].ID}', @date, '{InsertElems[0].OpponentID}', '{InsertElems[0].Result}')";
            command.ExecuteNonQueryAsync();
            connection.Close();
            InsertElems.RemoveAt(0);
            if (InsertElems.Count > 0)
                Insert();
        }
    }
}
