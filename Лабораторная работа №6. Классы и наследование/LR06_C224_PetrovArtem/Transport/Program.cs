using System;
using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Threading;

namespace Transport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PassengerCar car = new PassengerCar();
            car.WriteInfo();
            car.ChangeSpeed(-1.4);
            Console.WriteLine();
            car.InputCapacity(5, "");
            car.WriteInfo();
            car.FindCapacity();
            Console.WriteLine($"Количество транспортных средств - {Transport.Count}");
            Console.WriteLine();
            PassengerCar car1 = new PassengerCar("", "Lada", 130, -2, 4);
            car1.WriteInfo();
            car1.ChangeSpeed(-50);
            car1.InputCapacity(12, "");
            car1.FindCapacity();
            Console.WriteLine();
            if (car > car1)
                Console.WriteLine("Машина 1 больше Машины 2");
            if (car1 < car)
                Console.WriteLine("Машина 2 меньше Машины 1");
            else
                Console.WriteLine("Это хрень!");
            Console.WriteLine();
            car1--;
            car1.WriteInfo();
            car1.FindCapacity();
            Console.WriteLine();
            car1++;
            car1.WriteInfo();
            car1.FindCapacity();
            Console.WriteLine();
            car1 = car - 6;
            car1.WriteInfo();
            Console.WriteLine($"Количество транспортных средств - {Transport.Count}");
            Console.WriteLine();
            WaterTransport water1 = new WaterTransport();
            water1.WriteInfo();
            water1.FindCapacity();
            Console.WriteLine();
            WaterTransport water2 = new WaterTransport("Водный транспорт", "Volvo", 3, 6, 6);
            water2.WriteInfo();
            water2.FindCapacity();
            Console.WriteLine();
            int n = car1;
            Console.WriteLine($"int = {n}");
            PassengerCar info1 = (PassengerCar)n;
            Console.WriteLine($"Транспорт - {info1}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Количество транспортных средств - {Transport.Count}");
            Console.WriteLine();
            AeroTransport aero1 = new AeroTransport();
            aero1.WriteInfo();
            aero1.FindCapacity();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Количество транспортных средств - {Transport.Count}");
            Console.WriteLine();
            AeroTransport aero2 = new AeroTransport("Самолёт", "S7 AirLines", 25, 120, 23, 10);
            aero2.WriteInfo();
            aero2.FindCapacity();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Количество транспортных средств - {Transport.Count}.\n");
            Metro metro1 = new Metro();
            metro1.WriteInfo();
            metro1.FindCapacity();
            Console.WriteLine();
            Truck truck1 = new Truck();
            truck1.InputCapacity(2, "С прицепом");
            truck1.WriteInfo();
            truck1.FindCapacity();
            Console.WriteLine();
            Console.WriteLine($"id {metro1} = {metro1.GetHashCode()}");
            Console.WriteLine();
            Console.WriteLine("========== ТРАНСПОРТНЫЙ ПАРК ==========");
            Transport[] transport = new Transport[] { car, car1, water1, water2, aero1, aero2, metro1, truck1 };
            Console.WriteLine("=== id ТРАНСПОРТНЫХ СРЕДСТВ ПАРКА ===");
            foreach (var cars in transport)
                Console.WriteLine($"id {cars.Brand} = {cars.GetHashCode()}");
            Console.WriteLine();
            Console.WriteLine("=== ТРАНСПОРТНЫЕ СРЕДСТВА ===");
            foreach (var tr in transport)
            {
                tr.WriteInfo();
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("========== СОРТИРОВАННЫЙ ТРАНСПОРТНЫЙ ПАРК ПО НАЗВАНИЮ ТРАНСПОРТА ==========");
            Array.Sort(transport, (c1, c2) => string.Compare(c1.Brand, c2.Brand));
            Console.WriteLine("=== СОРТИРОВКА ПО ГРУЗОПОДЪЁМНОСТИ ТРАНСПОРТНОГО СРЕДСТВА ===");
            foreach (var cars in transport)
            {
                if ((cars is PassengerCar && (cars.Capacity <= 10 || cars.Speed > 10)))
                {
                    cars.WriteInfo();
                    Console.WriteLine($"Машина {cars.Brand}, грузоподъёмность - {cars.Capacity}, срок службы - {cars.Life} лет");
                }
            }
            Console.WriteLine();
            Console.WriteLine("=== СОРТИРОВКА ПО ВРЕМЕНИ ИСПОЛЬЗОВАНИЯ ТРАНСПОРТНОГО СРЕДСТВА ===");
            foreach (var cars in transport)
            {
                if (cars is WaterTransport || cars.Life > 5)
                {
                    cars.WriteInfo();
                    Console.WriteLine($"Машина {cars.Brand}, срок службы - {cars.Life}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("=== СОРТИРОВКА ПО ТИПУ ТРАНСПОРТНОГО СРЕДСТВА ===");
            foreach (var cars in transport)
            {
                if (cars is Metro || cars.Brand == "Вагон типа D")
                {
                    cars.WriteInfo();
                    Console.WriteLine($"Машина {cars}, тип вагона - {cars.Brand}");
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Количество = {Transport.Count}, {car1.ToString()}");
            Console.WriteLine(car1.Equals(car));
        }
    }
}
