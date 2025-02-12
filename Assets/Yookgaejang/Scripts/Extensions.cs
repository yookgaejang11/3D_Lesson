using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Yookgaejang
{
    public static class Extensions//static 클래스(new룰 안씀(메모리에 할당ㅇ하려고 쓰는건데 얘는ㄴ 할당안해도됨 걍 이씀) 정적클래스는 내부도 모두 정적이어야됨
    {
        private static System.Random rng = new System.Random();

        public static void Shuffle<T>(this IList<T> list)//<t>는 type 지정되지 안흥ㄴ
        {
            int n = list.Count;
            //리스트의 크기만큼 반복
            while (n > 1)
            {
                n--;
                //0보다 크거나 같고 int32.MaxValue 보다 작은 32비트 부호 있는
                int k = rng.Next(n + 1);
                T value= list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
   
}
