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

        //public static string BuildString(int left, int top, int lenght)
        //{
        //    Console.SetCursorPosition(left, top);
        //    Console.Write(new string(' ', 20));
        //    string output = "";
        //    ConsoleKeyInfo cki;

        //    do
        //    {
        //        Console.SetCursorPosition(left + output.Length, top);
        //        cki = Console.ReadKey(true);
                
        //        if (Char.IsLetter(cki.KeyChar) && output.Length < lenght)
        //        {
        //            output += cki.KeyChar.ToString();
        //        }
        //        else if (cki.Key == ConsoleKey.Backspace && output.Length > 0)
        //        {
        //            output = output.Substring(0, output.Length - 1);
        //            Console.SetCursorPosition(left + output.Length, top);
        //            Console.Write(' ');
        //        }

        //        Console.SetCursorPosition(left, top);
        //        Console.WriteLine(output);
        //    } while (cki.Key != ConsoleKey.Enter);

        //    return output;
        //}

        public static string BuildString(int left, int top, int lenght)
        {
            Console.SetCursorPosition(left, top);
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

            return output;
        }

        public static int BuildInt(int left, int top, int lenght)
        {
            Console.SetCursorPosition(left, top);
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
            } while (cki.Key != ConsoleKey.Enter);


            return Convert.ToInt32(output);
        }

        public static string BuildEmail(int left, int top, int lenght)
        {
            Console.SetCursorPosition(left, top);
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

            return output;
        }
    }
}
