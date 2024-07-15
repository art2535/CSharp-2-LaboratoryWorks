using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Transport
{
    internal class PassengerCar : Transport
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
            Console.Write($"Максимальная скорость - {Speed} км/ч, ");
            Console.WriteLine($"грузоподъёмность транпортного средства - {Capacity}.");
            Console.WriteLine($"Срок службы - {Life} лет.");
            Console.WriteLine($"Статус - {StatusLife()}.");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public override void FindCapacity()
        {
            int minCapacity = 1;
            int maxCapacity = 10;
            if (Capacity >= minCapacity && Capacity <= maxCapacity)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Все ОК! Грузоподъёмность нормальная и попадает в интервал от {minCapacity} до {maxCapacity}!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Грузоподъёмность не попадает в интервал от {minCapacity} до {maxCapacity}!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public override void InputCapacity(int capacity, string type)
        {
            Capacity = capacity;
        }
        protected override string StatusLife()
        {
            if (Life >= 0 && Life < 5)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                return "Транспортное средство прослужило немного";
            }
            if (Life >= 5 && Life < 10)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                return "Транспортное средство прослужило средний срок службы";
            }
            if (Life >= 10 && Life < 20)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return "Транспортное средство прослужило долго";
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            return "Транспортное средство не прослужило срок службы";
        }
        public static PassengerCar operator ++(PassengerCar car)
        {
            car.maxLoadCapacity++;
            return car;
        }
        public static PassengerCar operator --(PassengerCar car)
        {
            car.maxLoadCapacity--;
            return car;
        }
        public static PassengerCar operator +(PassengerCar car, int n)
        {
            car.serviceLife += n;
            return car;
        }
        public static PassengerCar operator +(int n, PassengerCar car)
        {
            car.serviceLife += n;
            return car;
        }
        public static PassengerCar operator -(PassengerCar car, int n)
        {
            car.serviceLife -= n;
            return car;
        }
        public static PassengerCar operator -(int n, PassengerCar car)
        {
            car.serviceLife -= n;
            return car;
        }
        public static bool operator >(PassengerCar car1, PassengerCar car2)
        {
            return car1.Life > car2.Life;
        }
        public static bool operator <(PassengerCar car1, PassengerCar car2)
        {
            return !(car1 > car2);
        }
        public static implicit operator int(PassengerCar car)
        {
            return (int)car.speed;
        }
        public static explicit operator PassengerCar(int n)
        {
            return new PassengerCar("", "Volvo", n * 2, n, n % 5);
        }

        public override string ToString()
        {
            return "Я - " + Brand + ", " + StatusLife();
        }
        public PassengerCar()
        {
            Type = "Легковая машина";
            Brand = "Volvo";
            Capacity = 3;
            Life = 5;
            Speed = 60;
        }
        public PassengerCar(string type, string brand, double speed, int life, int capacity) : this()
        {
            Type = type;
            Brand = brand;
            Life = life;
            Speed = speed;
            Capacity = capacity;
        }
    }
}
