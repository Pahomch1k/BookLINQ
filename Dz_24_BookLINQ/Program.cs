using System;
using System.Collections.Generic;
using System.Linq; 
using static System.Console;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization; 
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Dz_24_BookLINQ
{
    class Program
    {
        static void Init(List<Book> books)
        {
            WriteLine("Сколько книг инит?");
            int x; string n;
            x = Convert.ToInt32(ReadLine());

            for (int i = 0; i < x; i++)
            {
                int y = 0;
                Book b = new Book();
                Write("Название? "); n = ReadLine();
                b.name = n;

                WriteLine("Сколько Авторов?");
                y = Convert.ToInt32(ReadLine());
                for (int j = 0; j < y; j++)
                {
                    Write("Автор? "); n = ReadLine();
                    b.avtor.Add(n);
                }

                WriteLine("Сколько Категорий?");
                y = Convert.ToInt32(ReadLine());
                for (int j = 0; j < y; j++)
                {
                    Write("Категория? "); n = ReadLine();
                    b.kategory.Add(n);
                }

                Write("Цена? "); y = Convert.ToInt32(ReadLine());
                b.price = y;

                books.Add(b);
            }
        }

        static void Group(List<Book> books)
        {
            WriteLine("Сгруппировать\n" +
                "1. По автору\n" +
                "2. По категории\n" +
                "3. По цене\n" + 
                "4. По названию\n" +
                "5. Среднее арифм по цене\n" +
                "6. Соимость книг арвтора\n" + 
                "7. Выход\n");

            int x;
            x = Convert.ToInt32(ReadLine());

            switch (x)
            {
                case 1:
                    {
                        var orderedNumbers = from i in books
                                             orderby i.avtor ascending
                                             select i;
                        foreach (var i in orderedNumbers)
                            WriteLine(i);
                        WriteLine();
                    } break;
                case 2:
                    {
                        var orderedNumbers = from i in books
                                             orderby i.kategory ascending
                                             select i;
                        foreach (var i in orderedNumbers)
                            WriteLine(i);
                        WriteLine();
                    } break;
                case 3:
                    {
                        var orderedNumbers = from i in books
                                             orderby i.price ascending
                                             select i;
                        foreach (var i in orderedNumbers)
                            WriteLine(i);
                        WriteLine();
                    } break;
                case 4:
                    {
                        var orderedNumbers = from i in books
                                             orderby i.name ascending
                                             select i;
                        foreach (var i in orderedNumbers)
                            WriteLine(i);
                        WriteLine(); 
                    } break;
                case 5:
                    {
                        double avr = books.Average(n => n.price); 
                        WriteLine("Средняя цена - " + avr);
                        WriteLine();
                    } break;
                case 6:
                    {
                        Write("Avtor? ");
                        string o;
                        o = ReadLine();

                        var orderedNumbers = from i in books
                                             from kat in i.avtor
                                             where kat == o
                                             orderby i.price ascending
                                             select i;
                        foreach (var i in orderedNumbers)
                            WriteLine(i);
                        WriteLine();
                    } break; 
                case 7: break;
                default: break;
            }
        }

        static void Print(List<Book> books)
        { 
            WriteLine("Показать\n" +
                "1. Все\n" +
                "2. По цене\n" +
                "3. По категории\n" +
                "4. По кол-во авторов\n" +
                "5. По автору\n" +
                "6. Самаядорогая книга\n" +
                "7. По названию\n" +
                "8. По названию и авторам\n" +
                "9. По названию и автору\n" +
                "10. По названию и цене больше 100\n" +
                "11. Выход\n");

            int x;
            x = Convert.ToInt32(ReadLine());

            switch (x)
            {
                case 1:
                    {
                        for (int i = 0; i < books.Count; i++)
                            WriteLine(i + 1 + books[i].ToString());
                    }
                    break;
                case 2:
                    {
                        Write("Цена от? ");
                        int t;
                        t = Convert.ToInt32(ReadLine());

                        var Prices = from pr in books
                                     where pr.price > t
                                     select pr;
                        foreach (var user in Prices)
                            WriteLine(books.ToString());
                        WriteLine();
                    }
                    break;
                case 3:
                    {
                        Write("Категория? ");
                        string t;
                        t = ReadLine();

                        var Kategory = from bk in books
                                       from kat in bk.kategory
                                       where kat == t
                                       select bk;
                        foreach (var user in Kategory)
                            WriteLine(books.ToString());
                        WriteLine();
                    }
                    break;
                case 4:
                    {
                        Write("Сколько авторов? ");
                        int t;
                        t = Convert.ToInt32(ReadLine());

                        var Avtors = from avs in books
                                     where avs.avtor.Count == t
                                     select avs;
                        foreach (var user in Avtors)
                            WriteLine(books.ToString());
                        WriteLine();
                    }
                    break;
                case 5:
                    {
                        Write("Автор? ");
                        string t;
                        t = ReadLine();

                        var Avtor = from av in books
                                    from avt in av.avtor
                                    where avt == t
                                    select av;
                        foreach (var user in Avtor)
                            WriteLine(books.ToString());
                        WriteLine();
                    }
                    break;
                case 6:
                    {
                        int r = 0;
                        for (int i = 0; i < books.Count; i++)
                        {
                            if (i == 0) r = 0;
                            else if (books[i].price > books[i - 1].price) r = i;
                        }
                        WriteLine(books[r].ToString());
                    }
                    break;
                case 7:
                    {
                        Write("Название? ");
                        string t;
                        t = ReadLine();

                        var Name = from nm in books
                                   where nm.name == t
                                   select nm;
                        foreach (var user in Name)
                            WriteLine(books.ToString());
                        WriteLine();
                    }
                    break;
                case 8:
                    {
                        var Name1 = from nm in books
                                    select new
                                    {
                                        Name = nm.name,
                                        Avtors = nm.avtor
                                    };

                        foreach (var n in Name1)
                            WriteLine($"Название - {n.Name}\nАвторы - {n.Avtors}");
                        WriteLine();
                    }
                    break;
                case 9:
                    {
                        var Name1 = from nm in books
                                    where nm.avtor.Count < 2
                                    select new
                                    {
                                        Name = nm.name,
                                        Avtors = nm.avtor
                                    };
                        foreach (var n in Name1)
                            WriteLine($"Название - {n.Name}\nАвторы - {n.Avtors}");
                        WriteLine();
                    }
                    break;
                case 10:
                    {
                        var Name = from nm in books
                                   where nm.price > 100
                                   select new
                                   {
                                       Name = nm.name,
                                       Price = nm.price
                                   };
                        foreach (var n in Name)
                            WriteLine($"Название - {n.Name}\nАвторы - {n.Price}");
                        WriteLine();
                    }
                    break;
                case 11: break;
                default: break;
            }
        }

        static void Serialise(List<Book> books)
        {
            //char answer;
            FileStream stream = null;
            XmlSerializer serializer = null; 
            DataContractJsonSerializer jsonFormatter = null;  

            WriteLine("Сгруппировать\n" +
                "1. XML-сериализация объекта\n" +
                "2. XML-сериализация коллекции\n" +
                "3. XML-десериализация объекта\n" +
                "4. JSON-сериализация объекта\n" +
                "5. JSON-десериализация объекта\n" +
                "6. JSON-сериализация коллекции\n" +
                "7. Выход\n");

            int x;
            x = Convert.ToInt32(ReadLine());

            switch (x)
            {
                case 1:
                    {
                        //h = new Book("Ларри Пейдж","qweqwe", 42);
                        stream = new FileStream("../../data.xml", FileMode.Create);
                        serializer = new XmlSerializer(typeof(Book));
                        serializer.Serialize(stream, books[0]);
                        stream.Close();
                        WriteLine("Сериализация успешно выполнена!");
                    }
                    break;
                case 2:
                    {
                        stream = new FileStream("../../list.xml", FileMode.Create);
                        serializer = new XmlSerializer(typeof(List<int>));
                        serializer.Serialize(stream, books);
                        stream.Close();
                        WriteLine("Сериализация успешно выполнена!");
                    }
                    break;
                case 3:
                    {
                        stream = new FileStream("../../data.xml", FileMode.Open);
                        serializer = new XmlSerializer(typeof(Book));
                        Book h = (Book)serializer.Deserialize(stream);
                        WriteLine(h.name + "	" + h.avtor + "	" + h.price);
                        stream.Close();
                    }
                    break;
                case 4:
                    { 
                        stream = new FileStream("../../data.json", FileMode.Create);
                        jsonFormatter = new DataContractJsonSerializer(typeof(Book));
                        jsonFormatter.WriteObject(stream, books[0]);
                        stream.Close();
                        WriteLine("Сериализация успешно выполнена!");
                    }
                    break;
                case 5:
                    {
                        stream = new FileStream("../../data.json", FileMode.Open);
                        jsonFormatter = new DataContractJsonSerializer(typeof(Book));
                        Book h = (Book)jsonFormatter.ReadObject(stream);
                        WriteLine(h.name + "	" + h.avtor + "	" + h.price);
                        stream.Close();
                    }
                    break;
                case 6:
                    {
                        stream = new FileStream("../../list.json", FileMode.Create);
                        jsonFormatter = new DataContractJsonSerializer(typeof(List<int>));
                        jsonFormatter.WriteObject(stream, books);
                        stream.Close();
                        WriteLine("Сериализация успешно выполнена!");
                    }
                    break;
                case 7: break;
                default: break;
            }
        }

        static void Main(string[] args)
        {
            List<Book> books = new List<Book>();
            Init(books);

            WriteLine("1. Print()\n2. Group\n3. Serialise\n");
            int x;
            x = Convert.ToInt32(ReadLine());

            switch (x)
            {
                case 1: Print(books);
                    break;
                case 2: Group(books);
                    break;
                case 3: Serialise(books);
                    break;
                default:
                    break;
            } 
        }
    }

    [Serializable]
    [DataContract]
    class Book
    {
        [DataMember]
        public string name { get; set; }

        [DataMember]
        public List<string> avtor { get; set; }

        [DataMember] 
        public List<string> kategory { get; set; }

        [DataMember]
        public int price { get; set; }

        public Book()
        {
            avtor = new List<string>();
            kategory = new List<string>();
        }

        public Book(string a, string n, int p)
        {
            avtor = new List<string>();
            kategory = new List<string>();

            avtor.Add(a);
            name = n;
            price = p;
        }

        public override string ToString()
        {
            string n;

            n = "Название - " + name + "\nЦена - " + price + "\n";
            n += "Категории - ";
            for (int i = 0; i < kategory.Count; i++)
                n += kategory[i] + ",";
            n += "\n";
            n += "Автор - ";
            for (int i = 0; i < avtor.Count; i++)
                n += avtor[i] + ",";
            n += "\n";
            return n;
        }
    }
}