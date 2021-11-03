using System;
using System.Collections.Generic;

namespace MyTest
{
    //Интерфейс для бота
    public interface IObjectBot
    {
        //Метод выбора действия ботом
        public int action_bot();
    }
    //Интерфейс для игрока
    public interface IObjectPlayer
    {
        //Кол-во брони (свойство)
        public int Armor { get; set; }
        //Кол-во жизни (свойство)
        public int Life { get; set; }
        //Кол-во урона (свойство)
        public int Damage { get; set; }
        //Кол-во патронов (свойство)
        public int Bullets { get; set; }
        //Шанс на уклон
        public double Evasion { get; set; }
        //Шанс на крит
        public double Crit { get; set; }
        //Метод, получения информации об объекте
        public void GetInfo() { }
        //Метод, выстрел по указанному противнику
        public void Shot() { }
        //Метод, увеличение свойства - Life, на указанную цифру
        public void Repair() { }
        //Метод, увеления свойства - Bullets, на указанную цифру
        public void Buy_Bullets() { }
        //Метод, проверяющий равно ли свойство - Life нулю
        public bool IsAlive();
    }
    class Tank : IObjectPlayer
    {
        public int first_Life;
        //Конструктор класса Tank
        public Tank(int armor, int life, int damage, int bullets)
        {
            this.Armor = armor;
            this.Life = this.first_Life = life;
            this.Damage = damage;
            this.Bullets = bullets;
            this.Evasion = 0.2;
            this.Crit = 0.5;
        }
        public int Armor { get; set; }
        public int Life { get; set; }
        public int Damage { get; set; }
        public int Bullets { get; set; }
        public double Evasion { get; set; }
        public double Crit { get; set; }

        public void GetInfo()
        {
            Console.WriteLine($"Броня: {this.Armor}\nЗдоровье: {this.Life}\nУрон: {this.Damage}\nКол-во патронов: {this.Bullets}");
        }
        //Метод выстрела по указанному противнику. С шансом в 20% атакующий моет промахнуться, с шансом в 10% может нанести критический урон.
        //Крит урон величивает обычный урон на 20%.
        public void Shot(IObjectPlayer obj)
        {
            if (Evade())
            {
                Console.WriteLine("Игрок увернулся!");
            }
            else
            {
                if (Crit_damage())
                {
                    Console.WriteLine("Игрок кританул!");
                    if (obj.Armor - this.Damage * 0.2 < 0)
                    {
                        obj.Life += obj.Armor - this.Damage * 2;
                    }
                    else
                    {
                        obj.Life -= obj.Armor - this.Damage * 2;
                    }
                }
                else
                {
                    if (obj.Armor - this.Damage < 0)
                    {
                        obj.Life += obj.Armor - this.Damage;
                    }
                    else
                    {
                        obj.Life -= obj.Armor - this.Damage;
                    }
                }
            this.Bullets--;
            }
        }
        //Метод починки игрока. Увеличивает своство - Life, на указанную цифру. Если здоровье игрока равно начальному, ход пропускается
        public void Repair(int L)
        {
            if (L > this.first_Life)
            {
                Console.WriteLine("Кол-во жизней для починки не должно превышеть начальное значение!");
            }
            else
            {
                this.Life += L;
            }
        }

        protected bool Evade()
        {
            Random rnd = new Random();
            var x = rnd.NextDouble();
            return x < Evasion;
        }

        protected bool Crit_damage()
        {
            Random rnd = new Random();
            var x = rnd.NextDouble();
            return x == Crit;
        }
        //Метод покупки патронов. Увеличивает свойство - Bullets, на указанную цифру.
        public void Buy_Bullets(int B)
        {
            if ((B <= 5) && (this.Bullets + B <= 5))
            {
                this.Bullets += B;
            }
            else
            {
                Console.WriteLine("Ошибка ввода! Кол-во патронов не должно превышать 5!");
            }
        }
        //Метод, возвращающий true , если свойство Life равно нулю, иначе, возаращает false
        public bool IsAlive()
        {
            if (this.Life <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class TankBot : Tank, IObjectBot
    {
        public Random rnd = new Random();
        //Конструктор класса TankBot
        public TankBot(int armor, int life, int damage, int bullets)
            : base(armor, life, damage, bullets)
        {
            this.Life = this.first_Life = life;
            this.Damage = damage;
            this.Bullets = bullets;
            this.Evasion = 0.2;
            this.Crit = 0.5;
        }
        //Метод получает случайное число 1(Выстрел по противнику) или 2(Починка)
        //Если у бота здоровье равно начальному кол-ву и действие 2(Починка),
        //действие меняется на 1(Выстрел по противнику)
        public int action_bot()
        {
            int bot_action = rnd.Next(1, 2);
            if ((bot_action == 2) && (this.Life == this.first_Life))
            {
                bot_action = 1;
            }
            return bot_action;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Tank warrior = new Tank(10, 100, 20, 5);
            Console.WriteLine("Параметры игрока:");
            warrior.GetInfo();

            TankBot bot_warrior = new TankBot(10, 100, 15, 5);
            Console.WriteLine("Параметры бота:");
            bot_warrior.GetInfo();

            Random rnd = new Random();

            while ((!warrior.IsAlive()) && (!bot_warrior.IsAlive()))
            {
                Console.WriteLine("Выберите требуемое действие:\n 1. Огонь\n 2. Ремонт\n 3. Купить патроны");
                int action;
                while (!int.TryParse(Console.ReadLine(), out action))
                {
                    Console.WriteLine("Ошибка ввода! Введите число от 1 до 3");
                    Console.WriteLine("Выберите требуемое действие:\n 1. Огонь\n 2. Ремонт\n 3. Купить патроны");
                }
                switch (action)
                {
                    case 1:
                        if (warrior.Bullets > 0)
                        {
                            warrior.Shot(bot_warrior);
                            Console.WriteLine($"Игрок произвел выстрел! Здоровье бота стало равно {bot_warrior.Life}, кол-во патронов: {warrior.Bullets}");
                        }
                        else
                        {
                            Console.WriteLine("Нет патронов!Необходимо купить патроны!");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Введите кол-во здоровья для починки: ");
                        int L;
                        while (!int.TryParse(Console.ReadLine(), out L))
                        {
                            Console.WriteLine("Ошибка ввода! Введите число!");
                        }
                        warrior.Repair(L);
                        if (L > warrior.first_Life)
                        {
                            Console.WriteLine("Неправельный выбор! Передаю ход!");
                        }
                        else
                        {
                            Console.WriteLine($"Игрок произвел ремонт! Здоровье игрока стало равно {warrior.Life}");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Введите кол-во патронов для покупки(Общее кол-во патронов не должно превышать 5): ");
                        int B;
                        while (!int.TryParse(Console.ReadLine(), out B))
                        {
                            Console.WriteLine("Ошибка ввода! Введите число от 1 до 5");
                        }
                        warrior.Buy_Bullets(B);
                        Console.WriteLine($"Игрок купил патроны! Кол-во патронов игрока стало равно {warrior.Bullets}");
                        break;
                    default:
                        Console.WriteLine("Нет такой команды!");
                        break;
                }

                switch (bot_warrior.action_bot())
                {
                    case 1:
                        if (bot_warrior.Bullets > 0)
                        {
                            bot_warrior.Shot(warrior);
                            Console.WriteLine($"Бот произвел выстрел! Здоровье игрока стало равно {warrior.Life}, кол-во патронов: {bot_warrior.Bullets}");
                        }
                        else
                        {
                            int B = rnd.Next(1, 5);
                            bot_warrior.Buy_Bullets(B);
                            Console.WriteLine($"Бот купил {B} патронов! Кол-во патронов: {bot_warrior.Bullets}");
                        }
                        break;
                    case 2:
                        int L = rnd.Next(10, 20);
                        Console.WriteLine($"Бот выбрал {L} кол-во здоровья для починки!");
                        bot_warrior.Repair(L);
                        Console.WriteLine($"Бот произвел ремонт! Здоровье бота стало равно {bot_warrior.Life}");
                        break;
                }
            }
        }
    }
}
