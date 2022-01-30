using System;
using System.Collections.Generic;
using System.Linq;

namespace Horses
{
    class Program
    {
        static readonly int rowSize = 8;
        static readonly int columnSize = 8;

        static Point start = new Point(0, 0);
        static Point aim = new Point(1, 1);

        static bool isFinished;
        static int minimalMovementsCount;

        static void Main(string[] args)
        {
            Func<Point, Point>[] rules = GetRules();

            var rootNode = new TreeNode();
            var temporaryChildrenStorage = new List<TreeNode>();

            while (isFinished == false)
            {
                if (!rootNode.Children.Any())
                {
                    rootNode.Data = start;

                    rootNode = MakeNextMove(rootNode, rules);

                    temporaryChildrenStorage = rootNode.Children;
                }

                var innerChildren = new List<TreeNode>();

                foreach (TreeNode innerNode in temporaryChildrenStorage)
                {
                    innerChildren.AddRange(MakeNextMove(innerNode, rules).Children);
                }

                temporaryChildrenStorage = innerChildren;
            }

            Console.WriteLine($"Minimal movements count:{minimalMovementsCount}");
            Console.ReadLine();
        }

        static TreeNode MakeNextMove(TreeNode node, Func<Point, Point>[] rules)
        {   
            foreach(var rule in rules)
            {
                Point point = rule.Invoke(node.Data);

                if (point.IsValid)
                {
                    var movementsCount = node.Depth+1;

                    if (point.X == aim.X && point.Y == aim.Y)
                    {
                        minimalMovementsCount = movementsCount;
                        isFinished = true;
                    }

                    var childrenNode = new TreeNode
                    {
                        Parent = node,
                        Data = point,
                        Depth = movementsCount
                    };

                    node.Children.Add(childrenNode);
                }
            }

            return node;
        }

        static Func<Point, Point>[] GetRules()
        {
            Func<Point, Point>[] rules = new Func<Point, Point>[]
            {
                (Point point) => {
                    var result = new Point(point.X-1, point.Y+2);
                    if (result.X > 0 && result.Y < columnSize)
                    {
                        return result;
                    }

                    result.IsValid = false;
                    return result;
                },
                (Point point) => {
                    var result = new Point(point.X+1, point.Y+2);
                    if (result.X < rowSize && result.Y < columnSize)
                    {
                        return result;
                    }

                    result.IsValid = false;
                    return result;
                },
                (Point point) => {
                    var result = new Point(point.X+2, point.Y+1);
                    if (result.X < rowSize && result.Y < columnSize)
                    {
                        return result;
                    }

                    result.IsValid = false;
                    return result;
                },
                (Point point) => {
                    var result = new Point(point.X+2, point.Y-1);
                    if (result.X < rowSize && result.Y > 0)
                    {
                        return result;
                    }

                    result.IsValid = false;
                    return result;
                },
                (Point point) => {
                    var result = new Point(point.X+1, point.Y-2);
                    if (result.X < rowSize && result.Y > 0)
                    {
                        return result;
                    }

                    result.IsValid = false;
                    return result;
                },
                (Point point) => {
                    var result = new Point(point.X-1, point.Y-2);
                    if (result.X > 0 && result.Y > 0)
                    {
                        return result;
                    }

                    result.IsValid = false;
                    return result;
                },
                (Point point) => {
                    var result = new Point(point.X-2, point.Y-1);
                    if (result.X > 0 && result.Y > 0)
                    {
                        return result;
                    }

                    result.IsValid = false;
                    return result;
                },
                (Point point) => {
                    var result = new Point(point.X-2, point.Y+1);
                    if (result.X > 0 && result.Y < columnSize)
                    {
                        return result;
                    }

                    result.IsValid = false;
                    return result;
                },
            };

            return rules;
        }
    }
}
