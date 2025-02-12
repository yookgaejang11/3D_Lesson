using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Yookgaejang
{
    public static class Extensions//static Ŭ����(new�� �Ⱦ�(�޸𸮿� �Ҵ礷�Ϸ��� ���°ǵ� ��¤� �Ҵ���ص��� �� �̾�) ����Ŭ������ ���ε� ��� �����̾�ߵ�
    {
        private static System.Random rng = new System.Random();

        public static void Shuffle<T>(this IList<T> list)//<t>�� type �������� ���襤
        {
            int n = list.Count;
            //����Ʈ�� ũ�⸸ŭ �ݺ�
            while (n > 1)
            {
                n--;
                //0���� ũ�ų� ���� int32.MaxValue ���� ���� 32��Ʈ ��ȣ �ִ�
                int k = rng.Next(n + 1);
                T value= list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
   
}
