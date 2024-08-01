using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShakeGame
{
    internal static class Board
    {
        private const string PLAY_AGAIN_MESSAGE = "Play again?";
        private const string YES_MESSAGE = "Yes - y?";
        private const string NO_MESSAGE = "No - Press any key to exit!";
        private const string SCORE_MESSAGE = "Score = ";
        private const string GAME_OVER_MESSAGE = "GAME OVER!";

        private static Random randomGenerator = new Random();

        private static int rowVelocity;
        private static int colVelocity;

        public static bool IsGameOver { get; private set; }

        private static Point apple;

        private static Snake snake;

        public static void Initialize()
        {
            snake = new Snake();

            rowVelocity = 0;
            colVelocity = 1;

            Utils.PreparePlayground(snake);

            DrawSnake();
            SpawnApple();
        }

        public static void ReadInput()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        rowVelocity = (int)DirecctionAxis.NEUTRAL;
                        colVelocity = (int)DirecctionAxis.LEFT_UP;
                        break;
                    case ConsoleKey.RightArrow:
                        rowVelocity = (int)DirecctionAxis.NEUTRAL;
                        colVelocity = (int)DirecctionAxis.RIGHT_DOWN;
                        break;
                    case ConsoleKey.UpArrow:
                        rowVelocity = (int)DirecctionAxis.LEFT_UP;
                        colVelocity = (int)DirecctionAxis.NEUTRAL;
                        break;
                    case ConsoleKey.DownArrow:
                        rowVelocity = (int)DirecctionAxis.RIGHT_DOWN;
                        colVelocity = (int)DirecctionAxis.NEUTRAL;
                        break;
                    case ConsoleKey.Escape:
                        ExitGame();
                        break;

                }
            }
        }

        public static void UpdateSnake()
        {
            Point snakeHead = snake.Points.Last();
            Point newSnakeHead = new Point(snakeHead.Row + rowVelocity, snakeHead.Col + colVelocity);

            if (!Utils.IsInBoardBounds(newSnakeHead) || Utils.IsInSnake(snake, newSnakeHead))
            {
                IsGameOver = true;
                ExitGame();

                return;
            }

            if (Utils.AppleIsEaten(newSnakeHead, apple))
            {
                SpawnApple();
            } else
            {
                snake.Points.Dequeue();
            }

            snake.Points.Enqueue(newSnakeHead);
            Console.Title = $"{Utils.TITLE} {snake.Points.Count}";
        }

        public static void DrawSnake() => snake.Points.ToList().ForEach(point => Utils.DrawAt(point.Row, point.Col, ConsoleColor.Yellow, Utils.SNAKE_SYMBOL));

        public static void DrawApple() => Utils.DrawAt(apple.Row, apple.Col, ConsoleColor.DarkRed, Utils.APPLE_SYMBOL);

        public static void ClearSnake() => snake.Points.ToList().ForEach(point => Utils.DrawAt(point.Row, point.Col, ConsoleColor.White, Utils.EMPTY_SYMBOL));

        public static void ClearApple() => Utils.DrawAt(apple.Row, apple.Col, ConsoleColor.White, Utils.EMPTY_SYMBOL);

        public static void SpawnApple()
        {
            int appleRow = randomGenerator.Next(Console.WindowHeight);
            int appleCol = randomGenerator.Next(Console.WindowWidth);

            apple = new Point(appleRow, appleCol);
            DrawApple();
        }

        public static void ExitGame()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition((Console.WindowWidth - GAME_OVER_MESSAGE.Length) / Utils.HALF_SCREEN_DELIMITER, Console.WindowHeight / Utils.HALF_SCREEN_DELIMITER);
            Console.WriteLine(GAME_OVER_MESSAGE);

            Utils.CenterText($"{SCORE_MESSAGE} {snake.Points.Count}");

            Utils.CenterText(PLAY_AGAIN_MESSAGE);
            Utils.CenterText(YES_MESSAGE);
            Utils.CenterText(NO_MESSAGE);

            if (!Console.ReadKey().Key.Equals(ConsoleKey.Y))
            {
                IsGameOver = true;
                return;
            }

            IsGameOver = false;
            Initialize();
        }
    }
}
