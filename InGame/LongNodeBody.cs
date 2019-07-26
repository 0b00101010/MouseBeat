using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNodeBody : Node
{

    public override void Hit(){   }

    private void Start()
    {
        moveVector = new Vector3 (0, -0.3f, 0);
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -4)
            Destroy(gameObject);
    }

}
