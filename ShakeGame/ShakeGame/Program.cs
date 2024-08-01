namespace ShakeGame
{
    internal class Program
    {

        private const int MOVE_INTERVAL_IN_MS = 100;

        static void Main()
        {
            Board.Initialize();

            while (!Board.IsGameOver)
            {
                Board.ReadInput();

                Board.ClearSnake();
                Board.ClearApple();

                Board.UpdateSnake();

                Board.DrawSnake();
                Board.DrawApple();

                Thread.Sleep(MOVE_INTERVAL_IN_MS);
            }
        }
    }
}
