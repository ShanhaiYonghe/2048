using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Program
    {
        const int MATRIX_NUM = 4;
        static Dictionary<Tuple<int, int>, int> _dic = new Dictionary<Tuple<int, int>, int>();

        static Random _random = new Random();
        static int _step = 0;

        static void Main(string[] args)
        {
            _dic.Clear();
            _step = 0;

            for (int row = 0; row < MATRIX_NUM; row++)
            {
                for (int col = 0; col < MATRIX_NUM; col++)
                {
                    _dic.Add(new Tuple<int, int>(row, col), 0);
                }
            }

            Console.WriteLine("Use ↑ ↓ ← →");

            while (Read())
            {
            }
            Console.WriteLine("GameOver");

            while (true)
                Console.ReadKey();
        }
        private static bool Read()
        {
            return Calc(Console.ReadKey(true).Key);
        }
        private static bool Calc(ConsoleKey key)
        {
            #region Calc

            List<int> list = new List<int>();

            if (key == ConsoleKey.UpArrow)
            {
                for (int col = 0; col < MATRIX_NUM; col++)
                {
                    list = _dic.Where(item => item.Key.Item2 == col).OrderBy(item => item.Key.Item1).Select(item => item.Value).ToList();
                    list = CalcSingle(list);

                    for (int index = 0; index < MATRIX_NUM; index++)
                    {
                        _dic[new Tuple<int, int>(index, col)] = list[index];
                    }
                }
            }
            else if (key == ConsoleKey.DownArrow)
            {
                for (int col = 0; col < MATRIX_NUM; col++)
                {
                    list = _dic.Where(item => item.Key.Item2 == col).OrderByDescending(item => item.Key.Item1).Select(item => item.Value).ToList();
                    list = CalcSingle(list);

                    for (int index = 0; index < MATRIX_NUM; index++)
                    {
                        _dic[new Tuple<int, int>(index, col)] = list[index];
                    }
                }
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                for (int row = 0; row < MATRIX_NUM; row++)
                {
                    list = _dic.Where(item => item.Key.Item1 == row).OrderBy(item => item.Key.Item2).Select(item => item.Value).ToList();
                    list = CalcSingle(list);

                    for (int index = 0; index < MATRIX_NUM; index++)
                    {
                        _dic[new Tuple<int, int>(row, index)] = list[index];
                    }
                }
            }
            else if (key == ConsoleKey.RightArrow)
            {
                for (int row = 0; row < MATRIX_NUM; row++)
                {
                    list = _dic.Where(item => item.Key.Item1 == row).OrderByDescending(item => item.Key.Item2).Select(item => item.Value).ToList();
                    list = CalcSingle(list);

                    for (int index = 0; index < MATRIX_NUM; index++)
                    {
                        _dic[new Tuple<int, int>(row, index)] = list[index];
                    }
                }
            }
            else if (key == ConsoleKey.Backspace)
            {
                return false;
            }
            else
            {
                return true; //输入非法
            }

            #endregion

            bool flag = false;
            foreach (var tuple in _dic)
            {
                if (tuple.Value == 0)
                {
                    flag = true;
                    break;
                }
            }

            if (flag)
            {
                var listEmpty = _dic.Where(item => item.Value == 0).Select(item => item.Key).ToList();
                var tuple = listEmpty[_random.Next(listEmpty.Count)];
                _dic[tuple] = 2;

                _step++;
                Console.WriteLine("===============" + key.ToString() + "    Step:" + _step);
                Print();
            }

            if (!flag)
            {
                Tuple<int, int> tuple1 = null;
                Tuple<int, int> tuple2 = null;
                Tuple<int, int> tuple3 = null;

                for (int row = 0; row < MATRIX_NUM; row++)
                {
                    for (int col = 0; col < MATRIX_NUM; col++)
                    {
                        tuple1 = new Tuple<int, int>(row, col);
                        tuple2 = new Tuple<int, int>(row, col + 1);
                        tuple3 = new Tuple<int, int>(row + 1, col);

                        if (_dic.ContainsKey(tuple2) && _dic[tuple1] == _dic[tuple2])
                        {
                            flag = true;
                            break;
                        }
                        if (_dic.ContainsKey(tuple3) && _dic[tuple1] == _dic[tuple3])
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag)
                        break;
                }
            }
            return flag;
        }
        private static List<int> CalcSingle(List<int> list)
        {
            int index = 0;
            list = RemoveEmpty(list);
            while (index < MATRIX_NUM - 1)
            {
                if (list[index] > 0 && list[index + 1] == list[index])
                {
                    list[index] *= 2;
                    list[index + 1] = 0;
                    list = RemoveEmpty(list);
                }
                index++;
            }
            list = RemoveEmpty(list);

            return list;
        }
        private static List<int> RemoveEmpty(List<int> list)
        {
            List<int> listNew = new List<int>();
            foreach (var item in list)
            {
                if (item > 0)
                    listNew.Add(item);
            }
            for (int i = listNew.Count; i < list.Count; i++)
            {
                listNew.Add(0);
            }

            return listNew;
        }
        private static void Print()
        {
            for (int row = 0; row < MATRIX_NUM; row++)
            {
                for (int col = 0; col < MATRIX_NUM; col++)
                {
                    Console.Write(_dic[new Tuple<int, int>(row, col)].ToString());
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
