using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Tool
    {
        // Writes at a specified location with a specified font color
        public static void Write(int left, int top, string text, ConsoleColor color = ConsoleColor.Gray)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left, top);
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        // Returns a string, that is typed at a specified location and with a optional starting string
        public static string BuildString(int left, int top, int lenght, string output = "")
        {
            // Clears the location that the string is typed at, and inverts the colors of foreground and background to highlight the editting area
            Console.SetCursorPosition(left, top);
            ColorChange(false);
            Console.Write(new string(' ', lenght));

            ConsoleKeyInfo cki;
            do
            {
                // writes the string
                Console.SetCursorPosition(left, top);
                Console.WriteLine(output);

                // sets the cursor at the back of ths tring, and waits for a read key
                Console.SetCursorPosition(left + output.Length, top);
                cki = Console.ReadKey(true);

                // only allow letter digits or spacebar to be added to the string
                if ((Char.IsLetterOrDigit(cki.KeyChar) || cki.Key == ConsoleKey.Spacebar) && output.Length < lenght)
                {
                    output += cki.KeyChar.ToString();
                }
                // and lets you delete one char at a time if you use backspace with a string that is above 0 characters
                else if (cki.Key == ConsoleKey.Backspace && output.Length > 0)
                {
                    output = output.Substring(0, output.Length - 1);
                    Console.SetCursorPosition(left + output.Length, top);
                    Console.Write(' ');
                }

                //The loop is broken if you hit enter
            } while (cki.Key != ConsoleKey.Enter);

            // change the colors back to the original colors and output the string
            ColorChange();
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', lenght));
            Console.SetCursorPosition(left, top);
            Console.Write(output);

            return output;
        }

        // same as the previous method, but with int instead of string
        public static int BuildInt(int left, int top, int lenght, string output = "")
        {
            Console.SetCursorPosition(left, top);
            ColorChange(false);
            Console.Write(new string(' ', lenght));
            ConsoleKeyInfo cki;

            do
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine(output);

                Console.SetCursorPosition(left + output.Length, top);
                cki = Console.ReadKey(true);

                if ((Char.IsDigit(cki.KeyChar)) && output.Length < lenght)
                {
                    output += cki.KeyChar.ToString();
                }
                else if (cki.Key == ConsoleKey.Backspace && output.Length > 0)
                {
                    output = output.Substring(0, output.Length - 1);
                    Console.SetCursorPosition(left + output.Length, top);
                    Console.Write(' ');
                }
            } while (cki.Key != ConsoleKey.Enter || output.Length <= 0);

            ColorChange();
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', lenght));
            Console.SetCursorPosition(left, top);
            Console.Write(output);

            return Convert.ToInt32(output);
        }

        // same as above, but it also accepts @ and . to make emails
        public static string BuildEmail(int left, int top, int lenght, string output = "")
        {
            Console.SetCursorPosition(left, top);
            ColorChange(false);
            Console.Write(new string(' ', lenght));
            ConsoleKeyInfo cki;

            do
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine(output);

                Console.SetCursorPosition(left + output.Length, top);
                cki = Console.ReadKey(true);

                if ((Char.IsLetterOrDigit(cki.KeyChar) || cki.KeyChar == '@' || cki.KeyChar == '.') && output.Length < lenght)
                {
                    output += cki.KeyChar.ToString();
                }
                else if (cki.Key == ConsoleKey.Backspace && output.Length > 0)
                {
                    output = output.Substring(0, output.Length - 1);
                    Console.SetCursorPosition(left + output.Length, top);
                    Console.Write(' ');
                }
            } while (cki.Key != ConsoleKey.Enter);

            ColorChange();
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', lenght));
            Console.SetCursorPosition(left, top);
            Console.Write(output);

            return output;
        }

        // Combine all the information written into one long string, with specified spaces between words
        public static string FormatString(int[] buffer, string[] input)
        {
            // since we use "+=" the variable can't be null at start therefore we give it value of ""
            string output = "";
            int currentStringLength = 0;

            // begins a loop that goes through the two arrays
            for (int i = 0; i < buffer.Length; i++)
            {
                // if the current stringLength is smaller than the value of the buffer, it will fill the string with ' ' so they match
                if (buffer[i] > currentStringLength)
                {
                    int temp = buffer[i] - currentStringLength;
                    output += new string(' ', temp);
                }
                // if the current stringLength is longer, it will delete from the string and place ".. " as to show that not all of the value is shown.
                else if (buffer[i] < currentStringLength)
                {
                    int temp = currentStringLength - buffer[i];
                    output = output.Substring(0, output.Length - (temp + 3));
                    output += ".. ";
                }
                // writes the input at the end of the string and set the currentStringLength at the length of the string
                output += input[i];
                currentStringLength = output.Length;
            }

            return output;
        }

        // a "toggle" that switches between grey background and back foreground, and inverse, bepending on the bool value
        private static void ColorChange(bool useOldColors = true)
        {
            ConsoleColor[] palette = { ConsoleColor.Black, ConsoleColor.Gray };

            if (useOldColors == true)
            {
                Console.ForegroundColor = palette[1];
                Console.BackgroundColor = palette[0];
            }
            else
            {
                Console.ForegroundColor = palette[0];
                Console.BackgroundColor = palette[1];
            }
        }

        // Takes an input and makes the first letter uppercase and the rest lowercase
        public static string BuildPascalCase(string input)
        {
            if (input.Length == 0)
                return input;
            string firstLetter = input.Substring(0, 1).ToUpper(); // takes the first letter and uses ".ToUpper"
            string rest = input.Substring(1, input.Length - 1).ToLower(); // takes the rest of the string and uses ".ToLower"

            return firstLetter + rest;
        }
    }
}
