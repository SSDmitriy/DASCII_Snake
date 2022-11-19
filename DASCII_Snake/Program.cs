using System;
using static System.Console;


namespace DASCII_Snake
{
    class Program
    {
        //global vars
        private const int MAP_WIDTH = 40;
        private const int MAP_HEIGHT = 40;

        private const ConsoleColor BORDER_COLOR = ConsoleColor.Magenta;
        private const ConsoleColor HEAD_COLOR = ConsoleColor.White;
        private const ConsoleColor BODY_COLOR = ConsoleColor.Green;

        static void Main(string[] args)
        {
            SetWindowSize(MAP_WIDTH, MAP_HEIGHT);
            SetBufferSize(MAP_WIDTH, MAP_HEIGHT);
            CursorVisible = false;

            Snake snake = new Snake(MAP_WIDTH / 2, MAP_HEIGHT / 2, HEAD_COLOR, BODY_COLOR);

            Direction currentMovement = Direction.Up; //первоначальное движение вверх

            DrawBorder();



            ///
            while(true)
            {
                Thread.Sleep(150);

                currentMovement = ReadDirection(currentMovement);
                snake.MoveSnake(currentMovement);
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

            //ForbiddenDirection forbiddenDirection;
            //if (key.Equals(ConsoleKey.LeftArrow)) forbiddenDirection = ForbiddenDirection.Right;
            //if (key.Equals(ConsoleKey.RightArrow)) forbiddenDirection = ForbiddenDirection.Left;
            //if (key.Equals(ConsoleKey.UpArrow)) forbiddenDirection = ForbiddenDirection.Down;
            //if (key.Equals(ConsoleKey.DownArrow)) forbiddenDirection = ForbiddenDirection.Up;


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

            return currentDirection;
        }

    }
}