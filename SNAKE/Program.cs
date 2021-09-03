using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;
using NEAT;
namespace Snake_2._5
{
    public class GlobalData
    {
        public static int arraysize = 23; //max 25
        public static int sleeptime = 25;
        public static char snakeBody = '@';
        public static int SensorSize = 5;
        public static int TimeUntilStravation = 100;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Snake";
            NEAT.GlobalData.InputsNum = GlobalData.SensorSize * 7 + 4;
            NEAT.GlobalData.OutputsNum = 4;
            

            Console.WriteLine("Do you want to start a new pool ?");
            //Pool pool = (Console.ReadLine().ToString() == "no") ? NEAT.Main.LoadNET(@"c:\\SaveNET\\Snake.txt") : new Pool();
            Pool pool = (Console.ReadLine().ToString() == "no") ? NEAT.Main.LoadNET(@"Snake.txt") : new Pool();

	    Console.CancelKeyPress += new ConsoleCancelEventHandler((sender, e) => Console_CancelKeyPress(sender, e, pool));

            NEAT.Main.EvolveNeat(pool, SnakeFitness, stop);

        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e, Pool pool)
        {
            Console.WriteLine("Saving ...");
            System.Threading.Thread.Sleep(2000);
            NEAT.Main.SaveNET(@"Snake.txt", pool);
        }  //fix
        static void printBoard(char[,] board)
        {
            Console.Clear();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    Console.Write(board[i, m].ToString() + ' ');
                }
                Console.WriteLine();
            }
        }

        static bool Displaymovesnake(Snake snake, char[,] board)
        {
            if (snake.GetlastMove() == 0)  //left
            {
                if (snake.GetxPos() == 0) return false;
                if (board[snake.GetxPos() - 1, snake.GetyPos()] != ' ' && board[snake.GetxPos() - 1, snake.GetyPos()] != '*') return false;
                board[snake.GetxPos() - 1, snake.GetyPos()] = GlobalData.snakeBody;
                Console.SetCursorPosition((snake.GetxPos() - 1) * 2, snake.GetyPos());
                Console.Write(GlobalData.snakeBody);
                Console.SetCursorPosition(0, GlobalData.arraysize + 1);
                Displaymovetails(snake, board);
                snake.SetxPos(snake.GetxPos() - 1);
            }
            else if (snake.GetlastMove() == 1)  //right
            {
                if (snake.GetxPos() == GlobalData.arraysize - 1) return false;
                if (board[snake.GetxPos() + 1, snake.GetyPos()] != ' ' && board[snake.GetxPos() + 1, snake.GetyPos()] != '*') return false;
                board[snake.GetxPos() + 1, snake.GetyPos()] = GlobalData.snakeBody;
                Console.SetCursorPosition((snake.GetxPos() + 1) * 2, snake.GetyPos());
                Console.Write(GlobalData.snakeBody);
                Console.SetCursorPosition(0, GlobalData.arraysize + 1);
                Displaymovetails(snake, board);
                snake.SetxPos(snake.GetxPos() + 1);
            }
            else if (snake.GetlastMove() == 2) // up
            {
                if (snake.GetyPos() == 0) return false;
                if (board[snake.GetxPos(), snake.GetyPos() - 1] != ' ' && board[snake.GetxPos(), snake.GetyPos() - 1] != '*') return false;
                board[snake.GetxPos(), snake.GetyPos() - 1] = GlobalData.snakeBody;
                Console.SetCursorPosition(snake.GetxPos() * 2, snake.GetyPos() - 1);
                Console.Write(GlobalData.snakeBody);
                Console.SetCursorPosition(0, GlobalData.arraysize + 1);
                Displaymovetails(snake, board);
                snake.SetyPos(snake.GetyPos() - 1);
            }
            else if (snake.GetlastMove() == 3) // down
            {
                if (snake.GetyPos() == GlobalData.arraysize - 1) return false;
                if (board[snake.GetxPos(), snake.GetyPos() + 1] != ' ' && board[snake.GetxPos(), snake.GetyPos() + 1] != '*') return false;
                board[snake.GetxPos(), snake.GetyPos() + 1] = GlobalData.snakeBody;
                Console.SetCursorPosition(snake.GetxPos() * 2, snake.GetyPos() + 1);
                Console.Write(GlobalData.snakeBody);
                Console.SetCursorPosition(0, GlobalData.arraysize + 1);
                Displaymovetails(snake, board);
                snake.SetyPos(snake.GetyPos() + 1);
            }
            //else return false;
            return true;
        }
        static void Displaymovetails(Snake snake, char[,] board)
        {
            if (snake.GetTails().Count() == 0)
            {
                board[snake.GetxPos(), snake.GetyPos()] = ' ';
                Console.SetCursorPosition(snake.GetxPos() * 2, snake.GetyPos());
                Console.Write(' ');
                Console.SetCursorPosition(0, GlobalData.arraysize + 1);
                return;
            }
            if (snake.GetTails()[snake.GetTails().Count() - 1].GetTailx() != null && snake.GetTails()[snake.GetTails().Count() - 1].GetTaily() != null)
            {
                board[snake.GetTails()[snake.GetTails().Count() - 1].GetTailx().Value, snake.GetTails()[snake.GetTails().Count() - 1].GetTaily().Value] = ' ';
                Console.SetCursorPosition(snake.GetTails()[snake.GetTails().Count() - 1].GetTailx().Value * 2, snake.GetTails()[snake.GetTails().Count() - 1].GetTaily().Value);
                Console.Write(' ');
                Console.SetCursorPosition(0, GlobalData.arraysize + 1);
            }
            for (int i = snake.GetTails().Count() - 1; i > 0; i--)
            {
                snake.GetTails()[i].SetTailx(snake.GetTails()[i - 1].GetTailx().Value);
                snake.GetTails()[i].SetTaily(snake.GetTails()[i - 1].GetTaily().Value);
            }
            snake.GetTails()[0].SetTailx(snake.GetxPos());
            snake.GetTails()[0].SetTaily(snake.GetyPos());
        }
        static bool Displayfood(char[,] board, Random random)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    if (board[i, m] == '*') return false;
                }
            }
            List<int[]> a = new List<int[]>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    if (board[i, m] == ' ')
                    {
                        int[] ina = new int[2];
                        ina[0] = i;
                        ina[1] = m;
                        a.Add(ina);

                    }
                }
            }
            int ran = random.Next(0, a.Count());
            board[a[ran][0], a[ran][1]] = '*';
            Console.SetCursorPosition(a[ran][0] * 2, a[ran][1]);
            Console.Write('*');
            Console.SetCursorPosition(0, GlobalData.arraysize + 1);
            return true;
        }

        static bool movesnake(Snake snake, char[,] board)
        {
            if (snake.GetlastMove() == 0)  //left
            {
                if (snake.GetxPos() == 0) return false;
                if (board[snake.GetxPos() - 1, snake.GetyPos()] != ' ' && board[snake.GetxPos() - 1, snake.GetyPos()] != '*') return false;
                board[snake.GetxPos() - 1, snake.GetyPos()] = GlobalData.snakeBody;
                movetails(snake, board);
                snake.SetxPos(snake.GetxPos() - 1);
            }
            else if (snake.GetlastMove() == 1)  //right
            {
                if (snake.GetxPos() == GlobalData.arraysize - 1) return false;
                if (board[snake.GetxPos() + 1, snake.GetyPos()] != ' ' && board[snake.GetxPos() + 1, snake.GetyPos()] != '*') return false;
                board[snake.GetxPos() + 1, snake.GetyPos()] = GlobalData.snakeBody;
                movetails(snake, board);
                snake.SetxPos(snake.GetxPos() + 1);
            }
            else if (snake.GetlastMove() == 2) // up
            {
                if (snake.GetyPos() == 0) return false;
                if (board[snake.GetxPos(), snake.GetyPos() - 1] != ' ' && board[snake.GetxPos(), snake.GetyPos() - 1] != '*') return false;
                board[snake.GetxPos(), snake.GetyPos() - 1] = GlobalData.snakeBody;
                movetails(snake, board);
                snake.SetyPos(snake.GetyPos() - 1);
            }
            else if (snake.GetlastMove() == 3) // down
            {
                if (snake.GetyPos() == GlobalData.arraysize - 1) return false;
                if (board[snake.GetxPos(), snake.GetyPos() + 1] != ' ' && board[snake.GetxPos(), snake.GetyPos() + 1] != '*') return false;
                board[snake.GetxPos(), snake.GetyPos() + 1] = GlobalData.snakeBody;
                movetails(snake, board);
                snake.SetyPos(snake.GetyPos() + 1);
            }
            //else return false;
            return true;
        }
        static void movetails(Snake snake, char[,] board)
        {
            if (snake.GetTails().Count() == 0)
            {
                board[snake.GetxPos(), snake.GetyPos()] = ' ';
                return;
            }
            if (snake.GetTails()[snake.GetTails().Count() - 1].GetTailx() != null && snake.GetTails()[snake.GetTails().Count() - 1].GetTaily() != null)
            {
                board[snake.GetTails()[snake.GetTails().Count() - 1].GetTailx().Value, snake.GetTails()[snake.GetTails().Count() - 1].GetTaily().Value] = ' ';
            }
            for (int i = snake.GetTails().Count() - 1; i > 0; i--)
            {
                snake.GetTails()[i].SetTailx(snake.GetTails()[i - 1].GetTailx().Value);
                snake.GetTails()[i].SetTaily(snake.GetTails()[i - 1].GetTaily().Value);
            }
            snake.GetTails()[0].SetTailx(snake.GetxPos());
            snake.GetTails()[0].SetTaily(snake.GetyPos());
        }
        static bool food(char[,] board, Random random)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    if (board[i, m] == '*') return false;
                }
            }
            List<int[]> a = new List<int[]>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    if (board[i, m] == ' ')
                    {
                        int[] ina = new int[2];
                        ina[0] = i;
                        ina[1] = m;
                        a.Add(ina);

                    }
                }
            }
            int ran = random.Next(0, a.Count());
            board[a[ran][0], a[ran][1]] = '*';
            return true;
        }

        static class Randomlist
        {
            private static int counter1;
            private static List<int[]> b = new List<int[]>();
            public static List<int[]> a
            {
                get { return b; }
                set { b = value; }
            }
            public static int counter
            {
                get { return counter1; }
                set { counter1 = value; }
            }
        }
        static void randomizepos(char[,] board)
        {
            if (Randomlist.counter != 0) { return; }
            Randomlist.counter++;
            Random random = new Random();
            List<int[]> pos = new List<int[]>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    int[] ina = new int[2];
                    ina[0] = i;
                    ina[1] = m;
                    pos.Add(ina);
                }
            }
            int n = pos.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                int[] value = pos[k];
                pos[k] = pos[n];
                pos[n] = value;
            }
            Randomlist.a = pos;
        }
        static int FoodCounter = 0;
        static bool foodsnake(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    if (board[i, m] == '*') return false;
                }
            }
            List<int[]> b = new List<int[]>();
            for (int i = 0; i < Randomlist.a.Count(); i++)
            {
                if (board[Randomlist.a[i][0], Randomlist.a[i][1]] == ' ')
                {
                    b.Add(new int[] { Randomlist.a[i][0], Randomlist.a[i][1] });
                }
            }

            if (FoodCounter >= b.Count()) FoodCounter = 0;
            board[b[FoodCounter][0], b[FoodCounter][1]] = '*';
            FoodCounter++;
            return true;
        }
        static bool Displayfoodsnake(char[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    if (board[i, m] == '*') return false;
                }
            }
            List<int[]> b = new List<int[]>();
            for (int i = 0; i < Randomlist.a.Count(); i++)
            {
                if (board[Randomlist.a[i][0], Randomlist.a[i][1]] == ' ')
                {
                    b.Add(new int[] { Randomlist.a[i][0], Randomlist.a[i][1] });
                }
            }

            if (FoodCounter >= b.Count()) FoodCounter = 0;
            board[b[FoodCounter][0], b[FoodCounter][1]] = '*';
            Console.SetCursorPosition(b[FoodCounter][0] * 2, b[FoodCounter][1]);
            Console.Write('*');
            Console.SetCursorPosition(0, GlobalData.arraysize + 1);
            FoodCounter++;
            return true;
        }

        static double SnakeFitness(Genome genome)
        {
            char[,] board = new char[GlobalData.arraysize, GlobalData.arraysize];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    if (i == 0 || i == board.GetLength(0) - 1)
                    {
                        board[i, m] = '-';
                    }
                    else if (m == 0 || m == board.GetLength(1) - 1)
                    {
                        board[i, m] = '!';
                    }
                    else board[i, m] = ' ';
                }
            }
            board[board.GetLength(0) / 2, board.GetLength(1) / 2] = GlobalData.snakeBody;
            Snake snake = new Snake();
            snake.SetxPos(board.GetLength(0) / 2);
            snake.SetyPos(board.GetLength(1) / 2);

            Tail tail1 = new Tail();
            tail1.SetTailx(null);
            tail1.SetTaily(null);
            snake.AddaTail(tail1);

            randomizepos(board);
            FoodCounter = 0;
            foodsnake(board);
            NEAT.Main.CleanNodesValue(genome);
            double score = 0;
            int TimeUntilStravation = GlobalData.TimeUntilStravation;
            while (true)  //0 left 1 right 2 up 3 down
            {
                score++;
                double[] input = inputsSnake(board, snake);
                for (int i = 0; i < input.Length; i++)
                {
                    genome.GetNodes()[i].SetValue(input[i]);
                }
                NEAT.Main.StepNetwork(genome, 5);
                double out1 = genome.GetNodes()[input.Length].GetValue();
                double out2 = genome.GetNodes()[input.Length + 1].GetValue();
                double out3 = genome.GetNodes()[input.Length + 2].GetValue();
                double out4 = genome.GetNodes()[input.Length + 3].GetValue();
                if (out1 > out2 && out1 > out3 && out1 > out4 && (snake.GetlastMove() != 1 || snake.GetTails().Count() == 0)) snake.SetlastMove(0);
                else if (out2 > out1 && out2 > out3 && out2 > out4 & (snake.GetlastMove() != 0 || snake.GetTails().Count() == 0)) snake.SetlastMove(1);
                else if (out3 > out2 && out3 > out1 && out1 > out4 & (snake.GetlastMove() != 3 || snake.GetTails().Count() == 0)) snake.SetlastMove(2);
                else if (out4 > out2 && out4 > out3 && out4 > out1 & (snake.GetlastMove() != 2 || snake.GetTails().Count() == 0)) snake.SetlastMove(3);
                if (foodsnake(board)) 
                {
                    Tail tail = new Tail();
                    tail.SetTailx(null);
                    tail.SetTaily(null);
                    snake.AddaTail(tail);
                    TimeUntilStravation += GlobalData.TimeUntilStravation;
                    score += 100;
                }
                TimeUntilStravation--;
                if (TimeUntilStravation <= 0) goto end;
                if (!movesnake(snake, board)) goto end;
            }
            end:
            return score;
        }
        static void DisplaySnake(Genome genome)
        {
            char[,] board = new char[GlobalData.arraysize, GlobalData.arraysize];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    if (i == 0 || i == board.GetLength(0) - 1)
                    {
                        board[i, m] = '-';
                    }
                    else if (m == 0 || m == board.GetLength(1) - 1)
                    {
                        board[i, m] = '!';
                    }
                    else board[i, m] = ' ';
                }
            }
            board[board.GetLength(0) / 2, board.GetLength(1) / 2] = GlobalData.snakeBody;
            Snake snake = new Snake();
            snake.SetxPos(board.GetLength(0) / 2);
            snake.SetyPos(board.GetLength(1) / 2);

            Tail tail1 = new Tail();
            tail1.SetTailx(null);
            tail1.SetTaily(null);
            snake.AddaTail(tail1);

            printBoard(board);

            randomizepos(board);
            FoodCounter = 0;
            Displayfoodsnake(board);

            
            NEAT.Main.CleanNodesValue(genome);

            int TimeUntilStravation = GlobalData.TimeUntilStravation;
            while (true)  //0 left 1 right 2 up 3 down
            {
                double[] input = inputsSnake(board, snake);
                for (int i = 0; i < input.Length; i++)
                {
                    genome.GetNodes()[i].SetValue(input[i]);
                }
                NEAT.Main.StepNetwork(genome, 5);
                double out1 = genome.GetNodes()[input.Length].GetValue();
                double out2 = genome.GetNodes()[input.Length + 1].GetValue();
                double out3 = genome.GetNodes()[input.Length + 2].GetValue();
                double out4 = genome.GetNodes()[input.Length + 3].GetValue();
                if (out1 > out2 && out1 > out3 && out1 > out4 && (snake.GetlastMove() != 1 || snake.GetTails().Count() == 0)) snake.SetlastMove(0);
                else if (out2 > out1 && out2 > out3 && out2 > out4 & (snake.GetlastMove() != 0 || snake.GetTails().Count() == 0)) snake.SetlastMove(1);
                else if (out3 > out2 && out3 > out1 && out1 > out4 & (snake.GetlastMove() != 3 || snake.GetTails().Count() == 0)) snake.SetlastMove(2);
                else if (out4 > out2 && out4 > out3 && out4 > out1 & (snake.GetlastMove() != 2 || snake.GetTails().Count() == 0)) snake.SetlastMove(3); ;
                if (Displayfoodsnake(board))
                {
                    Tail tail = new Tail();
                    tail.SetTailx(null);
                    tail.SetTaily(null);
                    snake.AddaTail(tail);
                    TimeUntilStravation += GlobalData.TimeUntilStravation;
                }
                TimeUntilStravation--;
                if (TimeUntilStravation <= 0) goto end;
                if (!Displaymovesnake(snake, board)) goto end;
                System.Threading.Thread.Sleep(GlobalData.sleeptime);
            }
            end:
            System.Threading.Thread.Sleep(GlobalData.sleeptime);
        }

        static double[] inputsSnake(char[,] board, Snake snake)
        {
            double[] d = new double[NEAT.GlobalData.InputsNum];
            List<double> inputs = new List<double>();
            int snakeS = GlobalData.SensorSize;
            int? i2 = null;
            int? m2 = null;

            int snakeheadX;
            int snakeheadY;
            char[,] grid;
            /*left*/
            if (snake.GetlastMove() == 0) { grid = BoardRotate(board, false); snakeheadX = snake.GetyPos(); snakeheadY = board.GetLength(1) - 1 - snake.GetxPos(); }
            /*right*/
            else if (snake.GetlastMove() == 1) { grid = BoardRotate(board, true); snakeheadX = board.GetLength(0) - 1 - snake.GetyPos(); snakeheadY = snake.GetxPos(); }
            /*up*/
            else if (snake.GetlastMove() == 2) { grid = board; snakeheadX = snake.GetxPos(); snakeheadY = snake.GetyPos(); }
            /*down*/
            else /*(snake.GetlastMove() == 3)*/{ grid = BoardRotate(board, true); grid = BoardRotate(grid, true); snakeheadX = board.GetLength(0) - 1 - snake.GetxPos(); snakeheadY = board.GetLength(1) - 1 - snake.GetyPos(); }

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int m = 0; m < grid.GetLength(1); m++)
                {
                    if (grid[i, m] == '*')
                    {
                        i2 = i;
                        m2 = m;
                        inputs.Add(2 * ((grid.GetLength(0) - 1) / 2 - i) / (grid.GetLength(0) - 1));
                        inputs.Add(2 * ((grid.GetLength(1) - 1) / 2 - m) / (grid.GetLength(1) - 1));
                        goto found;
                    }
                }
            }
            found:
            //inputs.Add(2 * ((grid.GetLength(0) - 1) / 2 - snakeheadX) / (grid.GetLength(0) - 1));
            //inputs.Add(2 * ((grid.GetLength(1) - 1) / 2 - snakeheadY) / (grid.GetLength(1) - 1));        
            if (i2 != null) inputs.Add((i2.Value - snake.GetxPos()) / (grid.GetLength(0) - 1));
            else inputs.Add(0);
            if (m2 != null) inputs.Add((m2.Value - snake.GetyPos()) / (grid.GetLength(1) - 1));
            else inputs.Add(0);


            int i1 = snakeheadX;
            int m1 = snakeheadY;
            int counter2 = snakeS;
            while (i1 > 0 && counter2 > 0)
            {
                i1--;
                counter2--;
                if (grid[i1, m1] == ' ') inputs.Add(0);
                else if (grid[i1, m1] == GlobalData.snakeBody || grid[i1, m1] == '!' || grid[i1, m1] == '-') inputs.Add(1);
                else if (grid[i1, m1] == '*') inputs.Add(-1);
            }
            while (counter2 > 0) { counter2--; inputs.Add(1); }
            i1 = snakeheadX;
            m1 = snakeheadY;
            counter2 = snakeS;
            while (m1 > 0 && counter2 > 0)
            {
                m1--;
                counter2--;
                if (grid[i1, m1] == ' ') inputs.Add(0);
                else if (grid[i1, m1] == GlobalData.snakeBody || grid[i1, m1] == '!' || grid[i1, m1] == '-') inputs.Add(1);
                else if (grid[i1, m1] == '*') inputs.Add(-1);
            }
            while (counter2 > 0) { counter2--; inputs.Add(1); }

            i1 = snakeheadX;
            m1 = snakeheadY;
            counter2 = snakeS;
            while (m1 < grid.GetLength(1) - 1 && counter2 > 0)
            {
                m1++;
                counter2--;
                if (grid[i1, m1] == ' ') inputs.Add(0);
                else if (grid[i1, m1] == GlobalData.snakeBody || grid[i1, m1] == '!' || grid[i1, m1] == '-') inputs.Add(1);
                else if (grid[i1, m1] == '*') inputs.Add(-1);
            }
            while (counter2 > 0) { counter2--; inputs.Add(1); }

            i1 = snakeheadX;
            m1 = snakeheadY;
            counter2 = snakeS;
            while (m1 < grid.GetLength(1) - 1 && counter2 > 0)
            {
                m1++;
                counter2--;
                if (grid[i1, m1] == ' ') inputs.Add(0);
                else if (grid[i1, m1] == GlobalData.snakeBody || grid[i1, m1] == '!' || grid[i1, m1] == '-') inputs.Add(1);
                else if (grid[i1, m1] == '*') inputs.Add(-1);
            }
            while (counter2 > 0) { counter2--; inputs.Add(1); }

            i1 = snakeheadX;
            m1 = snakeheadY;
            counter2 = snakeS;
            while (m1 < grid.GetLength(1) - 1 && i1 < grid.GetLength(1) - 1 && counter2 > 0)
            {
                m1++;
                i1++;
                counter2--;
                if (grid[i1, m1] == ' ') inputs.Add(0);
                else if (grid[i1, m1] == GlobalData.snakeBody || grid[i1, m1] == '!' || grid[i1, m1] == '-') inputs.Add(1);
                else if (grid[i1, m1] == '*') inputs.Add(-1);
            }
            while (counter2 > 0) { counter2--; inputs.Add(1); }

            i1 = snakeheadX;
            m1 = snakeheadY;
            counter2 = snakeS;
            while (m1 < grid.GetLength(1) - 1 && i1 > 0 && counter2 > 0)
            {
                m1++;
                i1--;
                counter2--;
                if (grid[i1, m1] == ' ') inputs.Add(0);
                else if (grid[i1, m1] == GlobalData.snakeBody || grid[i1, m1] == '!' || grid[i1, m1] == '-') inputs.Add(1);
                else if (grid[i1, m1] == '*') inputs.Add(-1);
            }
            while (counter2 > 0) { counter2--; inputs.Add(1); }

            i1 = snakeheadX;
            m1 = snakeheadY;
            counter2 = snakeS;
            while (m1 > 0 && i1 > 0 && counter2 > 0)
            {
                m1--;
                i1--;
                counter2--;
                if (grid[i1, m1] == ' ') inputs.Add(0);
                else if (grid[i1, m1] == GlobalData.snakeBody || grid[i1, m1] == '!' || grid[i1, m1] == '-') inputs.Add(1);
                else if (grid[i1, m1] == '*') inputs.Add(-1);
            }
            while (counter2 > 0) { counter2--; inputs.Add(1); }

            while (m1 > 0 && i1 < grid.GetLength(1) - 1 && counter2 > 0)
            {
                m1--;
                i1++;
                counter2--;
                if (grid[i1, m1] == ' ') inputs.Add(0);
                else if (grid[i1, m1] == GlobalData.snakeBody || grid[i1, m1] == '!' || grid[i1, m1] == '-') inputs.Add(1);
                else if (grid[i1, m1] == '*') inputs.Add(-1);
            }
            while (counter2 > 0) { counter2--; inputs.Add(1); }


            for (int i = 0; i < inputs.Count(); i++)
            {
                d[i] = inputs[i];
            }
            return d;
        }
        static char[,] BoardRotate(char[,] board, bool clockwise)
        {
            char[,] board1 = new char[board.GetLength(0), board.GetLength(1)];

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int m = 0; m < board.GetLength(1); m++)
                {
                    if (clockwise) board1[i, m] = board[board.GetLength(0) - m - 1, i];
                    else board1[i, m] = board[m, board.GetLength(1) - 1 - i];
                }
            }
            return board1;
        }
        static bool stop(Pool pool)
        {
            if (pool.GetSpecies().Count() > 0 && pool.GetGeneration() > 0)
            {
                Genome BestGenome = new Genome();
                pool.GetSpecies().Sort((x, y) => x.GetTopfitness().CompareTo(y.GetTopfitness()));
                pool.GetSpecies()[pool.GetSpecies().Count() - 1].GetGenomes().Sort((x, y) => x.GetFitness().CompareTo(y.GetFitness()));
                BestGenome = NEAT.Main.copyGENOME(pool.GetSpecies()[pool.GetSpecies().Count() - 1].GetGenomes()[pool.GetSpecies()[pool.GetSpecies().Count() - 1].GetGenomes().Count() - 1]);
                DisplaySnake(BestGenome);
                Console.SetCursorPosition(0, GlobalData.arraysize + 1);
                Console.Write("Generation : {0} Max Fitness : {1}", pool.GetGeneration(), pool.GetMaxfitness());
            }
	    //if (pool.GetMaxfitness()>=600) { return true; }
            //else return false;
	    return false;
        }
    }
}
