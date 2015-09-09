using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Exercice5
{
    /// <summary>
    /// Class that is able to read a XML file.
    /// </summary>
    public class XMLScoreReader
    {
        List<Score> scores;

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLScoreReader"/> class.
        /// </summary>
        public XMLScoreReader()
        {
            scores = new List<Score>();
        }

        /// <summary>
        /// Loads the specified _file path.
        /// @see MoveToContent
        /// @see ReadToFollowing
        /// @see ReadElementContentAsString
        /// @see ReadElementContentAsInt
        /// @see Add
        /// </summary>
        /// <param name="_filePath">The _file path.</param>
        public void Load(string _filePath)
        {
            XmlReader reader = XmlReader.Create(_filePath);

            reader.MoveToContent();
            while (reader.ReadToFollowing("ScoreSave"))
            {
                Score score = new Score();
                reader.ReadToFollowing("Player");
                score.name = reader.ReadElementContentAsString();
                reader.ReadToFollowing("Score");
                score.score = reader.ReadElementContentAsInt();
                scores.Add(score);
            }
        }

        /// <summary>
        /// Gets the scores.
        /// </summary>
        /// <returns></returns>
        public List<Score> GetScores()
        {
            return scores;
        }

    }
}
