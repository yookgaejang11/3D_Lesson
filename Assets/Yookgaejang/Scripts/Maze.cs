using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Yookgaejang
{
    [SerializeField]
    public class MapLocation
    {
        //위치값
        public int x;
        public int z;
        //생성시 위치 넣기
        public MapLocation(int _x, int _z)//생성자
        {
            x = _x;
            z = _z;
        }
        //내 현재 위치 값을 vector2 값으로 변환
        public Vector2 ToVector()
        {
            return new Vector2(x, z);
        }

        /// <summary>
        /// operator + 정의, 람다식
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static MapLocation operator +(MapLocation a,MapLocation b)//operator 더하기 자체를 변형(연산 구조에 oo를 추가하겠다 ex:vector 연산
            => new MapLocation(a.x + b.x, a.z + b.z);

        /*public static MapLocation operator +(MapLocation a, MapLocation b)위에 코드랑 같음(람다식 쓸때와 안쓸때)
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
        public byte[,] map;//맵구조 2차원 배열
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
            GameObject Enemy = GameObject.CreatePrimitive(PrimitiveType.Sphere);   //유니티가 가지고 있는것들(큐브)생성할 수 있게
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
                    map[x, z] = 1; //1은 벽, 0은 통로
                }
            }
        }

        public virtual void Generate()//코드가 있든말든 상관 안함(가상) 상속받을 애에서 generator을 사용하기 위한 껍데기 함수 (걍 이런게 있다. 안만들어도 된다. 상속받을 애가 오버라이드 해서 동작 시킨다
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
                        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);   //유니티가 가지고 있는것들(큐브)생성할 수 있게
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
                        GameObject plane = GameObject.CreatePrimitive(PrimitiveType.Plane);   //유니티가 가지고 있는것들(큐브)생성할 수 있게
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
