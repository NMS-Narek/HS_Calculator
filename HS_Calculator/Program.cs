using System;

namespace HS_Calculator
{
    class Program
    {
        static bool bl;
        static int counter = 1;
        static int endnumberindex = 0;
        static string s;
        static int index = 0, endnumber = 0;
        static int size = 0;
        static void Main()
        {
            Console.Write("Enter the text  ");
            s = Console.ReadLine();
            if (s[0] == '+' || s[0] == '-') s = "0" + s;
            size = s.Length;

            bl = false;
            do
            {
                bl = CheckString();
                if (bl == true) break;
                else
                {
                    Console.WriteLine("The text is incorrect !!! Please rewrite it ");
                    s = Console.ReadLine();
                    if (s[0] == '+' || s[0] == '-') s = "0" + s;
                    size = s.Length;
                }
            } while (true);

            for (int j = 0; j <= counter - 1; j++)
            {
                EditString();
                Console.WriteLine(s + " ==>");
            }
            Console.WriteLine("-------------------");
            double result = ReadString(0);
            do
            {
                char ch = SymbolChar(s[index]);
                switch (ch)
                {
                    case '+':
                        index++;
                        result += ReadString(index);
                        break;
                    case '-':
                        index++;
                        result -= ReadString(index);
                        break;
                    default:
                        index++;
                        break;
                }
            } while (index != size - 1);
            Console.WriteLine(result);

            Console.Write("Write yes to repeat::");
            string Continue = Console.ReadLine();
            if (Continue == "Yes" || Continue == "yes")
            {
                counter = 1;
                endnumberindex = 0;
                index = 0;
                endnumber = 0;
                bl = false;
                Main();
            }
        }
        static char NumbersChar(char ch)
        {
            switch (ch)
            {
                case '0':
                    ch = '0';
                    break;
                case '1':
                    ch = '1';
                    break;
                case '2':
                    ch = '2';
                    break;
                case '3':
                    ch = '3';
                    break;
                case '4':
                    ch = '4';
                    break;
                case '5':
                    ch = '5';
                    break;
                case '6':
                    ch = '6';
                    break;
                case '7':
                    ch = '7';
                    break;
                case '8':
                    ch = '8';
                    break;
                case '9':
                    ch = '9';
                    break;
                case '.':
                    ch = '.';
                    break;
                default:
                    ch = '?';
                    break;
            }
            return ch;
        }
        static char SymbolChar(char ch)
        {
            switch (ch)
            {
                case '+':
                    ch = '+';
                    break;
                case '-':
                    ch = '-';
                    break;
                case '*':
                    ch = '*';
                    break;
                    ch = ',';
                case '/':
                    ch = '/';
                    break;
                default:
                    ch = '!';
                    break;
            }
            return ch;
        }
        static double ReadString(int startindex)
        {
            double number = 0;
            int k = startindex;

            char ch = '!';
            for (int i = startindex; i < size; i++)
            {
                endnumberindex = i;
                if (i == size - 1) endnumber = size - startindex;
                ch = NumbersChar(s[i]);
                if (ch == '.') continue;
                if (ch == '?' && i != startindex)
                {
                    endnumberindex = i - 1;
                    endnumber = i - startindex;
                    ch = SymbolChar(s[i]);
                    break;
                }
                if (ch == '?' && i != size - 1)
                {
                    ch = SymbolChar(s[i]);
                    if (ch == '!') break;
                    else continue;
                }
                if (ch == '?' && i == size - 1)
                {
                    ch = '!'; break;
                }
            }
            if (ch != '!')
            {
                string st = default;
                st = s.Substring(startindex, endnumber);
                number = Convert.ToDouble(st);
            }
            return number;
        }
        static void EditString()
        {
            double result = 0;
            bool b = false;
            char ch = ' ';
            int firstnumberindex = 0;
            int endnumberindex1 = 0;
            for (int i = 0; i < size; i++)
            {
                ch = NumbersChar(s[i]);
                if (ch != '?' && b == false)
                {
                    b = true;
                    firstnumberindex = i;
                    continue;
                }
                if (ch != '?' && b == true) continue;
                if (ch == '?')
                {
                    endnumberindex1 = i - 1;
                    ch = SymbolChar(s[i]);
                }
                if (ch == '+' || ch == '-')
                {
                    b = false; continue;
                }
                if (ch == '*' || ch == '/' && b == true)
                {
                    counter++;
                    break;
                }
            }
            if (ch == '*' || ch == '/' && b == true)
            {
                result = ReadString(firstnumberindex);
                if (ch == '*')
                {
                    result *= ReadString(endnumberindex1 += 2);
                    string st;
                    if (endnumberindex != size - 1) st = s.Substring(0, firstnumberindex) + Convert.ToString(result) + s.Substring(endnumberindex + 1, size - 1 - endnumberindex);
                    else
                        st = s.Substring(0, firstnumberindex) + Convert.ToString(result);
                    s = st;
                    size = s.Length;
                }
                if (ch == '/')
                {
                    double a = ReadString(endnumberindex1 += 2);
                    if (a != 0)
                    {
                        result /= a;
                        string st;
                        if (endnumberindex != size - 1) st = s.Substring(0, firstnumberindex) + Convert.ToString(result) + s.Substring(endnumberindex + 1, size - 1 - endnumberindex);
                        else
                            st = s.Substring(0, firstnumberindex) + Convert.ToString(result);
                        s = st;
                        size = s.Length;
                    }
                    else
                    {
                        Console.WriteLine("!!! The denominator cannot be equal to zero.");
                        Console.WriteLine("The text is incorrect !!! Please rewrite it ");
                        endnumberindex = 0;
                        endnumber = 0;
                        counter = 1;
                        s = Console.ReadLine();
                        if (s[0] == '+' || s[0] == '-') s = "0" + s;
                        size = s.Length;
                        EditString();
                    }
                }
            }
        }
        static bool CheckString()
        {
            bool result = false;
            char ch = ' ';
            for (int j = 0; j < size; j++)
            {
                ch = NumbersChar(s[j]);
                if (ch == '?') ch = SymbolChar(s[j]);
                if (ch == '!') break;
            }
            if (ch == '!') result = false;
            else result = true;
            return result;
        }
    }
}
