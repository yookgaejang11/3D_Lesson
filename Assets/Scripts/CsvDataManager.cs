using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public enum GameScore
{
    No,
    User,
    Score
}
public class CsvDataManager : MonoBehaviour
{
    [SerializeField]
    public TextAsset csvFile;

    //csv를 나누는 기준
    char lineSeperator = '\n';

    public class CsvData
    {
        public int No;
        public string User;
        public int Score;
    }
    public Dictionary<string, CsvData> dataDictionary = new Dictionary<string, CsvData> ();
    void Start()
    {
        #region csv에 자료 추가
        /*CsvData data = new CsvData ();
        data.No = 1;
        data.User = "yookgaejang11";
        data.Score = 777;
        dataDictionary.Add("1", data);

        CsvData data1 = new CsvData ();
        data1.No = 2;
        data1.User = "louie123";
        data1.Score = 444;
        dataDictionary.Add("2", data1);

        CsvData data2 = new CsvData ();
        data2.No = 3;
        data2.User = "KimMinCheol";
        data2.Score = 111;
        dataDictionary.Add("3", data2);
        CreateCSV();
        */
        #endregion
        //csv 자료 확인
        CsvToDictionary();
        /*Debug.Log(dataDictionary["1"].No + dataDictionary["1"].User + dataDictionary["1"].Score);
        Debug.Log(dataDictionary["2"].No + dataDictionary["2"].User + dataDictionary["2"].Score);
        Debug.Log(dataDictionary["3"].No + dataDictionary["3"].User + dataDictionary["3"].Score);*/

        for (int i = 1; i <= dataDictionary.Count; i++)
        {
            //objname == 오브젝트 키 값, 딕셔너리 값(No)
            Debug.Log("Num" + GetObjData(i.ToString(), GameScore.No)+ " " + "User" + GetObjData(i.ToString(), GameScore.User) + " " +"Score" + GetObjData(i.ToString(), GameScore.Score));
        }

        /*string[] datas = csvFile.text.Split("\n");

        foreach (string s in datas)
        {
            Debug.Log(s);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CsvToDictionary()
    {
        string[] records = csvFile.text.Split(lineSeperator);

        int lineCount = 0;
        
        foreach(string record in records)
        {
            lineCount++;

            string[] fields = record.Split(',');

            if(string.IsNullOrEmpty(fields[0]))
            {
                break;
            }

            CsvData objData = new CsvData
            {
                No = int.Parse(fields[0]),//Parse = 자기형에 따라서 자료형을 변환하겠다 int.Parse == int형으로 변환 int로 변환이 될 수 있는 자료형이어야 됨
                User = fields[1],
                Score = int.Parse(fields[2])
            };

            if(!dataDictionary.ContainsKey(fields[0]))
            {
                dataDictionary.Add(fields[0], objData);
                Debug.Log("Added: " + objData.No);

            }
            else
            {
                Debug.Log("duplicated object name exists");
            }
        }
        
    }

    public string GetObjData(string objName, GameScore dataName)
    {
        string data = "";

        if(!dataDictionary.ContainsKey(objName))
        {
            data = "None";
            return data;
        }

        switch (dataName)//
        {
            case GameScore.No:
                data = dataDictionary[objName].No.ToString();
                break;
            case GameScore.User:
                data = dataDictionary[objName].User;
                break;
            case GameScore.Score:
                data = dataDictionary[objName].Score.ToString();
                break;
        }

        return data;
    }

    public void CreateCSV()
    {
        //Debug.Log(Application.dataPath + @"\my.csv"); @ = 뒤에 통로 그대로 읽어라 \ 두개 쓸 필요 없이
        //Debug.Log(Application.dataPath + "\\my.csv");\ = 내가 문자르 쓰겠습니다 걍 그대로 경로 읽어주세요

        using (StreamWriter writer = new StreamWriter(Application.dataPath + @"\my.csv"))//using = 안에가 제대로 동작하고 있을 때만 Application.dataPath는 윈도우에만, Application.persistentDataPath는 안드로이드 용(내 프로젝트 경로 안)
        {
            foreach (var data in dataDictionary)
            {
                writer.WriteLine("{0},{1},{2}", data.Value.No, data.Value.User, data.Value.Score);
            }
        }
    }
}
