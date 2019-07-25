using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternRead : MonoBehaviour
{
    private List<string> fileStrs = new List<string>();
    private readonly char line = '/';
    private TextAsset file;

    [SerializeField]
    private GameObject[] nodePos;

    [SerializeField]
    private GameObject normalNode;

    [SerializeField]
    private GameObject longNode;

    public void ReadFile(string filePath)
    {
        file = Resources.Load(filePath) as TextAsset;
        string text = file.text;
        string[] strs = text.Split('\n');
        foreach (string str in strs)
            fileStrs.Add(str);
    }

    public void CreateNode(int nowBeat)
    {
        foreach (string str in fileStrs)
        {
                string[] spString = str.Split(line);

            if (int.Parse(spString[2]).Equals(nowBeat))
            {
                Debug.Log("Beat : " + nowBeat + ", PatternNumber : " + spString[2]);

                if (spString[0].Equals("@"))
                {
                    Instantiate(normalNode, nodePos[int.Parse(spString[1])].transform.position, Quaternion.identity);
                }

                if (spString[0].Equals("#"))
                {

                }

                if (spString[0].Equals("$"))
                {

                }

                fileStrs.Remove(fileStrs[0]);
                break;
            }
            else
            {
                break;
            }

        }
    }

}
