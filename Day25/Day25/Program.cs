using System;
using System.Collections.Generic;
using System.Linq;

namespace Day25
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
            // Part2();
        }

        static void Part1()
        {
            long doorPK = 19774466; // 17807724;
            long cardPK = 7290641; // 5764801;
            long subjectNum = 7;

            long doorLoopSize = 0;
            long cardLoopSize = 0;

            long value = 1;
            while (value != cardPK)
            {
                cardLoopSize++;
                value *= subjectNum;
                value %= 20201227;
            }
            
            value = 1;
            while (value != doorPK)
            {
                doorLoopSize++;
                value *= subjectNum;
                value = value % 20201227;
            }

            long encryptionKey = 1;
            for (int i = 0; i < cardLoopSize; i++)
            {
                encryptionKey *= doorPK;
                encryptionKey %= 20201227;
            }

            Console.WriteLine(encryptionKey);
        }

        static void Part2()
        {

        }
    }
}
