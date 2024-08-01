using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShakeGame
{
    internal static class Utils
    {
        private const int MIN_SCREEN_SIZE = 0;
        private const int MAXIMIZE = 3;

        public const int HALF_SCREEN_DELIMITER = 2;

        public const string SNAKE_SYMBOL = "█";
        public const string APPLE_SYMBOL = "@";
        public const string EMPTY_SYMBOL = " ";

        public const string TITLE = "Snake Game - Score => ";

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static void PreparePlayground(Snake snake)
        {
            Console.Clear();
            Console.CursorVisible = false;

            Console.Title = $"{TITLE} {snake.Points.Count}";
            Console.ForegroundColor = ConsoleColor.Gray;

            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

            IntPtr console = GetConsoleWindow();
            ShowWindow(console, MAXIMIZE);
        }

        public static void CenterText(string text)
        {
            Console.Write(new string(EMPTY_SYMBOL.ToCharArray()[0], (Console.WindowWidth - text.Length) / HALF_SCREEN_DELIMITER));
            Console.WriteLine(text);
        }

        public static void DrawAt(int row, int col, ConsoleColor color, string symbol)
        {
            Console.SetCursorPosition(col, row);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }

        public static bool IsInBoardBounds(Point snakeHead) 
            => snakeHead.Row >= MIN_SCREEN_SIZE && snakeHead.Row < Console.WindowHeight && snakeHead.Col >= MIN_SCREEN_SIZE && snakeHead.Col < Console.WindowWidth;

        public static bool IsInSnake(Snake snake, Point snakeHead) => snake.Points.Contains(snakeHead);

        public static bool AppleIsEaten(Point snakeHead, Point apple) => snakeHead.Row == apple.Row &&snakeHead.Col == apple.Col;

    }
}