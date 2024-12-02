using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameThatTune
{
    public class Song
    {
        public int fileID { get; set; }
        public string spotifyID { get; set; }

        public string title { get; set; }
        public string artist { get; set; }

        private string sampleURL = "my code breaks without it";
        public string imageURL { get; set; }

        public Song()
        {
            fileID = 0;
            spotifyID = "null";
            title = "null";
            artist = "null";
            imageURL = "null";
        }

        public Song(int fileID, string spotifyID, string title, string artist, string imageURL)
        {
            this.fileID = fileID;
            this.spotifyID = spotifyID;
            this.title = title;
            this.artist = artist;
            this.imageURL = imageURL;
        }


    }
}