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

    private LongNode curLongNode;

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
                // Debug.Log("Beat : " + nowBeat + ", PatternNumber : " + spString[2]);

                if (spString[0].Equals("@"))
                {
                    GameObject newObject = Instantiate(normalNode, nodePos[int.Parse(spString[1])].transform.position, Quaternion.identity);
                    nodePos[int.Parse(spString[1])].GetComponent<SpawnArea>().nodeList.Add(newObject.GetComponent<Node>());

                }

                if (spString[0].Equals("#"))
                {
                    curLongNode = Instantiate(longNode, nodePos[int.Parse(spString[1])].transform.position, Quaternion.identity).GetComponent<LongNode>();
                    //nodePos[int.Parse(spString[1])].GetComponent<SpawnArea>().nodeList.Add(curLongNode.GetComponent<Node>());
                    curLongNode.area = nodePos[int.Parse(spString[1])].GetComponent<SpawnArea>();
                    curLongNode.LongNodeStart();
                }

                if (spString[0].Equals("$"))
                {
                    curLongNode.LongNodeEnd();

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
