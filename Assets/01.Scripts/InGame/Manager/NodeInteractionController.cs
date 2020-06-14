using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteractionController : MonoBehaviour
{
    private List<List<Node>> activeNormalNodes = new List<List<Node>>();
    private List<List<LongNode>> activeLongNodes = new List<List<LongNode>>();
    
    private void Awake(){
        for(int i = 0; i < 8; i++){
            activeNormalNodes.Add(new List<Node>());
            activeLongNodes.Add(new List<LongNode>());
        }
    }

    public void AddActiveNormalNode(Node node, int position){
        activeNormalNodes[position].Add(node);
    }

    public void RemoveActiveNormalNode(Node node, int position){
        activeNormalNodes[position].Remove(node);
    }

    public void AddActiveLongNode(LongNode node, int position){
        activeLongNodes[position].Add(node as LongNode);
    }

    public void RemoveActiveLongNode(LongNode node, int position){
        activeLongNodes[position].Remove(node);
    }

    public void NoramlNodeInteraction(int position){
        try{
            activeNormalNodes[position][0]?.Interaction();
        }catch{
            // FIXME : Empty catch
            return ;
        }
    }

    public void LongNodeInteraction(int position){
        
    }

    public void LongNodeTailStart(int position){
        activeLongNodes[position][0].TailStart();
    }
}
