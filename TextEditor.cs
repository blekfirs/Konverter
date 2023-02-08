using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSharpProb
{
    public static class TextEditor
    {
        //Символ указателя позиции в тексте
        private const char _pos_simb = '|';
        public static string[] Edit(string[] text)
        {
            //изначальная позиция в конце первой строки
            TextPoint t_point = new TextPoint(0, text[0].Length - 1);
            while (true)
            {
                //отправляем в ShowText копию текста, чтоб указатель курсора не записался в тексе
                ShowText((string[])text.Clone(), t_point);
                ConsoleKeyInfo kInfo = Console.ReadKey();
                string curr_line = text[t_point.line];

                //Королевский switch-case XD
                //Стрелками перемещение по тексту
                //Клавиши Backspace, Delete, Space для редактирования
                //default для символов
                //if-else'ы для не допуска выхода за пределы массива
                switch (kInfo.Key)
                {
                    case ConsoleKey.Escape:
                        return null;
                    case ConsoleKey.Enter:
                        return text;
                    case ConsoleKey.UpArrow:
                        if (t_point.line - 1 < 0)
                            t_point.line = 0;
                        else
                            t_point.line--;
                        if (t_point.position >= text[t_point.line].Length - 1)
                            t_point.position = text[t_point.line].Length;
                        break;
                    case ConsoleKey.DownArrow:
                        if (t_point.line >= text.Length - 1)
                            t_point.line = text.Length - 1;
                        else
                            t_point.line++;
                        if (t_point.position >= text[t_point.line].Length - 1)
                            t_point.position = text[t_point.line].Length;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (t_point.position <= 0)
                            t_point.position = 0;
                        else
                            t_point.position--;
                        break;
                    case ConsoleKey.RightArrow:
                        t_point.position++;
                        break;
                    case ConsoleKey.Backspace:
                        text[t_point.line] =
                            curr_line.Remove(t_point.position - 1, 1);
                        t_point.position--;
                        break;
                    case ConsoleKey.Delete:
                        if (t_point.position < curr_line.Length - 1)
                            text[t_point.line] =
                                curr_line.Remove(t_point.position, 1);
                        break;
                    default:
                        char symb = kInfo.KeyChar;
                        if (!(char.IsLetterOrDigit(symb) || symb == ' '))
                            break;
                        text[t_point.line] =
                            curr_line.Insert(t_point.position, symb.ToString());
                        t_point.position++;
                        break;
                }
            }
        }

        //Метод отображения текста с с курсором в точке t_point
        private static void ShowText(string[] text, TextPoint t_point)
        {
            string description =
                "Enter - сохранить, Escape - выйти без сохранения\n" +
                "Клавиши Backspace, Delete, Space для редактирования текста\n" +
                new string('=', 100);
            //Если позиция указателя больше длинны строки - расширяем строку до нужного размера 
            if (t_point.position >= text[t_point.line].Length)
                text[t_point.line] = new string(' ', t_point.position).Insert(0, text[t_point.line]);
            text[t_point.line] =
                text[t_point.line].Insert(t_point.position, _pos_simb.ToString());

            MenuConsole.WriteLinesAndDescription(description, text);
        }

        //точка - позиция курсора в тексте 
        private struct TextPoint
        {
            public int line;
            public int position;

            public TextPoint(int line, int position)
            {
                this.line = line;
                this.position = position;
            }
        }
    }
}
