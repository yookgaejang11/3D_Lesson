using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MapGenerator myGenerator = (MapGenerator)target;
        if(GUILayout.Button("���� �����մϴ�"))
        {
            myGenerator.BuildGenerator();
        }
    }
    //����Ƽ Guiâ �ǵ�� ��ũ��Ʈ
}
