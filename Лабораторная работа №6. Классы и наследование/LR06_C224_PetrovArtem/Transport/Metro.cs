using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport
{
    internal class Metro : Transport
    {
        protected string type = "";
        public string Type
        {
            get
            {
                if (string.IsNullOrEmpty(type))
                    return "Без типа";
                return type;
            }
            set { type = value; }
        }
        public override void WriteInfo()
        {
            Draw(color);
            Console.Write($"ID транспорта - {id}, ");
            Console.Write($"марка транспорта - {Brand}, ");
            Console.WriteLine($"тип транспорта - {Type}.");
            Console.Write($"Максимальная скорость - {Speed} км/ч, вес - {Weight} тонн, ");
            Console.WriteLine($"грузоподъёмность транпортного средства - {Capacity}.");
            Console.WriteLine($"Срок службы - {Life} лет.");
            Console.WriteLine($"Статус - {StatusLife()}.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override void FindCapacity()
        {
            if (Capacity > 0 && Capacity < 13)
                Console.WriteLine("Вагон типа F");
            else if (Capacity > 12 && Capacity < 24)
                Console.WriteLine("Вагон типа E");
            else if (Capacity > 23 && Capacity < 30)
                Console.WriteLine("Вагон типа D");
        }
        public override void InputCapacity(int capacity, string type)
        {
            Capacity = capacity;
        }
        protected override string StatusLife()
        {
            if (Life > 0 && Life <= 7)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                return "Поезд метро годен для перевозки пассажиров";
            }
            if (Life >= 8 && Life <= 14)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Поезд метро почти не годен для перевозки пассажиров";
            }
            Console.ForegroundColor = ConsoleColor.Green;
            return "Аэротранспорт уже не годен для использования!";
        }
        public Metro()
        {
            Type = "Вагон метро";
            Brand = "Вагон типа D";
            Life = 5;
            Capacity = 24;
            Speed = 55;
            Weight = 8;
        }
        public Metro(string type, string brand, int life, int capacity, double speed, double weight) : this()
        {
            Type = type;
            Brand = brand;
            Life = life;
            Capacity = capacity;
            Speed = speed;
            Weight = weight;
        }
    }
}
