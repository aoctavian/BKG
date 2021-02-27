using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backgammon.Properties;

namespace Backgammon
{
    public partial class DataGridViewOnlinePlayers : UserControl
    {
        private const string ONLINE = "ONLINE", PLAYING = "PLAYING", AWAY = "AWAY";
        private const int INVITE = 0, ACCEPT = 1, CANCEL = 2, HIDDEN = 3;
        private static BindingList<OnlinePlayer> OnlinePlayersList { get; set; }
        private static Random random = new Random();

        private class OnlinePlayer
        {
            private string availability;
            private int actionButton;

            public OnlinePlayer(string id, string name, string availability)
            {
                Color = ColorTranslator.FromHtml(string.Format("#{0:X6}", random.Next(0x1000000) & 0x7F7F7F));
                Id = id;
                Name = name;
                Availability = availability;
                switch (availability)
                {
                    case ONLINE:
                    case AWAY:
                        ActionButton = INVITE;
                        break;
                    case PLAYING:
                        ActionButton = HIDDEN;
                        break;
                }
                OnlinePlayersList.Add(this);
                OnlinePlayersList.OrderBy(p => p.Name);
            }

            public Image AvailabilityImage { get; set; }
            public string Name { get; set; }
            public Image ActionButtonImage { get; set; }

            [Browsable(false)]
            public Color Color { get; set; }

            [Browsable(false)]
            public string Id { get; set; }

            [Browsable(false)]
            public int Zone { get; set; }

            [Browsable(false)]
            public int PiecesType { get; set; }

            [Browsable(false)]
            public int ActionButton
            {
                get { return actionButton; }
                set
                {
                    actionButton = value;
                    switch(value)
                    {
                        case INVITE:
                            ActionButtonImage = Resources.button_invite;
                            Zone = 0;
                            PiecesType = 0;
                            break;
                        case CANCEL:
                            ActionButtonImage = Resources.button_cancel;
                            Zone = 0;
                            PiecesType = 0;
                            break;
                        case ACCEPT:
                            ActionButtonImage = Resources.button_accept;
                            break;
                        case HIDDEN:
                            ActionButtonImage = Resources.button_hidden;
                            Zone = 0;
                            PiecesType = 0;
                            break;
                    }
                }
            }

            [Browsable(false)]
            public string Availability
            {
                get { return availability; }
                set
                {
                    availability = value;
                    switch(value)
                    {
                        case ONLINE:
                            AvailabilityImage = Resources.ic_online;
                            //ActionButton = INVITE;
                            break;
                        case PLAYING:
                            AvailabilityImage = Resources.ic_playing;
                            //ActionButton = HIDDEN;
                            break;
                        case AWAY:
                            AvailabilityImage = Resources.ic_away;
                            //ActionButton = INVITE;
                            break;
                    }
                }
            }
        }

        public static Color PlayerColor(string id)
        {
            OnlinePlayer player = OnlinePlayersList.Where(p => p.Id == id).Single();
            return player.Color;
        }

        public DataGridViewOnlinePlayers()
        {
            InitializeComponent();
            OnlinePlayersList = new BindingList<OnlinePlayer>();
            dataGridView.Columns[0].DataPropertyName = "AvailabilityImage";
            dataGridView.Columns[1].DataPropertyName = "Name";
            dataGridView.Columns[2].DataPropertyName = "ActionButtonImage";
            dataGridView.DataSource = OnlinePlayersList;// new BindingSource(OnlinePlayersList, null);
        }

        private void DataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 2 && Application.OpenForms.OfType<GameForm>().SingleOrDefault() == null)
            {
                OnlinePlayer player = (OnlinePlayer)dataGridView.CurrentRow.DataBoundItem;
                switch(player.ActionButton)
                {
                    case INVITE:
                        bool sended = Application.OpenForms.OfType<MainForm>().Single().SendInvitation(player.Id);
                        if (sended)
                        {
                            player.ActionButton = CANCEL;
                            OnlinePlayersList.ResetItem(OnlinePlayersList.IndexOf(player));
                        }
                        break;
                    case CANCEL:
                        Application.OpenForms.OfType<MainForm>().Single().SendCancelInvitation(player.Id);
                        player.ActionButton = INVITE;
                        OnlinePlayersList.ResetItem(OnlinePlayersList.IndexOf(player));
                        break;
                    case ACCEPT:
                        Application.OpenForms.OfType<MainForm>().Single().START_GAME(player.Id, player.Zone, player.PiecesType);
                        break;
                }
            }
        }

        public void Clear()
        {
            OnlinePlayersList.Clear();
        }

        public void Add(string id, string name, string availability)
        {
            new OnlinePlayer(id, name, availability);
        }

        public void Remove(string id)
        {
            OnlinePlayersList.Remove(OnlinePlayersList.Where(p => p.Id == id).Single());
        }

        public void Me_ONLINE()
        {
            dataGridView.Columns[2].Visible = true;
        }

        public void Me_PLAYING()
        {
            dataGridView.Columns[2].Visible = false;
            OnlinePlayersList.ToList().ForEach(p => p.ActionButton = INVITE);
            OnlinePlayersList.ResetBindings();
        }

        public void Me_AWAY()
        {
            OnlinePlayersList.Where(p => p.ActionButton == CANCEL || p.ActionButton == ACCEPT).ToList().ForEach(p => p.ActionButton = INVITE);
            OnlinePlayersList.ResetBindings();
        }

        public void ChangeAvailability(string id, string availability)
        {
            OnlinePlayer player = OnlinePlayersList.Where(p => p.Id == id).Single();
            switch(player.Availability)
            {
                case ONLINE:
                    if (availability == PLAYING)
                        player.ActionButton = HIDDEN;
                    else if (availability == AWAY)
                        player.ActionButton = INVITE;
                    break;
                case PLAYING:
                    player.ActionButton = INVITE;
                    break;
                case AWAY:
                    break;
            }
            player.Availability = availability;
            OnlinePlayersList.ResetItem(OnlinePlayersList.IndexOf(player));
        }

        public void ChangeName(string id, string name)
        {
            OnlinePlayer player = OnlinePlayersList.Where(p => p.Id == id).Single();
            player.Name = name;
            OnlinePlayersList.ResetItem(OnlinePlayersList.IndexOf(player));
        }

        public bool CheckCanPlay(string id)
        {
            OnlinePlayer player = OnlinePlayersList.Where(p => p.Id == id).Single();
            if (player.ActionButton == INVITE)
                return true;
            else
                return false;
        }

        public void ReceivedInvitation(string id, int zone, int piecesType)
        {
            OnlinePlayer player = OnlinePlayersList.Where(p => p.Id == id).Single();
            player.ActionButton = ACCEPT;
            player.Zone = zone;
            player.PiecesType = piecesType;
            OnlinePlayersList.ResetItem(OnlinePlayersList.IndexOf(player));
        }

        public void CancelInvitation(string id)
        {
            OnlinePlayer player = OnlinePlayersList.Where(p => p.Id == id).Single();
            player.ActionButton = INVITE;
            OnlinePlayersList.ResetItem(OnlinePlayersList.IndexOf(player));
        }
    }
}
