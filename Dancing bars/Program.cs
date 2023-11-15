using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dancing_bars
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Введіть кількість прогрес-барів:");
            int numberOfBars = int.Parse(Console.ReadLine());

            ProgressBarManager progressBarManager = new ProgressBarManager();
            progressBarManager.StartProgressBars(numberOfBars);

            Console.ReadLine(); // Чекаємо, поки користувач натисне Enter, щоб програма не закрилася відразу
        }
    }

    internal class ProgressBarManager
    {
        private const int ProgressBarLength = 20;
        private const int InitialTopPosition = 2; // Initial top position for the progress bars

        public void StartProgressBars(int numberOfBars)
        {
            Task[] tasks = new Task[numberOfBars];

            for (int i = 0; i < numberOfBars; i++)
            {
                int threadId = i; // Capture the loop variable
                tasks[i] = Task.Run(() => GenerateProgressBar(threadId));
            }

            Task.WaitAll(tasks); // Wait for all tasks to complete
        }

        private void GenerateProgressBar(int threadId)
        {
            Random random = new Random();
            int progress = random.Next(3,15);
            int direction = 1; // Напрямок збільшення або зменшення
            int initialTopPosition = InitialTopPosition + threadId;

            while (true)
            {
                lock (Console.Out)
                {
                    Console.SetCursorPosition(0, initialTopPosition);
                    Console.Write("|");
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(new string(' ', progress));
                    Console.ResetColor();
                    Console.Write(new string(' ', ProgressBarLength - progress));
                    Console.Write("| ");
                }

                progress = (progress + direction) % ProgressBarLength; // Circular progress

                Thread.Sleep(100); // Затримка для імітації роботи
            }
        }
    }
}
