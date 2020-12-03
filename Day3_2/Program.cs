using System;
using System.Threading.Tasks;
using Input;

namespace Day3_2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            InputReader.SessionKey = "your key";

            var map = (await InputReader.GetInput(3)).Split('\n',
                                                                      StringSplitOptions.RemoveEmptyEntries);
            long way1 = GetCollisionsCount(map, p =>
            {
                p.x += 1;
                p.y += 1;
                return p;
            });
            long way2 = GetCollisionsCount(map, p =>
            {
                p.x += 3;
                p.y += 1;
                return p;
            });
            long way3 = GetCollisionsCount(map, p =>
            {
                p.x += 5;
                p.y += 1;
                return p;
            });
            long way4 = GetCollisionsCount(map, p =>
            {
                p.x += 7;
                p.y += 1;
                return p;
            });
            long way5 = GetCollisionsCount(map, p =>
            {
                p.x += 1;
                p.y += 2;
                return p;
            });

            Console.WriteLine(way1 * way2 * way3 * way4 * way5);
        }

        static int GetCollisionsCount(string[] map,
                                      Func<(int x,int y), (int x, int y)> movement)
        {
            var position = (x:0, y:0);
            var mapWidth = map[0].Length;
            var collisions = 0;
            while (position.y < map.Length)
            {
                if (position.x >= mapWidth)
                {
                    position.x -= mapWidth;
                }
                if (map[position.y][position.x] == '#')
                {
                    collisions++;
                }
                position = movement(position);
            }
            return collisions;
        }
    }
}
