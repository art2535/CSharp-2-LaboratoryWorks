using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Transport
{
    internal abstract class Transport
    {
        // Поля
        protected string brand = "";        // Поле "Марка машины"
        public static int count;            // Поле "Количество машин"
        public double speed;                // Поле "Скорость"
        protected int maxLoadCapacity;      // Поле "Максимальная грузоподъёмность"
        protected double weight;            // Поле "Вес машины"
        protected int serviceLife;          // Поле "Срок службы"
        protected readonly int id;          // Поле "ID машины" - только для чтения
        protected Color color;              // Поле "Цвет машины"

        // Свойства
        // Свойство "Марка машины"
        public string Brand
        {
            get
            {
                if (string.IsNullOrEmpty(brand))
                    return "Без марки";
                return brand;
            }
            set { brand = value; }
        }
        // Свойство "Срок службы"
        public int Life
        {
            get { return serviceLife; }
            set 
            {
                if (value <= 0)
                    serviceLife = 0;
                else
                    serviceLife = value;
            }
        }
        // Виртуальное свойство "Скорость"
        public virtual double Speed
        {
            get { return speed; }
            set
            {
                if (value <= 0)
                    speed = 0;
                else if (value > 100 && value <= 200)
                    speed = value;
                else if (value > 200)
                    speed = 200;
                else
                    speed = value;
            }
        }
        // Виртуальное свойство "Вес машины"
        public virtual double Weight
        {
            get { return weight; }
            set
            {
                if (value < 0.5)
                    weight = 0.5;
                else if (value > 50)
                    weight = 50;
                else
                    weight = value;
            }
        }
        // Виртуальное свойство "Грузоподъёмность"
        public virtual int Capacity
        {
            get { return maxLoadCapacity; }
            set
            {
                if (value <= 1)
                    maxLoadCapacity = 1;
                else
                    maxLoadCapacity = value;
            }
        }
        // Статическое свойство "Количество машин"
        public static int Count
        {
            get { return count; }
        }
        // Методы
        public abstract void WriteInfo();
        public void ChangeSpeed(double delta)
        {
            Speed += delta;
        }
        protected abstract string StatusLife();
        public abstract void InputCapacity(int capacity, string type);
        public abstract void FindCapacity();
        protected void Draw(Color color)
        {
            switch (color)
            {
                case Color.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Color.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Color.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
        // Конструкторы
        public Transport()
        {
            Brand = "";
            Speed = 45;
            Life = 6;
            count++;
            id = count;
        }
        public Transport(string brand, double speed, int capacity, double weight) : this()
        {
            Brand = brand;
            Speed = speed;
            Capacity = capacity;
            Weight = weight;
        }
        public override int GetHashCode()
        {
            return id;
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != GetType())
                return false;
            return Life == ((PassengerCar)obj).Life;
        }
    }
}
