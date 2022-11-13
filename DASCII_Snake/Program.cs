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

            Snake snake = new Snake(12, 2, HEAD_COLOR, BODY_COLOR);

            DrawBorder();

            ///
            for (int i=0; i <20; i++) {
                Thread.Sleep(50);
                snake.MoveSnake(Direction.Right);
            }
            



            ReadKey();

        }

        //нарисовать края карты
        static void DrawBorder()
        {
            for (int i = 0; i < MAP_WIDTH; i++)
            {
                new Pixel(i, 0, BORDER_COLOR).Draw();
                new Pixel(i, MAP_HEIGHT-1, BORDER_COLOR).Draw();
            }

            for (int j = 1; j < MAP_HEIGHT; j++)
            {
                new Pixel(0, j, BORDER_COLOR).Draw();
                new Pixel(MAP_WIDTH-1, j, BORDER_COLOR).Draw();
            }
        }
    }
}