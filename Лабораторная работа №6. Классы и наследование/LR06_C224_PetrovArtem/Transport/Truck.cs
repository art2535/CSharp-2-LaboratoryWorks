using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport
{
    internal class Truck : Transport    // грузовик
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
            Console.WriteLine($"тип транспорта - {Type}.");
            Console.Write($"Максимальная скорость - {Speed} км/ч, вес - {Weight} тонн, ");
            Console.WriteLine($"грузоподъёмность транпортного средства - {Capacity}.");
            Console.WriteLine($"Срок службы - {Life} лет.");
            Console.WriteLine($"Статус - {StatusLife()}.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override void FindCapacity()
        {
            if (Capacity == 1)
                Console.WriteLine("Грузоподъемность ОК");
            else
                Console.WriteLine("Грузоподъемность не ОК");
        }
        public override void InputCapacity(int capacity, string type)
        {
            if (type == "С прицепом")
                Capacity = capacity * 2;
            else 
                Capacity = capacity;
        }
        protected override string StatusLife()
        {
            return "Статус у транспортных средств данного класса не оперделяется";
        }
        public Truck()
        {
            Type = "";
            Speed = 50;
            Weight = 4;
            Life = 2;
            Capacity = 1;
        }
        public Truck(string type, double speed, double weight, int life, int capacity) : this()
        {
            Type = type;
            Speed = speed;
            Weight = weight;
            Life = life;
            Capacity = capacity;
        }
    }
}
