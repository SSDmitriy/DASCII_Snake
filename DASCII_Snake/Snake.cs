﻿
namespace DASCII_Snake
{
    public class Snake
    {
        private ConsoleColor _headColor;
        private ConsoleColor _bodyColor;

        //Голова змейки
        public Pixel SnakeHead;

        public Snake(int initialX, int initialY,
                       ConsoleColor headColor, ConsoleColor bodyColor,
                            int bodyLenght = 7) // 7- начальное значение длины тела
        {
            _headColor = headColor;
            _bodyColor = bodyColor;

            //добавление головы внутри конструктора
            SnakeHead = new Pixel(initialX, initialY, _headColor);

            //добавил тело внутри конструктора
            for (int i = bodyLenght; i >= 0; i--)
            {
                //тело отрисовываем от головы, отсутпая вниз по 1 пикс
                SnakeBody.Enqueue(new Pixel(initialX, SnakeHead.getY() + i + 1, _bodyColor));
            }

            //нарисовать змею в коснтруторе
            DrawSnake();

        }

        //тело змейки - это очередь
        public Queue<Pixel> SnakeBody = new Queue<Pixel>();





        //public int GetSnakeHeadX()
        //{
        //    return SnakeHead.getX();
        //}

        //public int GetSnakeHeadY()
        //{
        //    return SnakeHead.getY();
        //}

        //движение змейки: стереть змейку, добавить голову в очередь,
        //          убрать из очереди последний хвост,
        //              нарисовать голову, нарисовать тело
        public void MoveSnake(Direction direction)
        {
            ClearSnake();

            SnakeBody.Enqueue(new Pixel(SnakeHead.getX(), SnakeHead.getY(), _bodyColor));

            SnakeBody.Dequeue();

            int _newHeadX = SnakeHead.getX();
            int _newHeadY = SnakeHead.getY();

            switch (direction)
            {
                case Direction.Left:
                    _newHeadX = SnakeHead.getX() - 1;
                    break;

                case Direction.Right:
                    _newHeadX = SnakeHead.getX() + 1;
                    break;

                case Direction.Up:
                    _newHeadY = SnakeHead.getY() - 1;
                    break;

                case Direction.Down:
                    _newHeadY = SnakeHead.getY() + 1;
                    break;
            }

            SnakeHead = new Pixel(_newHeadX, _newHeadY, _headColor);

            DrawSnake();
        }


        //отрисовка змеи на экране
        public void DrawSnake()
        {
            SnakeHead.Draw();

            foreach (Pixel pixel in SnakeBody)
            {
                pixel.Draw();
            }
        }

        //стирание змеи на экране
        public void ClearSnake()
        {
            SnakeHead.Clear();

            foreach (Pixel pixel in SnakeBody)
            {
                pixel.Clear();
            }
        }


    }
}
