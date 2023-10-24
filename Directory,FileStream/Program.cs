using Directory_FileStream.Exceptions;
using Directory_FileStream.Utilities;
using Newtonsoft.Json;

namespace Directory_FileStream
{
    internal class Program
    {
        public static string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Files", "name.json");
        static void Main(string[] args)
        {
            restart:
            Console.WriteLine();
            Console.WriteLine("1.Add Name");
            Console.WriteLine("2.Remove Name");
            Console.WriteLine("3.Search");
            Console.WriteLine("0.Exit");
            var result = Console.ReadLine();
            Console.Clear();
            switch (result)
            {
                case "1":
                    AddName();
                    break;
                case "2":
                    RemoveName();
                    break;
                case "3":
                    try { SearchName(); }catch(Exception e) { Console.WriteLine(e.Message); }
                    break;
                case "0":
                    return;
            }
            goto restart;
        }

        public static void AddName()
        {
            Console.WriteLine("Enter the name");
        restart:
            string name = Console.ReadLine().Capitalize();
            if (name.Length < 3)
            {
                Console.WriteLine("Minimum length 3,please re-enter");
                goto restart;
            }
            Add(name);
            Console.WriteLine("Name successfully added");
        }
        public static void RemoveName()
        {
            Console.WriteLine("Enter the removing name");
            string name = Console.ReadLine();
            Remove(name);
            Console.WriteLine("Name successfully deleted");
        }
        public static void SearchName()
        {
            Console.WriteLine("Search:");
            string search = Console.ReadLine().Capitalize();
            SearchWriteConsole(search);
        }
        public static bool Search(string search)
        {
            var names = Pull();
            return names.Any(x => x.ToLower().Contains(search.ToLower()));
        }
        public static void SearchWriteConsole(string search)
        {
            var names = Pull();
            var output = names.Where(x => x.ToLower().Contains(search.ToLower())).ToList();
            if (output.Count is 0)
            {
                throw new NameNotFoundException();
            }
            output.ForEach(x => Console.WriteLine(x));
        }
        public static void Add(string name)
        {
            var names = Pull();
            names.Add(name);
            Push(names);
        }
        public static void Remove(string name)
        {
            var names = Pull();
            names.Remove(name);
            Push(names);
        }

        public static List<string> Pull()
        {
            List<string> list;
            if (!File.Exists(path))
                File.Create(path).Close();
            using (StreamReader sr = new StreamReader(path))
            {
                list = JsonConvert.DeserializeObject<List<string>>(sr.ReadToEnd());

            }
            if (list is null) list = new();
            return list;
        }
        public static void Push(List<string> list)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(JsonConvert.SerializeObject(list));
            }
            return;
        }
    }
}