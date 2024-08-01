using System;
using System.IO;
using System.Collections.Generic;

namespace Baekjoon {
    class Program {
        static int[,] array;
        static int xsize = 0;
        static int ysize = 0;

        static Program() {
            array = new int[0, 0];
        }
        
        static void Main() {
            using (StreamReader sr = new StreamReader(new BufferedStream(Console.OpenStandardInput())))
            using (StreamWriter sw = new StreamWriter(new BufferedStream(Console.OpenStandardOutput()))) {
                string[] sizeLine = sr.ReadLine()?.Split() ?? throw new InvalidOperationException("Input size line is null.");
                if (sizeLine.Length != 2) throw new FormatException("Size input format is incorrect.");
                ysize = int.Parse(sizeLine[0]);
                xsize = int.Parse(sizeLine[1]);
                
                array = new int[ysize, xsize];
                
                for (int y = 0; y < ysize; y++) {
                    string line = sr.ReadLine() ?? throw new InvalidOperationException("Input line is null.");
                    if (line.Length != xsize) throw new FormatException("Maze row length does not match expected width.");
                    for (int x = 0; x < xsize; x++) {
                        array[y, x] = line[x] - '0';
                    }
                }

                int[] dy = { -1, 0, 1, 0 };
                int[] dx = { 0, -1, 0, 1 };
                
                Queue<(int, int)> queue = new Queue<(int, int)>();
                array[0, 0] = 2;
                queue.Enqueue((0, 0));
                
                while (queue.Count > 0) {
                    var (y, x) = queue.Dequeue();
                    int currentStep = array[y, x];

                    if (y == ysize - 1 && x == xsize - 1) {
                        sw.WriteLine(currentStep - 1);
                        return;
                    }
                    
                    for (int d = 0; d < 4; d++) {
                        int ny = y + dy[d];
                        int nx = x + dx[d];
                        
                        if (ny >= 0 && ny < ysize && nx >= 0 && nx < xsize && array[ny, nx] == 1) {
                            array[ny, nx] = currentStep + 1;
                            queue.Enqueue((ny, nx));
                        }
                    }
                }
                
                sw.WriteLine(-1);
            }
        }
    }
}
