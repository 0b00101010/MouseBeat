using System;
using UnityEngine;

[Serializable]
public class SongProcessAction{
    [SerializeField]
    private int _position;
    
    [SerializeField]
    private Action _action;

    public int position{
        get{
            return _position;
        }

        set{
            _position = value;
        }
    }

    public Action action{
        get{
            return _action;
        }

        set{
            _action = value;
        }
    }

    public static int generateSequence = 1;
}