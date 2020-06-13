using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;
public class NodeGenerator : MonoBehaviour
{

    [Header("Node Objects")]
    [SerializeField]
    private GameObject normalNodeObjects;

    private List<Node> normalNodes = new List<Node>();

    [Header("Transforms")]
    [SerializeField]
    private Transform[] startTransforms;

    [SerializeField]
    private Transform[] endTransforms;

    private List<Vector2> startPositions = new List<Vector2>();
    private List<Vector2> endPositions = new List<Vector2>();


    private void Awake(){
        Node[] tempNodes = normalNodeObjects.GetComponentsInChildren<NormalNode>(true);
        normalNodes = tempNodes.ToList();

        for(int i = 0; i < startTransforms.Length; i++){
            startPositions.Add(startTransforms[i].position);
        }
        
        for(int i = 0; i < endTransforms.Length; i++){
            endPositions.Add(endTransforms[i].position);
        }
    }

    public void NormalNodeGenerate(int index){
        Node node = GetAvaliableNode(normalNodes);
        node.Execute(startPositions[index], endPositions[index]);
    }

    [Button("Normal Node Generate")]
    public void NormalNodeGenerate(){
        int index = Random.Range(0, startPositions.Count);
        
        Node node = GetAvaliableNode(normalNodes);
        node.Execute(startPositions[index], endPositions[index]);
    }

    private Node GetAvaliableNode(List<Node> nodes){
        for(int i = 0; i < nodes.Count; i++){
            if(!nodes[i].gameObject.activeInHierarchy){
                return nodes[i];
            }
        }

        return null;
    }


}
