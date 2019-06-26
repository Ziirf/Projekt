using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt
{
    class Tool
    {
        public static void Write(int left, int top, string text, ConsoleColor color = ConsoleColor.Gray)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.SetCursorPosition(left, top);
            Console.Write(text);
            Console.ForegroundColor = oldColor;
        }

        public static string BuildString(int left, int top, int lenght, string output = "")
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

            } while (cki.Key != ConsoleKey.Enter);

            ColorChange();
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', lenght));
            Console.SetCursorPosition(left, top);
            Console.Write(output);

            return output;
        }

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

        public static string FormatString(int[] buffer, string[] input)
        {
            string output = "";
            int currentStringLength = 0;

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

        public static string BuildPascalCase(string input)
        {
            if (input.Length == 0)
                return input;
            string firstLetter = input.Substring(0, 1).ToUpper();
            string rest = input.Substring(1, input.Length - 1).ToLower();

            return firstLetter + rest;
        }
    }
}
