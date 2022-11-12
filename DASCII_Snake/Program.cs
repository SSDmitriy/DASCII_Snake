using static System.Console;


namespace DASCII_Snake
{
    class Program
    {
        //global vars
        private const int MAP_WIDTH = 30;
        private const int MAP_HEIGHT = 20;

        static void Main(string[] args)
        {
            SetWindowSize(MAP_WIDTH, MAP_HEIGHT);
            SetBufferSize(MAP_WIDTH, MAP_HEIGHT);
            CursorVisible = false;

            ReadKey();

        }
    }
}