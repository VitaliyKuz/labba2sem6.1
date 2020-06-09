
using System;
using System.Collections.Generic;
using System.Text;
namespace labba2sem6._1
{
    class Program
    {
        private static int n = int.Parse(Console.ReadLine());

        public static Student[] group = new Student[n];

        private static void Main(string[] args)
        {
            Input input = new Input();

            input.Read();
            
            Output output = new Output();

            output.Write();
        }
    }
}



namespace labba2sem6._1
{
    class Student
    {
        private string _surename;
        private string _place;
        private DateTime _birthday;
        
        public string Surename
        {
            get => _surename;
            set => _surename = value;
        }
        public DateTime Birthday
        {
            get => _birthday;
            set => _birthday = value;
        }

        public string Place
        {
            get => _place;

            set => _place = value;
        }

        public Student()
        {
            Surename = "Не вказано";
            Place = "Не вказано";
            Birthday = DateTime.Parse("01.01.01");
        }
    }
}


namespace labba2sem6._1
{
    interface IOutput
    {
        void WriteTable();

        Student[][] Twins();

        void WriteTwins(Student[][] twins);
    }
}





namespace labba2sem6._1
{
    interface IInput
    {
        void Read();

        void ReadTable();
    }
}




namespace labba2sem6._1
{
    class Input : IInput
    {
        public void Read()
        {
            ReadTable();
        }

        public void ReadTable()
        {
            Console.Write("Введiть данi про студентiв групи.\nКiлькiсть студентiв: ");

            for (int i = 0; i < Program.group.Length; ++i)
            {
                Program.group[i] = new Student();

                Console.WriteLine("Студент №{0}:", i + 1);

                Console.Write("Прiзвище: ");
                Program.group[i].Surename = Console.ReadLine();

                Retry:
                {
                    Console.Write("Дата народження: ");

                    if (DateTime.TryParse(Console.ReadLine(), out DateTime tempDate))
                    {
                        Program.group[i].Birthday = tempDate;
                    }
                    else
                    { 
                        Console.WriteLine("Неправильний формат!");
                        goto Retry;
                    }
                }

                Console.Write("Мiсце народження: ");
                Program.group[i].Place = Console.ReadLine();
            }
        }
    }
}





namespace labba2sem6._1
{
    class Output : IOutput
    {
        private bool isTwins = false;

        public void Write()
        {
            WriteTable();
            WriteTwins(Twins());
        }

        public void WriteTable()
        {
            Console.WriteLine("\n{0,-20}{1, -20}{2, -20}", "Прiзвище", "Дата народження", "Мiсце народження");

            for (int i = 0; i < Program.group.Length; ++i)
            {
                for (int j = 0; j < Program.group.Length - 1; ++j)
                {
                    if (Program.group[j].Birthday > Program.group[j + 1].Birthday)
                    {
                        string tempStr;
                        DateTime tempDate;

                        tempStr = Program.group[j].Surename;
                        Program.group[j].Surename = Program.group[j + 1].Surename;
                        Program.group[j + 1].Surename = tempStr;

                        tempDate = Program.group[j].Birthday;
                        Program.group[j].Birthday = Program.group[j + 1].Birthday;
                        Program.group[j + 1].Birthday = tempDate;

                        tempStr = Program.group[j].Place;
                        Program.group[j].Place = Program.group[j + 1].Place;
                        Program.group[j + 1].Place = tempStr;
                    }
                }
            }

            for (int i = 0; i < Program.group.Length; ++i)
            {
                Console.WriteLine("{0,-20}{1, -20}{2, -20}", Program.group[i].Surename, Program.group[i].Birthday.ToShortDateString(), Program.group[i].Place);
            }
        }

        public Student[][] Twins()
        {
            Student[][] twins = new Student[367][];

            int[] days = new int[367];

            for (int i = 0; i < 367; ++i)
            {
                days[i] = 0;
            }

            for (int i = 0; i < Program.group.Length; ++i)
            {
                ++days[Program.group[i].Birthday.DayOfYear];
            }

            for (int i = 0; i < 367; ++i)
            {
                twins[i] = new Student[days[i]];

                int index = 0;

                for (int j = 0; j < Program.group.Length; ++j)
                {
                    if (Program.group[j].Birthday.DayOfYear == i)
                    {
                        twins[i][index] = Program.group[j];

                        if (index > 0)
                        {
                            isTwins = true;
                        }

                        ++index;
                    }
                }
            }

            return twins;
        }

        public void WriteTwins(Student[][] twins)
        {
            if (isTwins)
            {
                Console.WriteLine("\nБлизнята:");
                Console.WriteLine("{0,-20}{1, -20}{2, -20}", "Прiзвище", "Дата народження", "Мiсце народження\n");

                for (int i = 0; i < twins.Length; ++i)
                {
                    if (twins[i].Length > 1)
                    {
                        for (int j = 0; j < twins[i].Length; ++j)
                        {
                            Console.WriteLine("{0,-20}{1, -20}{2, -20}", twins[i][j].Surename, twins[i][j].Birthday.ToShortDateString(), twins[i][j].Place);
                        }

                        Console.ReadKey();
                    }
                }
            }
        }
    }
}