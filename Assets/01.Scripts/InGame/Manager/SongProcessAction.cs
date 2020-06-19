using System;
using UnityEngine;

[Serializable]
public class SongProcessAction{
    [SerializeField]
    private float _position;
    
    [SerializeField]
    private Action _action;

    public float position{
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
}