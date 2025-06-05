using System;

namespace Tic_Tac_Toe_Game
{
    internal class Program
    {
        public struct Player
        {
            public string name;
            public int score;
        }

        static void Main(string[] args)
        {
            DisplayMenu();
        }

        static void DisplayMenu()
        {
            int menuOption = 0;

            Console.WriteLine("Welcome to Tic-Tac-Toe" +
                "\n 1 - Play" +
                "\n 2 - Exit");

            menuOption = Convert.ToInt32(Console.ReadLine());
            switch (menuOption)
            {
                case 1: InitialiseGame(); break;
                case 2: Environment.Exit(0); break;
            }

        }
        static void InitialiseGame()
        {

            //Variables:
            char[,] board = new char[3, 3];
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x <= 2; x++)
                {
                    board[x, y] = '-';
                }
            }

            Player[] Data = new Player[2];
            int moveNumber = 0;

            GetName(ref Data);
            Game(ref Data, ref board, ref moveNumber);
        }
        static void GetName(ref Player[] Data)
        {
            Console.WriteLine("Enter Player 1 Name: ");
            Data[0].name = Console.ReadLine();
            Console.WriteLine("Enter Player 2 Name: ");
            Data[1].name = Console.ReadLine();
        }
        static void Game(ref Player[] Data, ref char[,] board, ref int moveNumber)
        {
            bool isGameOver = false;
            string winType = null;
            int playerTurn = -1;
            while (!isGameOver && moveNumber < 9)
            {
                playerTurn = (moveNumber) % 2;
                int row, col;
                char playerChar = 'Z';

                if (playerTurn == 0)
                {
                    playerChar = 'X';
                }
                else
                {
                    playerChar = 'O';
                }

                moveNumber++;
                DisplayBoard(ref board);
                Console.WriteLine($"{Data[playerTurn].name}'s move." +
                    $"\nEnter the coordinates for your move.");

            invalidInput:
                Console.Write("Column: "); col = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.Write("Row: "); row = Convert.ToInt32(Console.ReadLine()) - 1;

                bool valid = CheckMove(ref board, row, col);
                if (!valid)
                {
                    Console.WriteLine("Invalid input. Square is already occupied.");
                    goto invalidInput;
                }

                board[row, col] = playerChar;

                if (moveNumber >= 5)
                {
                    CheckWin(ref isGameOver, ref board, ref winType);
                }
            }
            Console.WriteLine("Game Over! Press any key to return to the menu.");
            if (winType != null)
            {
                Console.WriteLine($"{Data[playerTurn].name} won with a {winType}");
            }
            else
            {
                Console.WriteLine("Draw!");
            }
                Console.ReadLine();
        }
        static void DisplayBoard(ref char[,] board)
        {
            Console.Clear();
            Console.WriteLine("Current Board:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        static bool CheckMove(ref char[,] B, int row, int col)
        {
            bool valid = true;
            if (B[row, col] != '-')
            {
                valid = false;
            }
            return valid;
        }
        static void CheckWin(ref bool isGameOver, ref char[,] B, ref string winType)
        {
            //Check columns
            for (int i = 0; i <= 2; i++)
            {
                if (B[0, i] != '-' && B[0, i] == B[1, i] && B[1, i] == B[2, i])
                {
                    isGameOver = true;
                    winType = "col";
                    return;
                }
            }

            //Check rows
            for (int i = 0; i <= 2; i++)
            {
                if (B[i, 0] != '-' && B[i, 0] == B[i, 1] && B[i, 1] == B[i, 2])
                {
                    isGameOver = true;
                    winType = "row";
                    return;
                }
            }

            //Check diagonals
            if (B[0, 0] != '-' && B[0, 0] == B[1, 1] && B[1, 1] == B[2, 2])
            {
                isGameOver = true;
                winType = "diagonal (TL->BR)";
                return;
            }

            if (B[0, 2] != '-' && B[0, 2] == B[1, 1] && B[1, 1] == B[2, 0])
            {
                isGameOver = true;
                winType = "diagonal (BL->TR)";
                return;
            }
        }
    }
}
