using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BkgSvApp
{
    public static class XMLFile
    {
        private const string DATE = "date",
                             USER = "user", REMOTE_IP = "remoteIP", LOCAL_IP = "localIP",
                             COMMAND = "command", TYPE = "type", TIME = "time", ID = "id",
                             VALUE = "value";

        public static void InsertConnection(string remoteIP, string localIP)
        {
            string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}//Connections.xml";

            XElement root = XElement.Load(filePath);
            XElement date = root.Elements(DATE).Where(e => e.Attribute(VALUE).Value == DateTime.Now.ToString("dd/MM/yyyy")).SingleOrDefault();
            if (date == null)
            {
                date = new XElement(DATE, new XAttribute(VALUE, DateTime.Now.ToString("dd/MM/yyyy")));
                root.AddFirst(date);
            }
            XElement users = date.Elements(USER).Where(e => e.Attribute(REMOTE_IP).Value == remoteIP && e.Attribute(LOCAL_IP).Value == localIP).SingleOrDefault();

            if (users == null)
            {
                XElement user = new XElement(USER, new XAttribute(REMOTE_IP, remoteIP),
                                                   new XAttribute(LOCAL_IP, localIP));

                XElement command = new XElement(COMMAND, new XAttribute(TIME, DateTime.Now.ToString("hh:mm:ss tt")),
                                                         new XAttribute(TYPE, "CONNECTED"));
                user.AddFirst(command);
                date.AddFirst(user);
            }
            else
            {
                XElement command = new XElement(COMMAND, new XAttribute(TIME, DateTime.Now.ToString("hh:mm:ss tt")),
                                                         new XAttribute(TYPE, "CONNECTED"));
                users.AddFirst(command);
            }

            root.Save(filePath);
        }

        public static void InsertCommand(string remoteIP, string localIP, string id, string commandType)
        {
            string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}//Connections.xml";

            XElement root = XElement.Load(filePath);
            XElement user = root.Element(DATE).Elements(USER).Where(e => e.Attribute(REMOTE_IP).Value == remoteIP && e.Attribute(LOCAL_IP).Value == localIP).Single();
            XElement command = new XElement(COMMAND, new XAttribute(TIME, DateTime.Now.ToString("hh:mm:ss tt")),
                                                     new XAttribute(ID, id),
                                                     new XAttribute(TYPE, commandType));
            user.AddFirst(command);
            root.Save(filePath);
        }

        public static void InsertCommand(string remoteIP, string localIP, string id, string commandType, string commandValue)
        {
            string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}//Connections.xml";

            XElement root = XElement.Load(filePath);
            XElement user = root.Element(DATE).Elements(USER).Where(e => e.Attribute(REMOTE_IP).Value == remoteIP && e.Attribute(LOCAL_IP).Value == localIP).Single();
            XElement command = new XElement(COMMAND, new XAttribute(TIME, DateTime.Now.ToString("hh:mm:ss tt")),
                                                     new XAttribute(ID, id),
                                                     new XAttribute(TYPE, commandType),
                                                     new XAttribute(VALUE, commandValue));
            user.AddFirst(command);
            root.Save(filePath);
        }
    }
}
