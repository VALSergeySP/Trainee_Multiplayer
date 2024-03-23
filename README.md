<header><h1 align=center>Задание для Trainee course<br>(мультиплеер)</h1></header>
<subtitle><i>Задействованные специалисты: Unity/C# Developer;<br>Сроки разработки - <b>5 рабочих дней</b>;</i></subtitle>

<body>
 <h2><b>Описание проекта:</b></h2>
<p>Лобби формируется до 2 игроков, каждый игрок выбирает себе персонажа ,после чего начинается игра.  В начале игры раздается рандомное оружие(не повторяются у игроков)  и начинается первая волна спавна врагов. В течение первой  волны(1 мин) спавнятся обычные зомби и патроны .Когда заканчивается первая волна идет отдых 30 сек. После чего наступает вторая волна(3 мин): увеличено количество зомби и спавнятся аптечки . Дополнительно спавнятся скелеты.  Третья волна (5 мин) спавнятся все виды врагов и все подбираемые предметы; После чего, отображается список игроков с количеством нанесенного урона + количество убитых врагов. Если игрок умер, то он больше не продолжает сессию, а становится наблюдателем.Если умерли все,то игра заканчивается</p>

<h2><b>Требования к проекту:</b></h2>
<p>
<ol>
 <li>Загрузить ассет и реализовать механику проекту</li>
 <li>Использовать Photon Fusion в качестве сетевого решения</li>
 <li>Создать локацию в которой будут спавнится волнами  n количество врагов</li>
 <li>При создании локации использовать Tile Map</li>
 <li>Создать 3 видов врагов:
 <ul>
  <li>Обычный зомби -медленный зомби с слабой атакой</li>
  <li>Прокаченный зомби - медленный зомби с увеличенным здоровьем и уроном, но с низкой частотой атак;</li>
  <li>Скелет - атакует на расстоянии, но быстро;</li>
 </ul>
 </li>
 <li>Создать 3 вида оружия:
 <ul>
  <li>Обрез - малая дистанци	я атаки, большой урон. атакует одного врага;</li>
  <li>Дробовик - средняя дистанция, небольшой урон, атакует  нескольких врагов;</li>
  <li>Автомат - средний урон, большая дистанция атаки, атакует одного врага;</li>
 </ul>
 </li>
 <li>Создать три подбираемых предметов:
 <ul>
  <li>Аптечка - восстанавливает здоровье;</li>
  <li>Бомба - уничтожает врагов в n радиусе;</li>
  <li>Ящики с патронами;</li>
 </ul>
 </li>
 <li>Создать механизм подключения нескольких игроков в одну игровую сессию;</li>
 <li>Создать механику выбора персонажа из трех возможных;</li>
 <li>Отображение таймера, который показывает время до конца матча;</li>
 <li>Управление через виртуальный джойстик.</li>
</ol>
</p>
<p>
 <h2>Требования к коду:</h2>
 <ol>
  <li>Использовать принципы SOLID, в частности принцип  SRP;</li>
  <li>Создание системы смены управления, в зависимости от платформы;</li>
  <li>Движение нужно  реализовать  через мастер клиент;</li>
  <li>Не использовать большие классы- контроллеры;</li>
  <li>Не использовать анти паттерн “Singleton”;</li>
  <li>
   Создать удобный инструментарий для настройки основных аспектов игры:
   <ul>
    <li>Настройка волн;</li>
    <li>Характеристики оружия;</li>
    <li>Характеристики врагов.</li>
   </ul>
  </li>
  <li>
   Использовать паттерны проектирования:
   <ul>
    <li>Фабричный метод;</li>
    <li>Состояние;</li>
    <li>Стратегия.</li>
   </ul>
  </li>
 </ol>
</p>


<p align=center>
 <h2>Результат</h2>
 <img src="https://github.com/VALSergeySP/Trainee_Multiplayer/blob/develop/Assets/Undead_Screenshots/image_2024-03-23_10-10-58.png" alt="Screenshot 1"/>
 <img src="https://github.com/VALSergeySP/Trainee_Multiplayer/blob/develop/Assets/Undead_Screenshots/image_2024-03-23_10-10-376.png" alt="Screenshot 2"/>
 <img src="https://github.com/VALSergeySP/Trainee_Multiplayer/blob/develop/Assets/Undead_Screenshots/image_2024-03-23_10-10-375.png" alt="Screenshot 3"/>
 <img src="https://github.com/VALSergeySP/Trainee_Multiplayer/blob/develop/Assets/Undead_Screenshots/image_2024-03-23_10-10-374.png" alt="Screenshot 4"/>
 <img src="https://github.com/VALSergeySP/Trainee_Multiplayer/blob/develop/Assets/Undead_Screenshots/image_2024-03-23_10-10-363.png" alt="Screenshot 5"/>
 <img src="https://github.com/VALSergeySP/Trainee_Multiplayer/blob/develop/Assets/Undead_Screenshots/image_2024-03-23_10-10-362.png" alt="Screenshot 6"/>
 <img src="https://github.com/VALSergeySP/Trainee_Multiplayer/blob/develop/Assets/Undead_Screenshots/image_2024-03-23_10-10-36.png" alt="Screenshot 7"/>
 <img src="https://github.com/VALSergeySP/Trainee_Multiplayer/blob/develop/Assets/Undead_Screenshots/7.png" alt="Screenshot 8"/>
</p>

</body>
