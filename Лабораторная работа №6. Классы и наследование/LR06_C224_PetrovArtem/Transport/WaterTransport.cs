using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport
{
    internal class WaterTransport : Transport
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
            if (Capacity >= 1 && Capacity <= 4)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Водный транспорт предназначен для использования от 1 до 4 человек");
            }
            if (Capacity >= 5 && Capacity <= 10)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Водный транспорт предназначен для использования от 5 до 10 человек");
            }
            if (Capacity >= 11)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Водный транспорт предназначен для туров и экскурсий от 11 человек");
            }
        }
        public override void InputCapacity(int capacity, string type)
        {
            Capacity = capacity;
        }
        protected override string StatusLife()
        {
            if (Life > 0 && Life <= 5)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                return "Водный транспорт прослужил немного";
            }
            if (Life >= 6 && Life <= 9)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                return "Водный транспорт почти не годен для использования";
            }
            Console.ForegroundColor = ConsoleColor.Green;
            return "Водный транспорт уже не годен!";
        }
        public WaterTransport()
        {
            Type = "Водный транспорт";
            Weight = 5;
            Brand = "Woltswagen";
            Capacity = 4;
            Life = 3;
        }
        public WaterTransport(string type, string brand, double weight, int capacity, int life) : this()
        {
            Type = type;
            Brand = brand;
            Weight = weight;
            Capacity = capacity;
            Life = life;
        }
    }
}
