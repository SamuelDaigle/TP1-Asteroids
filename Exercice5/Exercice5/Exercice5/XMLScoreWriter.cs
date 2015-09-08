using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Exercice5
{
    public class XMLScoreWriter
    {
        private XmlDocument xmlDoc;
        private XmlNode rootNode;
        private XmlNode userNode;

        public void WriteXML(string _playerName, string _score)
        {
            if(_playerName == "")
            {
                _playerName = " ";
            }
            try
            {
                XmlDocument myDoc = new XmlDocument();
                myDoc.Load("Scores.xml");

                XmlNode songPlayed = myDoc.CreateNode(XmlNodeType.Element, "ScoreSave", null);

                XmlNode title = myDoc.CreateNode(XmlNodeType.Element, "Player", null);
                title.InnerText = _playerName;
                songPlayed.AppendChild(title);

                XmlNode timePlayed = myDoc.CreateNode(XmlNodeType.Element, "Score", null);
                timePlayed.InnerText = _score;
                songPlayed.AppendChild(timePlayed);

                myDoc.DocumentElement.AppendChild(songPlayed);
                myDoc.Save("Scores.xml");
            }
            catch
            {
                XmlTextWriter writer = new XmlTextWriter("Scores.xml", System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;

                writer.WriteStartElement("Table");
                writer.WriteStartElement("ScoreSave");

                writer.WriteStartElement("Player");
                writer.WriteString(_playerName);
                writer.WriteEndElement();

                writer.WriteStartElement("Score");
                writer.WriteString(_score);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndElement();

                writer.WriteEndDocument();
                writer.Close();
            }
        }
    }
    public class ScoreSave
    {
        public String score;
    }
}