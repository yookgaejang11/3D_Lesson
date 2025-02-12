using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Yookgaejang
{
    [SerializeField]
    public class MapLocation
    {
        //��ġ��
        public int x;
        public int z;
        //������ ��ġ �ֱ�
        public MapLocation(int _x, int _z)//������
        {
            x = _x;
            z = _z;
        }
        //�� ���� ��ġ ���� vector2 ������ ��ȯ
        public Vector2 ToVector()
        {
            return new Vector2(x, z);
        }

        /// <summary>
        /// operator + ����, ���ٽ�
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static MapLocation operator +(MapLocation a,MapLocation b)//operator ���ϱ� ��ü�� ����(���� ������ oo�� �߰��ϰڴ� ex:vector ����
            => new MapLocation(a.x + b.x, a.z + b.z);

        /*public static MapLocation operator +(MapLocation a, MapLocation b)���� �ڵ�� ����(���ٽ� ������ �Ⱦ���)
        {
            return new MapLocation(a.x + b.x, a.z + b.z);
        }*/

    }
    public class Maze : MonoBehaviour
    {
        public List<MapLocation> directions = new List<MapLocation>() {
                                              new MapLocation(1,0),
                                              new MapLocation(0,1),
                                              new MapLocation(-1,0),
                                              new MapLocation(0, -1)
        };

        public int width = 30;
        public int depth = 30;
        public byte[,] map;//�ʱ��� 2���� �迭
        public List<Vector3> hall = new List<Vector3>();
        public bool isSpawn;
        public int scale = 6;
        // Start is called before the first frame update
        void Start()
        {
            InitialiseMap();
            Generate();
            DrawMap();
            StartCoroutine("SpawnEnemy");
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        IEnumerator SpawnEnemy()
        {

            isSpawn = false;
            int number = Random.Range(0, hall.Count);
            GameObject Enemy = GameObject.CreatePrimitive(PrimitiveType.Sphere);   //����Ƽ�� ������ �ִ°͵�(ť��)������ �� �ְ�
            Enemy.transform.localScale = new Vector3(scale, scale, scale);
            Enemy.transform.localPosition = hall[number];
            hall.RemoveAt(number);
            yield return new WaitForSeconds(1);
            if(hall.Count <= 0)
            {
                yield return null;
            }

            StartCoroutine("SpawnEnemy");

        }
        void InitialiseMap()
        {
            map = new byte[width,depth];
            for(int z = 0; z < depth ; z++)
            {
                for(int x = 0; x < width ; x++)
                {
                    map[x, z] = 1; //1�� ��, 0�� ���
                }
            }
        }

        public virtual void Generate()//�ڵ尡 �ֵ縻�� ��� ����(����) ��ӹ��� �ֿ��� generator�� ����ϱ� ���� ������ �Լ� (�� �̷��� �ִ�. �ȸ��� �ȴ�. ��ӹ��� �ְ� �������̵� �ؼ� ���� ��Ų��
        {
            /*
             
             */
        }

        void DrawMap()
        {
            for(int z = 0; z < depth ; z++)
            {
                for (int x = 0;x < width ; x++)
                {
                    if (map[x,z] == 1)
                    {
                        Vector3 pos = new Vector3(x * scale, 0, z * scale);
                        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);   //����Ƽ�� ������ �ִ°͵�(ť��)������ �� �ְ�
                        wall.transform.localScale = new Vector3 (scale, scale, scale);
                        wall.transform.localPosition = pos;
                    }
                    else if(map[x,z] == 0)
                    {
                        Vector3 pos = new Vector3(x * scale, 0, z * scale);
                        hall.Add(pos);
                    }

                }
            }

            for (int z = 0; z < depth; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (map[x, z] == 1)
                    {
                        Vector3 pos = new Vector3(x * scale, 0, z * scale);
                        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);   //����Ƽ�� ������ �ִ°͵�(ť��)������ �� �ְ�
                        plane.transform.localScale = new Vector3(scale,scale,scale);
                        plane.transform.localPosition = new Vector3(pos.x, -2.1f, pos.z);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public int CountSquareNeighbours(int x, int z)
        {
            int count = 0;
            if(x <= 0 || x >= width - 1 || z <= 0|| z >= depth -1)
            {
                return 5;
            }
            if (map[x-1,z] == 0)
            {
                count++;
            }
            if (map[x + 1,z] == 0)
            {
                count++;
            }
            if (map[x, z + 1] == 0)
            {
                count++;
            }
            if (map[x,z-1] == 0)
            {
                count++;
            }
            return count;
        }

        
    }
}
