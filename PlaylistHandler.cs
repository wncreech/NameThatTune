using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NameThatTune
{
    public class PlaylistUtilities
    {
        public static string ExtractIDFromURL(string url) {
            string[] result = url.Split(new string[] { "/playlist/", "?" }, StringSplitOptions.RemoveEmptyEntries);
            return result[1];
        }
        
    }
}