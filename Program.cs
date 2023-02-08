using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ConsoleSharpProb
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                List<Person> persons;
                string pathFrom, pathTo;

                MenuConsole.Write(
                    "Извлечение\n" +
                    new string('=', 100) + '\n' +
                    "Путь к файлу: ");
                pathFrom = Console.ReadLine();

                //Извлекаем List<Person> из файла
                try { 
                    persons = PersonSerializer.ExtractFrom(pathFrom); }
                catch (Exception ex) {
                    MenuConsole.WriteLine("Ошибка извлечения персоны из файла\n" + ex.Message);
                    Console.ReadKey();
                    continue;
                }

            choice_again:
                //Преобразуем все Person в массивы строк и записываем в один большой массив
                string[] str_persons = persons.SelectMany(x => x.ToStrings()).ToArray();
                MenuConsole.WriteLine(
                    "F1 - Сохранить в фалй, F2 - Редактировать, Escape - выйти\n" +
                    new string('=', 100) + '\n'+
                    string.Join("\n", str_persons));

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F1: break;
                    case ConsoleKey.F2:
                        //Редактируем массив строк (TextEditor.Edit),
                        //полученные строки преобразуем в List<Person> (Person.ListFromStrings)
                        persons = 
                            Person.ListFromStrings(
                                TextEditor.Edit(str_persons));
                        goto choice_again;
                    case ConsoleKey.Escape:
                        return;
                    default: 
                        goto choice_again;
                }

                MenuConsole.Write(
                    "Сохранеие\n" +
                    new string('=', 100) + '\n' +
                    "Путь к файлу: ");
                pathTo = Console.ReadLine();
                //Сохраняем в файл
                persons.SaveAs(pathTo);

                Console.WriteLine("Запись в файл прошла успешно");
                Console.ReadKey();
            }
        }
    }
}
