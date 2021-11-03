# MyTest \n
Тестовое задание
Цель: Создание консольной игры на языке C#. 
Уровень сложности: Легкий
1.	Реализовать класс Танка (робота, самолета и т.д.), имеющий следующие свойства:
-«Броня»
-«Жизнь»
-«Урон»
И методы: 
•	Конструктор класса для свойств, указанных выше
•	«Выстрел»
•	«Починка»
2.	Реализовать перечисление возможных действий игрока (по кол-ву методов)
3.	Создать основной метод программы Main. В теле метода необходимо:
•	создать два объекта класса танк (робот, самолет и т.д., для пользователя и компьютера) и передать в их конструктор необходимые параметры
•	вывести в консоль количество очков жизни игрока, количество очков жизни компьютера и возможные действия
При вводе игроком команд в консоль должны выполняться соответствующие действия: 
•	Выстрел – метод принимает объект противника в виде параметра. Отнимает у него кол-во жизней равное количеству урона, наносимое объектом игрока, и добавляет показатель брони (при желании можно модифицировать формулу).
•	Починка – прибавляет N кол-во жизней объекту.
Далее должен следовать ход противника: случайным образом он должен либо выполнять метод «Выстрел», либо «Починка».
4.	Добавить проверку на правильность ввода действия пользователем.
Уровень сложности: Средний
1.	Добавить к нашему классу свойство «Кол-во патронов» и инициализировать его в конструкторе, а также метод «Купить патроны».
2.	На главном экране отобразить кол-во патронов игрока и противника.
3.	Метод «Купить патроны» добавляет N патронов объекту.
4.	При срабатывании метода «Выстрел» выполнить проверку, а есть ли патроны? Если есть, уменьшить их кол-во на единицу, нет – вывести сообщение о необходимости покупки патронов для игрока или выполнить метод «Купить патроны» для компьютера, после чего завершить ход.
5.	Методу «Починка» добавить условие, при котором текущее кол-во жизней не может быть больше кол-ва жизней, переданного в конструктор при инициализации объекта.
6.	Во время хода компьютера добавить условие, что если у его объекта максимальное кол-во жизней, не выполнять метод «Починка».
7.	Добавить вероятность критического выстрела и промаха (критический выстрел с вероятностью 10%, промах - 20%). Критический выстрел увеличивает показатель «Урон» на 20%. Уведомить о данных событиях пользователя (для возвращения двух значений, как вариант, можно использовать выходной параметр).
Уровень сложности: Высокий
1.	Реализовать интерфейсы для класса объекта и компьютера. Интерфейс для объекта должен содержать объявления всех свойств и методов объекта, а также наследоваться вашим объектом. Интерфейс для компьютера должен содержать объявление метода «Ход компьютера». 
2.	Создать отдельный класс объекта, наследующий интерфейсы для объекта и компьютера, и описать в нём свой алгоритм для методов, объявленных в интерфейсах (свобода творчества). 
