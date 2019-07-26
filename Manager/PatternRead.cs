using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField]
    private Image background;

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
        for (int i = 0; i < fileStrs.Count; i++)
        {
            string str = fileStrs[0];
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

                if (spString[0].Equals("*"))
                {
                    StartCoroutine(ChangeBackground(int.Parse(spString[1])));
                }

                fileStrs.Remove(fileStrs[0]);
            }
   
        }
    }

    private IEnumerator ChangeBackground(int num) {
        yield return StartCoroutine(GameManager.instance.IFadeOut(background, 0.05f));
        background.sprite = StageManager.instance.backgorunds[num];
        StartCoroutine(GameManager.instance.IFadeIn(background, 0.01f));
    }

}
