using System;
using System.Collections.Generic;

namespace ConsoleSharpProb
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }

        public Person() { }
        public Person(string name, int age, int height)
        {
            this.Name = name;
            this.Age = age;
            this.Height = height;
        }

        //Преобразование Person в массив строк
        public string[] ToStrings()
        {
            string[] strs = new string[3];
            strs[0] = Name;
            strs[1] = Age.ToString();
            strs[2] = Height.ToString();
            return strs;
        }

        //Преобразование из строки в Person
        public static Person FromString(string str) =>
            FromStrings(str.Split('\n'));

        //Преобразование из строк в Person
        public static Person FromStrings(string[] fields)
        {
            string name;
            int age, height;
            name = fields[0].Replace("\n", String.Empty);
            age = Convert.ToInt32(fields[1]);
            height = Convert.ToInt32(fields[2]);
            return new Person(name, age, height);
        }

        //Преобразование из строк в List<Person>
        public static List<Person> ListFromStrings(string[] fields)
        {
            List<Person> persons = new List<Person>(fields.Length / 3);
            for (int i = 0; i < fields.Length; i += 3)
                persons.Add(
                    FromStrings(
                        fields.SubArray(i, 3)));
            return persons;
        }
    }
}
