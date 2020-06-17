using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NaughtyAttributes;
public class NodeGenerator : MonoBehaviour
{

    [Header("Node Objects")]
    [SerializeField]
    private GameObject normalNodeObject;

    [SerializeField]
    private GameObject longNodeObject;

    private List<Node> normalNodes = new List<Node>();
    private List<Node> longNodes = new List<Node>();

    [Header("Transforms")]
    [SerializeField]
    private Transform[] startTransforms;

    [SerializeField]
    private Transform[] endTransforms;

    private List<Vector2> startPositions = new List<Vector2>();
    private List<Vector2> endPositions = new List<Vector2>();

    [Header("Events")]
    [SerializeField]
    private IntEvent longNodeStopEvent;

    private void Awake(){
        Node[] tempNodes = normalNodeObject.GetComponentsInChildren<NormalNode>(true);
        normalNodes = tempNodes.ToList();

        tempNodes = longNodeObject.GetComponentsInChildren<LongNode>(true);
        longNodes = tempNodes.ToList();

        for(int i = 0; i < startTransforms.Length; i++){
            startPositions.Add(startTransforms[i].position);
        }
        
        for(int i = 0; i < endTransforms.Length; i++){
            endPositions.Add(endTransforms[i].position);
        }
    }

    private void Start(){
        Coroutine().Start(this);
    }

    public IEnumerator Coroutine(){
        while(true){
            // NormalNodeGenerate();
            LongNodeGenerate();
            yield return YieldInstructionCache.WaitSeconds(2.0f);
        }
    }

    public void NormalNodeGenerate(int index){
        Node node = GetAvaliableNode(normalNodes);
        node.Execute(startPositions[index], endPositions[index], index);
    }

    [Button("Normal Node Generate")]
    public void NormalNodeGenerate(){
        int index = Random.Range(0, startPositions.Count);

        Node node = GetAvaliableNode(normalNodes);
        node.Execute(startPositions[index], endPositions[index], index);
    }

    public void LongNodeGenerate(int index){
        Node node = GetAvaliableNode(longNodes);
        node.Execute(startPositions[index], endPositions[index], index);
    }

    [Button("Long Node Generate")]
    public void LongNodeGenerate(){
        int index = Random.Range(0, startPositions.Count);

        Node node = GetAvaliableNode(longNodes);
        node.Execute(startPositions[index], endPositions[index], index);

        IEnumerator coroutine() {
            yield return YieldInstructionCache.WaitSeconds(2.25f);
            LongNodeStop(index);
        }

        coroutine().Start(this);
    }

    public void LongNodeStop(int index){
        longNodeStopEvent.Invoke(index);
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
