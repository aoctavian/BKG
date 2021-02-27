using Backgammon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BkgSvApp
{
    class PLAYER
    {
        public static List<PLAYER> PlayersList = new List<PLAYER>();

        private TcpClient Client { get; set; }
        private string RemoteEndPoint { get; set; }
        private string LocalEndPoint { get; set; }
        private Thread ClientThread { get; set; }
        private StreamReader STR { get; set; }
        private StreamWriter STW { get; set; }

        private bool Connected { get; set; }
        private bool LoggedIn { get; set; }

        private static long IDGuest = -1;
        private long ID { get; set; }
        private string Name { get; set; }
        private string Status { get; set; } = DEF.ONLINE;
        private PLAYER OPPONENT { get; set; }

        private readonly Random random = new Random();

        public static string GameAppVersion;

        public PLAYER(TcpClient client)
        {
            PLAYER player = PlayersList.Find(p => p.RemoteEndPoint == ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString() &&
                                                  p.LocalEndPoint == ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString());
            if (player != null)
            {
                Console.WriteLine("IP ALREADY EXISTS");
                player.STR.Close();
            }

            Client = client;
            RemoteEndPoint = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            LocalEndPoint = ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
            XMLFile.InsertConnection(RemoteEndPoint, LocalEndPoint);
            PlayersList.Add(this);
            Console.WriteLine("PLAYERS ++ => " + PlayersList.Count);
            STR = new StreamReader(Client.GetStream());
            STW = new StreamWriter(Client.GetStream())
            {
                AutoFlush = true //se curata dupa fiecare scriere
            };
            Connected = true;
            ClientThread = new Thread(ListenToClient)
            {
                IsBackground = true
            };
            ClientThread.Start();
        }

        private void RemovePlayer(PLAYER player)
        {
            PlayersList.Remove(player);
            Console.WriteLine($"BACKGAMMON PLAYERS -- => {PlayersList.Count}");
            if (PlayersList.Count == 0)
                IDGuest = -1;
        }

        private void Send(string send)
        {
            STW.WriteLine(Crypto.Encrypt(send));
        }

        private void SendToAll(string send)
        {
            foreach (PLAYER player in PlayersList)
                if (player.ID != ID && player.LoggedIn)
                    player.Send(send);
        }

        private string GetBytes(string s)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            string stringBytes = null;
            foreach (byte b in bytes)
                stringBytes += $"{b} ";
            return stringBytes;
        }

        private void DoWhatReceived(string received)
        {
            Console.WriteLine($"{ID}_{Name} ----- {received}");
            string[] r = received.Split('.');
            switch (r[0])
            {
                case DEF.ID_USER:
                    ID = Convert.ToInt32(r[1]);
                    break;
                case DEF.NAME:
                    Name = r[1];
                    break;
                case DEF.GUEST:
                    ID = IDGuest--;
                    Name = $"Guest{ID * (-1)}";
                    Send($"{DEF.GUEST_VALUES}.{ID}.{Name}");
                    break;


                case DEF.TRIED_TO_CONNECT:
                    XMLFile.InsertCommand(RemoteEndPoint, LocalEndPoint, ID.ToString(), DEF.TRIED_TO_CONNECT, $"{r[1]}, {r[2]} times");
                    break;
                case DEF.LOGGED_IN:
                    LoggedIn = true;
                    XMLFile.InsertCommand(RemoteEndPoint, LocalEndPoint, ID.ToString(), DEF.LOGGED_IN);
                    Send($"{DEF.UPDATE_APP}.{GameAppVersion}");
                    SendToAll($"{DEF.NEW_PLAYER}.{ID}.{Name}");
                    break;

                case DEF.UPDATE_APP_VERSION:
                    if (LoggedIn)
                    {
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "GameAppVersion.txt", $"{r[1]}.{r[2]}.{r[3]}.{r[4]}");
                        GameAppVersion = $"{r[1]}.{r[2]}.{r[3]}.{r[4]}";
                        Console.WriteLine($"Game App Updated version {GameAppVersion}");
                        XMLFile.InsertCommand(RemoteEndPoint, LocalEndPoint, ID.ToString(), DEF.UPDATE_APP_VERSION, GameAppVersion);
                    }
                    break;

                case DEF.GET_ONLINE_PLAYERS:
                    Send($"{DEF.ONLINE_PLAYERS}.{string.Join(".", PlayersList.Where(p => p.ID != ID && p.LoggedIn == true).Select(x => $"{x.ID}*{x.Name}*{x.Status}"))}");
                    break;
                case DEF.ONLINE:
                    Status = DEF.ONLINE;
                    SendToAll($"{DEF.AVAILABILITY_ONLINE}.{ID}");
                    break;
                case DEF.PLAYING:
                    Status = DEF.PLAYING;
                    SendToAll($"{DEF.AVAILABILITY_PLAYING}.{ID}");
                    break;
                case DEF.AWAY:
                    Status = DEF.AWAY;
                    SendToAll($"{DEF.AVAILABILITY_AWAY}.{ID}");
                    break;
                case DEF.NEW_NAME:
                    Name = r[1];
                    SendToAll($"{DEF.PLAYER_NEW_NAME}.{ID}.{Name}");
                    XMLFile.InsertCommand(RemoteEndPoint, LocalEndPoint, ID.ToString(), "NEW_NAME", Name);
                    break;


                case DEF.INVITATION:
                    int index = PlayersList.FindIndex(x => x.ID == Convert.ToInt32(r[1]));
                    PlayersList[index].Send($"{DEF.INVITED_BY}.{ID}.{r[2]}.{r[3]}");
                    break;
                case DEF.CANCEL_INVITATION:
                    int index2 = PlayersList.FindIndex(x => x.ID == Convert.ToInt32(r[1]));
                    PlayersList[index2].Send($"{DEF.CANCEL_INVITE}.{ID}");
                    break;
                case DEF.STARTING_GAME:
                    OPPONENT = PlayersList.Find(x => x.ID == Convert.ToInt32(r[1]));
                    OPPONENT.OPPONENT = this;
                    OPPONENT.Send($"{DEF.INVITE_ACCEPTED}.{ID}");
                    Thread.Sleep(500);
                    int START = random.Next(2); //1 = Starts who invites
                    Console.WriteLine($"RANDOM: {START}");
                    switch (START)
                    {
                        case 0:
                            Send(DEF.START_GAME);
                            break;
                        case 1:
                            OPPONENT.Send(DEF.START_GAME);
                            break;
                    }
                    break;
                case DEF.LEAVE_MATCH:
                    OPPONENT = null;
                    break;


                case DEF.UPDATE_DB_SCORE:
                    DB.Update(ID, r[1], r[2], r[3], r[4], r[5], OPPONENT.ID, r[6]);
                    break;


                case DEF.DISCONNECT: //When play offline
                    Send(DEF.DISCONNECT_ACKNOWLEDGED);
                    Connected = false;
                    Console.WriteLine($"{ID}_{Name} disconnected.");
                    XMLFile.InsertCommand(RemoteEndPoint, LocalEndPoint, ID.ToString(), DEF.DISCONNECT);
                    RemovePlayer(this);
                    break;
                case DEF.LEAVING_GAME:
                    SendToAll($"{DEF.LEAVING_GAME}.{ID}");
                    Connected = false;
                    Console.WriteLine($"{ID}_{Name} left the game.");
                    XMLFile.InsertCommand(RemoteEndPoint, LocalEndPoint, ID.ToString(), DEF.LEAVING_GAME);
                    RemovePlayer(this);
                    break;
                    

                case DEF.GLOBAL_CHAT_MESSAGE:
                    SendToAll($"{DEF.GLOBAL_CHAT_MESSAGE}.{ID}.{Name}.{string.Join(".", r.Skip(1))}");
                    break;


                default:
                    if (LoggedIn)
                        OPPONENT.Send(received);
                    else
                    {
                        Console.WriteLine($"ILLEGAL_STRING: {received}");
                        XMLFile.InsertCommand(RemoteEndPoint, LocalEndPoint, ID.ToString(), "ILLEGAL_BYTES", GetBytes(received));
                        Connected = false;
                        RemovePlayer(this);
                    }
                    break;
            }
        }

        private void ListenToClient()
        {
            while (Connected)
            {
                try
                {
                    string encodedString = STR.ReadLine();
                    try
                    {
                        string decodedString = Crypto.Decrypt(encodedString);
                        DoWhatReceived(decodedString);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"{ID}_{Name} ----- {encodedString} -> decode error >{ex.Message}<\nDISCONNECTED {ID}_{Name} illegal encoded string received");
                        XMLFile.InsertCommand(RemoteEndPoint, LocalEndPoint, ID.ToString(), "DECODE_ERROR_BYTES", GetBytes(encodedString)/*received*/);
                        RemovePlayer(this);
                        goto close;
                    }
                }
                catch (Exception ex)
                {
                    LoggedIn = false;
                    Console.WriteLine($"DISCONNECTED {ID}_{Name} crashed >{ex.Message}<");
                    SendToAll($"{DEF.FORCED_LEAVING_GAME}.{ID}");
                    RemovePlayer(this);
                    goto close;
                }
            }
            close:  this.Client.Close();
        }
    }
}
