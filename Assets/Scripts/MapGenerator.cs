using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
   public Color ColorFloor = Color.white;
   public Color ColorWall = Color.red;
   //public Color colorCurveWall = Color.green;
   //public color ColorEdgeWall =Color.blue;
    //�� ���� ��ġ
    public Color ColorResponse = new Color(64,128,128);

    public Transform Terrain;
    public Texture2D MapInfo;
    public float tileSize = 4.0f;
    private int mapWidth;
    private int mapHeight;
    public GameObject Floor;
    public GameObject Wall;
    public GameObject Floor_Response;

    public void BuildGenerator()
    {
        GenerateMap();
    }


    private void GenerateMap()
    {
        mapWidth = MapInfo.width; //52
        mapHeight = MapInfo.height; //44

        Color[] pixels = MapInfo.GetPixels();

        for(int i = 0; i < mapHeight; i++)
        {
            for(int j = 0; j < mapWidth; j++)
            {
                Color pixelcolor = pixels[i * mapHeight + j]; //0
                Debug.Log(pixelcolor);
                //�ٴ�
                if(pixelcolor == ColorFloor)
                {
                    GameObject floor = GameObject.Instantiate(Floor,Terrain);//floor�̶�� �������� terrain�̶�� �θ� �ְڴ�
                    floor.transform.position = new Vector3(j * tileSize,0, i * tileSize);
                }
                //��
                if(pixelcolor == ColorWall)
                {
                    GameObject wall = GameObject.Instantiate(Wall, Terrain);
                    wall.transform.position = new Vector3(j * tileSize, 0, i * tileSize);
                    
                }
                if(pixelcolor == ColorResponse)
                {
                    GameObject floor = GameObject.Instantiate(Floor_Response, Terrain);
                    floor.transform.position = new Vector3(j    * tileSize, 0, i * tileSize);
                }
            }
        }
    }
}
