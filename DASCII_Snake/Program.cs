using System;
using System.Collections.Generic;
using System.Drawing;
using static System.Console;
using System.Diagnostics;



namespace DASCII_Snake
{
    class Program
    {
        //global vars
        public const int MAP_WIDTH = 40;
        public const int MAP_HEIGHT = 40;
        public const int BODY_START_LENGHT = 7;


        //public const int scoreToWin = MAP_WIDTH * MAP_HEIGHT - BODY_START_LENGHT - 1;


        private const ConsoleColor BORDER_COLOR = ConsoleColor.Magenta;
        private const ConsoleColor HEAD_COLOR = ConsoleColor.White;
        private const ConsoleColor BODY_COLOR = ConsoleColor.Green;
        private const ConsoleColor FOOD_COLOR = ConsoleColor.Yellow;

        private static readonly Random random = new Random();

        static int scoreToWin = 20;
        static int score = BODY_START_LENGHT;
        static bool _isGameover = false;
        static bool _isSelfEat = false;
        static bool _isWin = false;

        static int speed = 10;



        static void Main(string[] args)
        {

            //Console.SetWindowSize(40, 40);
            //Console.SetBufferSize(40, 40);

            Console.SetWindowSize(MAP_WIDTH, MAP_HEIGHT);
            Console.SetBufferSize(MAP_WIDTH, MAP_HEIGHT);


            string instructionMessage1 = "ПРОБЕЛ - заново";
            string instructionMessage2 = "ESC - выход";

            string GreetingMessage1 = "☻☺☻ Привет, Полина! ☺☻☺";
            string GreetingMessage2 = "Не желаешь ли взрастить";
            string GreetingMessage21 = "этого милого электронного червёнка?";
            string GreetingMessage3 = "Чтобы он стал длиннее и проворнее!";

            string GreetingMessage4 = "Сколько червь может съесть?";
            string GreetingMessage5 = "1 - только десять";
            string GreetingMessage6 = "2 - всего двадцать";
            string GreetingMessage7 = "3 - наверное, тридцать";
            string GreetingMessage8 = "4 - целых сорок";
            string GreetingMessage9 = "5 - аж пятьдесят";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(MAP_WIDTH / 2 - GreetingMessage1.Length / 2, MAP_HEIGHT / 2 - 14);
            Console.Write(GreetingMessage1);
            Console.SetCursorPosition(MAP_WIDTH / 2 - GreetingMessage2.Length / 2, MAP_HEIGHT / 2 - 11);
            Console.Write(GreetingMessage2);
            Console.SetCursorPosition(MAP_WIDTH / 2 - GreetingMessage21.Length / 2, MAP_HEIGHT / 2 - 9);
            Console.Write(GreetingMessage21);
            Console.SetCursorPosition(MAP_WIDTH / 2 - GreetingMessage3.Length / 2, MAP_HEIGHT / 2 - 6);
            Console.Write(GreetingMessage3);

        restart:
            if (_isGameover) Clear();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(MAP_WIDTH / 2 - GreetingMessage4.Length / 2, MAP_HEIGHT / 2 - 2);
            Console.Write(GreetingMessage4);

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(MAP_WIDTH / 2 - 10, MAP_HEIGHT / 2 - 0);
            Console.Write(GreetingMessage5);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(MAP_WIDTH / 2 - 10, MAP_HEIGHT / 2 + 2);
            Console.Write(GreetingMessage6);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(MAP_WIDTH / 2 - 10, MAP_HEIGHT / 2 + 4);
            Console.Write(GreetingMessage7);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(MAP_WIDTH / 2 - 10, MAP_HEIGHT / 2 + 6);
            Console.Write(GreetingMessage8);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(MAP_WIDTH / 2 - 10, MAP_HEIGHT / 2 + 8);
            Console.Write(GreetingMessage9);


            //в цикле дождаться нажатия 1 -5 и установить scoreToWin
            Console.SetCursorPosition(MAP_WIDTH / 2 - 10, MAP_HEIGHT / 2 + 13);
            Console.Write("Выбрано: ");

            ConsoleKey key;

            //chooseAgain:
            key = ReadKey().Key;
            Console.SetCursorPosition(MAP_WIDTH / 2 - 1, MAP_HEIGHT / 2 + 13);
            string choice = key.ToString();
            string choseenGoal = "";

            switch (choice)
            {
                case "D1":
                    scoreToWin = 10;
                    choseenGoal = "10";
                    break;
                case "D2":
                    scoreToWin = 20;
                    choseenGoal = "20";
                    break;
                case "D3":
                    scoreToWin = 30;
                    choseenGoal = "30";
                    break;
                case "D4":
                    scoreToWin = 40;
                    choseenGoal = "40";
                    break;
                case "D5":
                    scoreToWin = 50;
                    choseenGoal = "50";
                    break;
                case "Escape":
                    goto endOfEnd;
                    break;
                default:
                    goto restart;
            }


            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(MAP_WIDTH / 2 - 1, MAP_HEIGHT / 2 + 13);
            Console.WriteLine(choseenGoal);
            Thread.Sleep(1000);
            Console.SetCursorPosition(MAP_WIDTH / 2 - 7, MAP_HEIGHT / 2 + 15);
            Console.WriteLine("стартуем...");
            Thread.Sleep(1000);


            _isWin = false;
            _isGameover = false;
            _isSelfEat = false;
            score = BODY_START_LENGHT;
            speed = 10;
            //SetWindowSize(MAP_WIDTH, MAP_HEIGHT);
            //SetBufferSize(MAP_WIDTH, MAP_HEIGHT);
            CursorVisible = false;

            Clear();
            Console.SetCursorPosition(0, 0);
            DrawBorder();
            Direction currentMovement = Direction.Up; //первоначальное движение вверх

            Stopwatch sw = new Stopwatch();

            Snake snake = new Snake(MAP_WIDTH / 2, MAP_HEIGHT / 2, HEAD_COLOR, BODY_COLOR);

            Pixel food = AddNewFood(snake);
            food.Draw();


            while (true)
            {
                sw.Restart();

                Direction oldMovement = currentMovement;

                while (sw.ElapsedMilliseconds <= (150 - speed))
                {
                    if (currentMovement == oldMovement)
                    {
                        currentMovement = ReadDirection(currentMovement);
                    }
                }

                //Thread.Sleep(200 - speed);



                //Если голова на текущем ходу совпала с едой, то передать eat=true
                if (snake.SnakeHead.getX() == food.getX() && snake.SnakeHead.getY() == food.getY())
                {
                    snake.MoveSnake(currentMovement, true);
                    if (score < scoreToWin - 1)
                    {
                        food = AddNewFood(snake);
                        food.Draw();
                    }
                    score++;
                    if (speed < 110) speed += 10;
                    //Console.Beep(600, 100);
                }
                else
                {
                    snake.MoveSnake(currentMovement);
                }

                //вывод текущего счета
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(3, MAP_HEIGHT - 2);
                Console.Write("Осталось съесть: " + (scoreToWin - score));
                Console.SetCursorPosition(MAP_WIDTH / 2 + 5, MAP_HEIGHT - 2);
                Console.Write("Скорость: " + speed / 10);


                //условие проигрыша - если врезался в границы
                if (snake.SnakeHead.getX() == 1
                    || snake.SnakeHead.getX() == MAP_WIDTH - 2
                    || snake.SnakeHead.getY() == 1
                    || snake.SnakeHead.getY() == MAP_HEIGHT - 3
                    )
                {
                    _isGameover = true;
                    _isWin = false;
                    break;
                }

                //условие проигрыша - если укусил себя
                bool exitFlag = false;
                foreach (Pixel pix in snake.allPixels)
                {

                    if (pix.getX() == snake.SnakeHead.getX() && pix.getY() == snake.SnakeHead.getY())
                    {
                        _isGameover = true;
                        _isSelfEat = true;
                        _isWin = false;
                        exitFlag = true; //это чтобы после выхода из foreach дальше выйти из while
                        break;
                    }

                }
                if (exitFlag) break;


                if (score >= scoreToWin)
                {
                    _isGameover = true;
                    _isWin = true;
                    break;
                }






            }



            //после игры
            if (_isGameover)
            {
                if (_isWin) // вслучае победы ► ◄ ▲ ▼
                {
                    string winMessage1 = "►►►        Ура!  Победа!      ◄◄◄";
                    string winMessage2 = "►►►     Твоя длина: " + Convert.ToString(score) + "!!!     ◄◄◄";
                    string winMessage3 = "►►►          You are          ◄◄◄";
                    string winMessage4 = "►►► THE LOOOOOOOOONGEST WORM! ◄◄◄";

                    //Console.Beep(440, 400);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage1.Length / 2, MAP_HEIGHT / 2 - 4);
                    Console.Write(winMessage1);
                    Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage2.Length / 2, MAP_HEIGHT / 2 - 2);
                    Console.Write(winMessage2);
                    Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage3.Length / 2, MAP_HEIGHT / 2 + 0);
                    Console.Write(winMessage3);
                    Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage4.Length / 2, MAP_HEIGHT / 2 + 2);
                    Console.Write(winMessage4);

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(MAP_WIDTH / 2 - instructionMessage1.Length / 2, MAP_HEIGHT / 2 + 6);
                    Console.Write(instructionMessage1);
                    Console.SetCursorPosition(MAP_WIDTH / 2 - instructionMessage2.Length / 2 - 1, MAP_HEIGHT / 2 + 8);
                    Console.Write(instructionMessage2);
                    //_isWin = false;

                }

                if (!_isWin) // вслучае проигрыша
                {
                    if (!_isSelfEat)
                    {
                        string winMessage1 = "Твоя длина:";
                        string winMessage2 = Convert.ToString(score);
                        string winMessage3 = "...есть, куда расти...";

                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage1.Length / 2, MAP_HEIGHT / 2 - 2);
                        Console.Write(winMessage1);
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage2.Length / 2, MAP_HEIGHT / 2 + 0);
                        Console.Write(winMessage2);
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage3.Length / 2, MAP_HEIGHT / 2 + 2);
                        Console.Write(winMessage3);

                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.SetCursorPosition(MAP_WIDTH / 2 - instructionMessage1.Length / 2, MAP_HEIGHT / 2 + 6);
                        Console.Write(instructionMessage1);
                        Console.SetCursorPosition(MAP_WIDTH / 2 - instructionMessage2.Length / 2 - 1, MAP_HEIGHT / 2 + 8);
                        Console.Write(instructionMessage2);
                        //_isWin = false;
                    }
                    else
                    {
                        string winMessage1 = "EAT MY SHORTS!";
                        string winMessage2 = "Твоя длина: " + Convert.ToString(score);
                        string winMessage3 = "...давай ещё разок...";
                        //Console.Beep(880, 300);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage1.Length / 2, MAP_HEIGHT / 2 - 2);
                        Console.Write(winMessage1);
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage2.Length / 2, MAP_HEIGHT / 2 + 0);
                        Console.Write(winMessage2);
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage3.Length / 2, MAP_HEIGHT / 2 + 2);
                        Console.Write(winMessage3);


                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.SetCursorPosition(MAP_WIDTH / 2 - instructionMessage1.Length / 2, MAP_HEIGHT / 2 + 6);
                        Console.Write(instructionMessage1);
                        Console.SetCursorPosition(MAP_WIDTH / 2 - instructionMessage2.Length / 2 - 1, MAP_HEIGHT / 2 + 8);
                        Console.Write(instructionMessage2);
                        //_isWin = false;
                    }


                }

            }



        final:
            key = ReadKey(true).Key;

            if (_isGameover == true)
            {
                if (key == ConsoleKey.Spacebar)
                {
                    goto restart;
                }
                else if (key == ConsoleKey.Escape)
                {
                    goto endOfEnd;
                }
                else
                {
                    goto final;
                }
            }

        endOfEnd:
            Console.Write("");
        }

        //нарисовать края карты
        static void DrawBorder()
        {

            //горизонтальные границы
            for (int i = 1; i < MAP_WIDTH - 2; i++)
            {
                new Pixel(i, 1, BORDER_COLOR).Draw();
                new Pixel(i, MAP_HEIGHT - 3, BORDER_COLOR).Draw();
            }


            //вертикальные
            for (int j = 1; j < MAP_HEIGHT - 2; j++)
            {
                new Pixel(1, j, BORDER_COLOR).Draw();
                new Pixel(MAP_WIDTH - 2, j, BORDER_COLOR).Draw();
            }

           
        }

        //считать курсор для направления движения
        static Direction ReadDirection(Direction currentDirection)
        {
            if (!KeyAvailable) return currentDirection;

            ConsoleKey key = ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (currentDirection != Direction.Right) //если не нажата противоположная стрелка
                    {
                        currentDirection = Direction.Left;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (currentDirection != Direction.Left)
                    {
                        currentDirection = Direction.Right;
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (currentDirection != Direction.Down)
                    {
                        currentDirection = Direction.Up;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (currentDirection != Direction.Up)
                    {
                        currentDirection = Direction.Down;
                    }
                    break;
            }
            //Console.Beep(400, 150);
            return currentDirection;
        }

        //сгенерировать еду
        static Pixel AddNewFood(Snake snake)
        {
            Pixel food;
            do
            {
                food = new Pixel(random.Next(2, MAP_WIDTH - 3), random.Next(2, MAP_HEIGHT - 4), FOOD_COLOR);
            } while (snake.SnakeHead.getX() == food.getX() && snake.SnakeHead.getY() == food.getY()
                        || snake.SnakeBody.Any(b => b.getX() == food.getX() && b.getY() == food.getY())
                        );
            //snake.allPixels
            return food;
        }

    }
}