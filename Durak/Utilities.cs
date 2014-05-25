using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Durak
{
    /// <summary>
    /// This utilities class is used to write updates to log files that keep track of the human
    /// players statistics and also keeps track of what happens each game
    /// </summary>
    class Utilities
    {

        /// <summary>
        /// Updates the log file of your choice with the message, defaults to the default log file
        /// </summary>
        /// <param name="message"></param>
        /// <param name="file"></param>
        public static void UpdateLog(String message, String file = "log.txt")
        {
            String filePath = System.IO.Directory.GetCurrentDirectory() + @"\"+file;
            if (File.Exists(filePath))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(DateTime.Now + " " + message);
                }
            }
            else
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine(DateTime.Now + " " + message);
                }
            }
        }

        /// <summary>
        /// Pass a bool true if player uses and false if player wins to rewrite to the stats file 
        /// to update the players stats
        /// </summary>
        /// <param name="hasLost"></param>
        public static void UpdateStats(bool hasLost)
        {
            String filePath = System.IO.Directory.GetCurrentDirectory() + @"\stats.txt";
            // holds each line of input from the file temporarily
            String[] tempStrings;

            int wins = 0;
            int losses = 0;
            int played = 0;
            // Create a file to write to. 
            if(File.Exists(filePath))
            {
                //get current values 
                using (StreamReader sr = File.OpenText(filePath))
                {
                    tempStrings = sr.ReadLine().Split();
                    int.TryParse(tempStrings[1], out wins);
                    tempStrings = sr.ReadLine().Split();
                    int.TryParse(tempStrings[1], out losses);
                    tempStrings = sr.ReadLine().Split();
                    int.TryParse(tempStrings[1], out played);
                }

                //do math
                played++;
                if (hasLost)
                {
                    losses++;
                }
                else
                {
                    wins++;
                }

                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Wins: " + wins);
                    sw.WriteLine("Losses: " + losses);
                    sw.WriteLine("Played: " + played);
                }
            }
            else
            {

                using (StreamWriter sw = File.CreateText(filePath))
                {
                    sw.WriteLine("Wins: " + wins);
                    sw.WriteLine("Losses: " + losses);
                    sw.WriteLine("Played: " + played);
                }
            }
            
        }
    }
}
