using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteractionController : MonoBehaviour
{
    private List<List<NormalNode>> activeNormalNodes = new List<List<NormalNode>>();
    private List<List<LongNode>> activeLongNodes = new List<List<LongNode>>();
    
    private void Awake(){
        for(int i = 0; i < 8; i++){
            activeNormalNodes.Add(new List<NormalNode>());
            activeLongNodes.Add(new List<LongNode>());
        }
    }

    public void AddActiveNormalNode(NormalNode node, int position){
        activeNormalNodes[position].Add(node);
    }

    public void RemoveActiveNormalNode(NormalNode node, int position){
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
            // () => activeNormalNodes[position]?[0]?.Interaction();

            return ;
        }
    }

    public void LongNodeInteraction(int position){
        try{
            activeLongNodes[position][0]?.Interaction();        
        }catch{
            return;
        }

    }

    public void LongNodeEndInteraction(int position){
        try{
            activeLongNodes[position][0]?.EndInteraction();        
        }catch{
            return;
        }
    }

    public void LongNodeTailStart(int position){
        int index = -1;

        do{
            index++;
        }while(activeLongNodes[position][index].TailExecute());
    }
}
