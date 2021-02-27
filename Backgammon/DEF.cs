using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Backgammon
{
    public class DEF
    {
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int description, int reservedValue);

        # region public
        //public const string SERVER = "5.15.99.155";
        //public const int PORT_SERVER = 23305;
        //private const string PORT_DATABASE = "23306";
        #endregion

        #region internal
        public const string SERVER = "WS2019";
        public const int PORT_SERVER = 21312;
        private const string PORT_DATABASE = "3306";
        #endregion

        public const int PORT_SERVER_INTERNAL_SV_APP = 21312;
        private const string DATABASE_BACKGAMMON = "backgammon";
        private const string USER = "bkgm";
        private const string PASSWORD = "iB8tg7TcheAVKOiT";
        public static MySqlConnection connection = new MySqlConnection($"server={SERVER};port={PORT_DATABASE};database={DATABASE_BACKGAMMON};user={USER};password={PASSWORD};");

        internal const int TYPE_GUEST = 0, TYPE_PLAYER = 1;
        internal const int NOT_CONNECTED = 0, CONNECTED = 1;

        internal static NET NET;

        public const string
            ONLINE = "ONLINE",
            PLAYING = "PLAYING",
            AWAY = "AWAY";

        public const string //NET
            GUEST_VALUES = "GUEST_VALUES",

            ONLINE_PLAYERS = "ONLINE_PLAYERS",
            NEW_PLAYER = "NEW_PLAYER",
            AVAILABILITY_ONLINE = "AVAILABILITY_ONLINE",
            AVAILABILITY_PLAYING = "AVAILABILITY_PLAYING",
            AVAILABILITY_AWAY = "AVAILABILITY_AWAY",
            PLAYER_NEW_NAME = "PLAYER_NEW_NAME",

            INVITED_BY = "INVITED_BY",
            CANCEL_INVITE = "CANCEL_INVITE",
            INVITE_ACCEPTED = "INVITE_ACCEPTED",

            DISCONNECT_ACKNOWLEDGED = "DISCONNECT_ACKNOWLEDGED",
            FORCED_LEAVING_GAME = "FORCED_LEAVING_GAME";

        public const string //GameForm
            START_GAME = "START_GAME",
            PLAYER_INFO_1 = "PLAYER_INFO_1",
            PLAYER_INFO_2 = "PLAYER_INFO_2",
            CHAT_MESSAGE = "CHAT_MESSAGE",
            CHAT_MESSAGE_TYPING = "CHAT_MESSAGE_TYPING",
            CHAT_MESSAGE_STOPPED_TYPING = "CHAT_MESSAGE_STOPPED_TYPING",
            ASK_FOR_INFO = "ASK_FOR_INFO",
            PERMISSION_GRANTED = "PERMISSION_GRANTED",
            MOVE = "MOVE",
            UNDO = "UNDO",
            DICE = "DICE",
            GAME_OVER = "GAME_OVER",
            REMATCH = "REMATCH",
            GAME_END = "GAME_END",
            OPPONENT_LEFT = "OPPONENT_LEFT",
            CHANGE_TURN = "CHANGE_TURN";

        public const string //SvApp
            //category creditentials
            ID_USER = "ID_USER",
            NAME = "NAME",
            GUEST = "GUEST",

            //category connect
            TRIED_TO_CONNECT = "TRIED_TO_CONNECT",
            LOGGED_IN = "LOGGED_IN",

            //category players
            GET_ONLINE_PLAYERS = "GET_ONLINE_PLAYERS",
            NEW_NAME = "NEW_NAME",

            //category game
            INVITATION = "INVITATION",
            CANCEL_INVITATION = "CANCEL_INVITATION",
            STARTING_GAME = "STARTING_GAME",
            LEAVE_MATCH = "LEAVE_MATCH",

            //category database
            UPDATE_DB_SCORE = "UPDATE_DB_SCORE",

            //category disconnect
            LEAVING_GAME = "LEAVING_GAME",
            DISCONNECT = "DISCONNECT",

            //category chat
            GLOBAL_CHAT_MESSAGE = "GLOBAL_CHAT_MESSAGE",

            //category update app
            UPDATE_APP_VERSION = "UPDATE_APP_VERSION",
            UPDATE_APP = "UPDATE_APP";
    }
}

