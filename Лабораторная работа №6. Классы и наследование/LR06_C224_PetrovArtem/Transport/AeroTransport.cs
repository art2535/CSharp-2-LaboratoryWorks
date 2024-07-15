using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport
{
    internal class AeroTransport : Transport
    {
        protected string type = "";
        protected string company = "";
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
        public string Company
        {
            get
            {
                if (string.IsNullOrEmpty(company))
                    return "Без компании";
                return company;
            }
            set { company = value; }
        }
        public override void WriteInfo()
        {
            Draw(color);
            Console.Write($"ID транспорта - {id}, ");
            Console.Write($"марка транспорта - {Brand}, ");
            Console.WriteLine($"тип транспорта - {Type}.");
            Console.Write($"Максимальная скорость - {Speed} км/ч, вес - {Weight} тонн, ");
            Console.WriteLine($"грузоподъёмность транпортного средства - {Capacity}.");
            Console.WriteLine($"Авиакомания - {Company}");
            Console.WriteLine($"Срок службы - {Life} лет.");
            Console.WriteLine($"Статус - {StatusLife()}.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override void FindCapacity()
        {
            if (Capacity >= 1 && Capacity <= 22)
            {
                Console.WriteLine($"{Type} предназначен для больших авиаполётов");
            }
            if (Capacity >= 23 && Capacity <= 49)
            {
                Console.WriteLine($"{Type} предназначен для больших авиаполётов");
            }
            if (Capacity >= 50)
            {
                Console.WriteLine($"{Type} предназначен для больших авиаполётов");
            }
        }
        public override void InputCapacity(int capacity, string type)
        {
            Capacity = capacity;
        }
        protected override string StatusLife()
        {
            if (Life > 0 && Life <= 8)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                return "Аэротранспорт прослужил немного";
            }
            if (Life >= 9 && Life <= 13)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return "Аэротранспорт почти не годен для использования";
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            return "Аэротранспорт уже не годен!";
        }
        public AeroTransport()
        {
            Type = "Самолёт";
            Company = "AeroFlot";
            Weight = 100;
            Speed = 196;
            Capacity = 50;
            Life = 4;
        }
        public AeroTransport(string type, string company, double weight, double speed, int capacity, int life) : this()
        {
            Type = type;
            Company = company;
            Weight = weight;
            Speed = speed;
            Capacity = capacity;
            Life = life;
        }
    }
}
