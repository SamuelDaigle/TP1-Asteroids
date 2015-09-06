﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Exercice5
{
    public class XMLScoreReader
    {
        List<Score> scores;

        public XMLScoreReader()
        {
            scores = new List<Score>();
        }

        public void Load(string _filePath)
        {
            XmlReader reader = XmlReader.Create(_filePath);

            reader.MoveToContent();
            while (reader.ReadToFollowing("Score"))
            {
                Score score = new Score();
                reader.ReadToFollowing("Name");
                score.name = reader.ReadElementContentAsString();
                reader.ReadToFollowing("Test");
                score.score = reader.ReadElementContentAsInt();
                scores.Add(score);
            }
        }

        public List<Score> GetScores()
        {
            return scores;
        }

    }
}
