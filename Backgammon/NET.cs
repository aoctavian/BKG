using Backgammon.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backgammon
{
    public class NET
    {
        public MainForm MainForm { get; set; }
        public GameForm GameForm { get; set; }
        public TcpClient Client { get; set; }
        public StreamReader STR { get; set; }
        public StreamWriter STW { get; set; }
        public bool Connected { get; set; }
        public Thread PlayerThread;

        public NET(TcpClient client, MainForm mainForm)
        {
            Connected = true;
            MainForm = mainForm;
            Client = client;
            STR = new StreamReader(Client.GetStream());
            STW = new StreamWriter(Client.GetStream())
            {
                AutoFlush = true //se curata dupa fiecare scriere
            };
            SendToServer($"{DEF.ID_USER}.{Settings.Default.Id}");
            SendToServer($"{DEF.NAME}.{Settings.Default.Name}");
            SendToServer(DEF.LOGGED_IN);
            PlayerThread = new Thread(ListenToOpponent)
            {
                IsBackground = true
            };
            PlayerThread.Start();
        }

        public NET(TcpClient client)
        {
            Connected = true;
            Client = client;
            STR = new StreamReader(Client.GetStream());
            STW = new StreamWriter(Client.GetStream())
            {
                AutoFlush = true //se curata dupa fiecare scriere
            };
            PlayerThread = new Thread(ListenToOpponent)
            {
                IsBackground = true
            };
            PlayerThread.Start();
        }

        private void SendToServer(string send)
        {
            //Console.WriteLine(send);
            STW.WriteLine(Crypto.Encrypt(send));
        }

        public void BackOnline()
        {
            if (PlayerThread.IsAlive)
            {
                MainForm.SetAvailability(DEF.ONLINE);
                SendToServer(DEF.LEAVE_MATCH);
            }
            else
                MainForm.SetOnline();
        }

        protected void DoWhatReceived(string received)
        {
            Console.WriteLine("-- RECEIVED: " + received);
            string[] r = received.Split('.');
            switch (r[0])
            {
                case DEF.GUEST_VALUES:
                    Settings.Default.Id = Convert.ToInt32(r[1]);
                    Settings.Default.Name = r[2];
                    Settings.Default.Save();
                    break;


                case DEF.ONLINE_PLAYERS:
                    MainForm.ShowOnlinePlayers(string.Join(".", r));
                    break;
                case DEF.NEW_PLAYER:
                    if (GameForm != null && GameForm.OPPONENT.Id == int.Parse(r[1]))
                        GameForm.GAMEOVER_OpponentCrashed();
                    MainForm.NewOnlinePlayer(r[1], r[2]);
                    break;
                case DEF.AVAILABILITY_ONLINE:
                    MainForm.PlayerChangedAvailability(r[1], DEF.ONLINE);
                    break;
                case DEF.AVAILABILITY_PLAYING:
                    MainForm.PlayerChangedAvailability(r[1], DEF.PLAYING);
                    break;
                case DEF.AVAILABILITY_AWAY:
                    MainForm.PlayerChangedAvailability(r[1], DEF.AWAY);
                    break;
                case DEF.PLAYER_NEW_NAME:
                    MainForm.PlayerChangedName(r[1], r[2]);
                    break;


                case DEF.INVITED_BY:
                    MainForm.InvitedBy(r[1], Convert.ToInt32(r[2]), Convert.ToInt32(r[3]));
                    break;
                case DEF.CANCEL_INVITE:
                    MainForm.CancelInvitation(r[1]);
                    break;
                case DEF.INVITE_ACCEPTED:
                    MainForm.START_GAME(r[1]);
                    break;


                case DEF.DISCONNECT_ACKNOWLEDGED:
                    Connected = false;
                    break;
                case DEF.FORCED_LEAVING_GAME:
                    if (GameForm != null && GameForm.OPPONENT.Id == int.Parse(r[1]))
                        GameForm.GAMEOVER_OpponentCrashed();
                    goto case DEF.LEAVING_GAME;
                case DEF.LEAVING_GAME:
                    MainForm.PlayerLeavingGame(r[1]);
                    break;


                case DEF.GLOBAL_CHAT_MESSAGE:
                    MainForm.GlobalChatNewMessageReceived(r[1], r[2], string.Join(".", r.Skip(3)));
                    break;

                case DEF.UPDATE_APP:
                    MainForm.CheckForUpdate($"{r[1]}.{r[2]}.{r[3]}.{r[4]}");
                    break;

                default:
                    GameForm.DoWhatReceived(received/*string.Join(".", r)*/);
                    break;
            }
        }

        private void ListenToOpponent()
        {
            while (Connected)
            {
                try
                {
                    string received = STR.ReadLine();
                    DoWhatReceived(Crypto.Decrypt(received));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Crashed >{ ex.Message}<");
                    Connected = false;
                    if (MainForm != null && MainForm.InternetConnection == DEF.CONNECTED)
                        MainForm.ServerConnection = DEF.NOT_CONNECTED; //App.OpenForms
                    if (Application.OpenForms.OfType<LogInForm>().Single().InternetConnection == DEF.CONNECTED)
                        Application.OpenForms.OfType<LogInForm>().Single().ServerConnection = DEF.NOT_CONNECTED;
                }
            }
            this.Client.Close();
            Console.WriteLine("Connection to server stopped");
        }
    }
}
