using System;
using System.Collections.Generic;
using System.Drawing;
using static System.Console;


namespace DASCII_Snake
{
    class Program
    {
        //global vars
        public const int MAP_WIDTH = 40;
        public const int MAP_HEIGHT = 40;
        public const int BODY_START_LENGHT = 7;

        public const int SCORE_TO_WIN = MAP_WIDTH * MAP_HEIGHT - BODY_START_LENGHT - 1;
        
        private const ConsoleColor BORDER_COLOR = ConsoleColor.Magenta;
        private const ConsoleColor HEAD_COLOR = ConsoleColor.White;
        private const ConsoleColor BODY_COLOR = ConsoleColor.Green;

        static int score = BODY_START_LENGHT;
        static bool _isGameover = false;
        static bool _isSelfEat = false;
        static bool _isWin = false;


        static void Main(string[] args)
        {
            SetWindowSize(MAP_WIDTH, MAP_HEIGHT);
            SetBufferSize(MAP_WIDTH, MAP_HEIGHT);


            CursorVisible = false;

            Snake snake = new Snake(MAP_WIDTH / 2, MAP_HEIGHT / 2, HEAD_COLOR, BODY_COLOR);

            Direction currentMovement = Direction.Up; //первоначальное движение вверх

            DrawBorder();


            while (true)
            {
                Thread.Sleep(50);

                currentMovement = ReadDirection(currentMovement);
                snake.MoveSnake(currentMovement);
 

                //условие проигрыша - если врезался в границы
                if (snake.SnakeHead.getX() == 0
                    || snake.SnakeHead.getX() == MAP_WIDTH - 1
                    || snake.SnakeHead.getY() == 0
                    || snake.SnakeHead.getY() == MAP_HEIGHT - 1
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


                if(score == SCORE_TO_WIN)
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
                    string winMessage1 = "►►►       Congratulation!     ◄◄◄";
                    string winMessage2 = "►►►          You are          ◄◄◄";
                    string winMessage3 = "►►► THE LOOOOOOOOONGEST WORM! ◄◄◄";
                    Console.Beep(440, 400);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage1.Length / 2, MAP_HEIGHT / 2 - 2);
                    Console.Write(winMessage1);
                    Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage2.Length / 2, MAP_HEIGHT / 2 + 0);
                    Console.Write(winMessage2);
                    Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage3.Length / 2, MAP_HEIGHT / 2 + 2);
                    Console.Write(winMessage3);

                }

                if (!_isWin) // вслучае проигрыша
                {
                    if (!_isSelfEat)
                    {
                        string winMessage1 = "Твой счёт:";
                        string winMessage2 = Convert.ToString(score);
                        string winMessage3 = "...есть, куда расти...";
                        Console.Beep(880, 300);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage1.Length / 2, MAP_HEIGHT / 2 - 2);
                        Console.Write(winMessage1);
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage2.Length / 2, MAP_HEIGHT / 2 + 0);
                        Console.Write(winMessage2);
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage3.Length / 2, MAP_HEIGHT / 2 + 2);
                        Console.Write(winMessage3);
                    }
                    else
                    {
                        string winMessage1 = "EAT MY SHORTS!";
                        string winMessage2 = "Твой счёт: " + Convert.ToString(score);
                        string winMessage3 = "...давай ещё разок...";
                        Console.Beep(880, 300);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage1.Length / 2, MAP_HEIGHT / 2 - 2);
                        Console.Write(winMessage1);
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage2.Length / 2, MAP_HEIGHT / 2 + 0);
                        Console.Write(winMessage2);
                        Console.SetCursorPosition(MAP_WIDTH / 2 - winMessage3.Length / 2, MAP_HEIGHT / 2 + 2);
                        Console.Write(winMessage3);
                    }


                }

            }


            ReadKey();

        }



        //нарисовать края карты
        static void DrawBorder()
        {
            for (int i = 0; i < MAP_WIDTH; i++)
            {
                new Pixel(i, 0, BORDER_COLOR).Draw();
                new Pixel(i, MAP_HEIGHT - 1, BORDER_COLOR).Draw();
            }

            for (int j = 1; j < MAP_HEIGHT; j++)
            {
                new Pixel(0, j, BORDER_COLOR).Draw();
                new Pixel(MAP_WIDTH - 1, j, BORDER_COLOR).Draw();
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
            Console.Beep(400, 100);
            return currentDirection;
        }

    }
}