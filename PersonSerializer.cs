using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ConsoleSharpProb
{
    static class PersonSerializer
    {
        private static XmlSerializer _xml = new XmlSerializer(typeof(List<Person>));

        //Метод сохранения List<Person> как xml/json/txt
        public static void SaveAs(this List<Person> persons, string fpath)
        {
            string extention = Path.GetExtension(fpath);
            using (FileStream fs = new FileStream(fpath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                switch (extention)
                {
                    case ".xml":
                        _xml.Serialize(fs, persons);
                        break;
                    case ".json":
                        string js_str = JsonConvert.SerializeObject(persons);
                        StreamReadWriter.Write(fs, js_str);
                        break;
                    case ".txt":
                        StreamReadWriter.Write(fs,
                            string.Join("\n", persons.Select(x => x.ToString())));
                        break;
                }
            }
        }

        //Метод извлечения List<Person> из xml/json/txt
        public static List<Person> ExtractFrom(string fpath)
        {
            string extention = Path.GetExtension(fpath);
            List<Person> persons = null;
            using (FileStream fs = new FileStream(fpath, FileMode.OpenOrCreate, FileAccess.Read))
            {
                switch (extention)
                {
                    case ".xml":
                        persons = _xml.Deserialize(fs) as List<Person>;
                        RemoveEndls(persons);
                        break;
                    case ".json":
                        string js_str = StreamReadWriter.Read(fs);
                        persons = JsonConvert.DeserializeObject<List<Person>>(js_str);
                        RemoveEndls(persons);
                        break;
                    case ".txt":
                        string str = StreamReadWriter.Read(fs);
                        string[] strs = str.Split('\n');
                        persons = Person.ListFromStrings(strs);
                        break;
                }
            }
            return persons;
        }

        //Метод убирает лишние символы '\n' из имен
        private static void RemoveEndls(List<Person> persons)
        {
            foreach (Person p in persons)
                if (p.Name.Contains('\n'))
                    p.Name = p.Name.Replace("\n", String.Empty);
        }
    }
}
