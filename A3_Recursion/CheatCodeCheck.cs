//Author: Joon Song
//Project Name: A3_Recursion
//File Name: CheatCodeCheck.cs
//Creation Date: 10/24/2018
//Modified Date: 10/30/2018
//Description: Program to check if a cheat code is valid - "is it a nerd word?"

using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

namespace A3_Recursion
{
    public class CheatCodeCheck
    {
        //Stop watch to time run time
        private static Stopwatch stopWatch = new Stopwatch();

        static void Main(string[] args)
        {            
            //Setting up various variables for file IO logic
            string baseFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Substring(6) + @"\";
            string inputFileName;
            IEnumerable<string> lines;
            const string OUTPUT_FILE_NAME = "Song_J.txt";
            StreamWriter outFile = File.CreateText(baseFilePath + OUTPUT_FILE_NAME);

            //Requesting and retrieving input file name
            Console.WriteLine("Enter Input File Name:");
            inputFileName = Console.ReadLine();            

            //Starting stop watch
            stopWatch.Start();

            //Reading in and caching lines of input
            lines = File.ReadLines(baseFilePath + inputFileName);

            //Writing appropraite output for each line of input
            foreach (string line in lines)
            {
                outFile.WriteLine($"{line}:{(NerdWordCheck(line, line.Length) ? "YES" : "NO")}");
            }

            //Closing the file and stopping the stop watch
            outFile.Close();
            stopWatch.Stop();

            //Outputting the elasped time
            Console.WriteLine($"{stopWatch.Elapsed.Minutes} minutes {stopWatch.Elapsed.Seconds}.{stopWatch.Elapsed.Milliseconds} seconds");

            //Reading line to keep program open
            Console.ReadLine();
        }

        /// <summary>
        /// Subprogram to check if a given word is a nerd word
        /// </summary>
        /// <param name="word">The word to check if its a nerd word</param>
        /// <param name="length">The length of the word</param>
        /// <returns>Whether the parameter word is a nerd word</returns>
        private static bool NerdWordCheck(string word, int length)
        {            
            //Variables to count number of 'A's and 'B's
            short aCounter = 0;
            short bCounter = 0;
            
            //Checking if word is a base case - returning appropriate result
            if (word == "X")
            {
                return true;
            }
            else if (length == 0)
            {
                return false;
            }

            //If the first letter is an 'A'
            if (word[0] == 'A')
            {
                //Incrementing 'A' counter
                ++aCounter;

                //Iterating through each character in the word
                for (short i = 1; i < length; i++)
                {
                    //If incrementing appropriate counter based on if the charecter is a 'A' or a 'B'
                    if ((word[i] == 'B' && ++bCounter == aCounter) || (word[i] == 'A' && ++aCounter == bCounter))
                    {
                        //Returning a recursive case -replacing NerdWord with base nerdword('X') for second componenf of string
                        return NerdWordCheck(word.Substring(1, i - 1), i - 1) && NerdWordCheck("X" + word.Substring(i + 1), length - i);
                    }
                }

                //Otheriwise returning false
                return false;
            }

            //If there is a Y in the middle of the word, split the string and do nerd word check
            for (int i = 1; i < length - 1; ++i)
            {
                if (word[i] == 'Y')
                {
                    return NerdWordCheck(word.Substring(0, i), i) && NerdWordCheck(word.Substring(i + 1), length - i - 1);
                }
            }

            //Otherwise returning false
            return false;
        }
    }
}