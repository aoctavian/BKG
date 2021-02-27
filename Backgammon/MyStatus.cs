using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Backgammon
{
    public class MyStatus
    {
        public static int Wins { get; set; }
        public static int WinningStreak { get; set; }
        public static int LongestWinningStreak { get; set; }
        public static int Loses { get; set; }
        public static int Abandons { get; set; }

        public static List<Match> MatchesList = new List<Match>();

        public class Match
        {
            public string Date { get; set; }
            public string Opponent { get; set; }
            public string Result { get; set; }

            public Match(string date, string opponent, string result)
            {
                Date = date;
                Opponent = opponent;
                Result = result;
                MatchesList.Add(this);
            }
        }

        public static async void GetMyStatus()
        {
            MySqlCommand command;
            DbDataReader DR;

            await DEF.connection.OpenAsync();
            command = new MySqlCommand("select * from usersscore where IdUser = '" + Settings.Default.Id + "'", DEF.connection);
            DR = await command.ExecuteReaderAsync();
            if (DR.Read())
            {
                Wins = Convert.ToInt32(DR[1]);
                WinningStreak = Convert.ToInt32(DR[2]);
                LongestWinningStreak = Convert.ToInt32(DR[3]);
                Loses = Convert.ToInt32(DR[4]);
                Abandons = Convert.ToInt32(DR[5]);
            }
            DR.Close();
            
            command = new MySqlCommand("select Date, Name, Result from usersmatches INNER JOIN accounts ON accounts.Id = usersmatches.IdUserOpponent where IdUser = '" + Settings.Default.Id + "'", DEF.connection);
            DR = await command.ExecuteReaderAsync();
            while (DR.Read())
            {
                string dateString = DR[0].ToString();
                DateTime dt = DateTime.Parse(dateString);
                string date = dt.ToString("d MMM yyyy | hh:mm tt");
                string opponent = DR[1].ToString();
                string result = DR[2].ToString();
                new Match(date, opponent, result);
            }
            DR.Close();

            command = new MySqlCommand("select Date, Result from usersmatches where IdUserOpponent < 0 AND IdUser = '" + Settings.Default.Id + "'", DEF.connection);
            DR = await command.ExecuteReaderAsync();
            while (DR.Read())
            {
                string dateString = DR[0].ToString();
                DateTime dt = DateTime.Parse(dateString);
                string date = dt.ToString("d MMM yyyy | hh:mm tt");
                string result = DR[1].ToString();
                new Match(date, "GUEST", result);
            }
            DEF.connection.Close();
        }
    }
}
