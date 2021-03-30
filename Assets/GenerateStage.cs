using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GenerateStage : MonoBehaviour
{
    [SerializeField]
    private GameObject Stage;

    [SerializeField]
    private TextAsset textFile;
 
    private string[] textData;
 
    private int raw;
    private int col;
 
    [SerializeField]
    private GameObject WallPrefab;
    [SerializeField]
    private GameObject FloorPrefab;
 
    private void Start()
    {
        string textLines = textFile.text;
 
        // 改行でデータを分割して配列に代入
        textData = textLines.Split('\n');
 
        col = textData[0].Split(',').Length;
        raw = textData.Length;
 
        for(int i = 0; i < raw; i++)
        { 
            string[] tempWords = textData[i].Split(',');

            for(int j = 0; j < col; j++)
            {
                switch (tempWords[j])
                    {
                        case "":
                            Instantiate(FloorPrefab, new Vector3(10f * (j - ((col - 1) / 2)), 0f, 10f * (i - ((raw - 1) / 2))), Quaternion.identity, Stage.transform);
                            break;
 
                        case "1":
                        default:
                            Instantiate(WallPrefab, new Vector3(10f * (j - ((col - 1) / 2)), 5f, 10f * (i - ((raw - 1) / 2))), Quaternion.identity, Stage.transform);
                            break;
                    }
                }
            }
        }
    }