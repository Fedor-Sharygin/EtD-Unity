using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : Reactive
{

    [SerializeField] private Vector3 mHidePosition;
    public override void React()
    {
        transform.position = mHidePosition;
    }

}
