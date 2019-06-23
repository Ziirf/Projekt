using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Tool
    {
        public static bool CheckInt(string input)
        {
            bool parse = Int32.TryParse(input, out int output);
            if (parse)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string BuildString(int left, int top, int lenght)
        {
            Console.SetCursorPosition(left, top);
            ColorChange(false);
            Console.Write(new string(' ', lenght));
            string output = "";
            ConsoleKeyInfo cki;

            do
            {
                Console.SetCursorPosition(left + output.Length, top);
                cki = Console.ReadKey(true);

                if ((Char.IsLetterOrDigit(cki.KeyChar) || cki.Key == ConsoleKey.Spacebar) && output.Length < lenght)
                {
                    output += cki.KeyChar.ToString();
                }
                else if (cki.Key == ConsoleKey.Backspace && output.Length > 0)
                {
                    output = output.Substring(0, output.Length - 1);
                    Console.SetCursorPosition(left + output.Length, top);
                    Console.Write(' ');
                }

                Console.SetCursorPosition(left, top);
                Console.WriteLine(output);
            } while (cki.Key != ConsoleKey.Enter);

            ColorChange();
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', lenght));
            Console.SetCursorPosition(left, top);
            Console.Write(output);

            return output;
        }

        public static int BuildInt(int left, int top, int lenght)
        {
            Console.SetCursorPosition(left, top);
            ColorChange(false);
            Console.Write(new string(' ', lenght));
            string output = "";
            ConsoleKeyInfo cki;

            do
            {
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

                Console.SetCursorPosition(left, top);
                Console.WriteLine(output);
            } while (cki.Key != ConsoleKey.Enter && output.Length > 0);

            ColorChange();
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', lenght));
            Console.SetCursorPosition(left, top);
            Console.Write(output);

            return Convert.ToInt32(output);
        }

        public static string BuildEmail(int left, int top, int lenght)
        {
            Console.SetCursorPosition(left, top);
            ColorChange(false);
            Console.Write(new string(' ', lenght));
            string output = "";
            ConsoleKeyInfo cki;

            do
            {
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

                Console.SetCursorPosition(left, top);
                Console.WriteLine(output);
            } while (cki.Key != ConsoleKey.Enter);

            ColorChange();
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', lenght));
            Console.SetCursorPosition(left, top);
            Console.Write(output);

            return output;
        }

        public static string FormatString(int[] buffer, string[] input)
        {
            string output = "";
            int currentStringLength = 0;

            //string[] InfoArray = Info();
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] > currentStringLength)
                {
                    int temp = buffer[i] - currentStringLength;
                    output += new string(' ', temp);
                }
                else if (buffer[i] < currentStringLength)
                {
                    int temp = currentStringLength - buffer[i];
                    output = output.Substring(0, output.Length - temp);
                }
                output += input[i];
                currentStringLength = output.Length;
            }

            return output;
        }

        private static void ColorChange(bool useOldColors = true)
        {
            ConsoleColor[] palette = {ConsoleColor.Black, ConsoleColor.Gray };

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
    }
}
