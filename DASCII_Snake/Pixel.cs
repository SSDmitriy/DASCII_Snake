﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DASCII_Snake
{
    public struct Pixel
    {
        //private const char PIXEL_CHAR = '▓' ▒ ░  █ ☺ ☻ ► ◄ ▲ ▼ ◙;
        private const char PIXEL_CHAR = '▓';
        private const char PIXEL_EMPTY = ' ';

        private int x;
        private int y;
        public ConsoleColor color;

        public Pixel(int x, int y, ConsoleColor color){

            this.x = x;
            this.y = y;
            this.color = color;

            }

        public int getX()
        {
            return x;
        }
        public int getY()
        {
            return y;
        }


        //public Pixel(int x, int y, ConsoleColor color)
        //{
        //    X = x;
        //    Y = y;
        //    Color = color;
        //}

        //public int X { get; }
        //public int Y { get; }

        //public ConsoleColor Color { get; }

        public void draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(PIXEL_CHAR);
        }
        
        public void clear()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(PIXEL_EMPTY);
        }




    }
}
