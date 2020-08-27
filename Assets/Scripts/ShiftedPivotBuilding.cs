using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftedPivotBuilding : Building
{
    public override void SetPosition(Vector3 position, bool onLoad)
    {
        if(!onLoad)
            base.SetPosition(position + (transform.up * collider.bounds.extents.y));
        else
            base.SetPosition(position);
    }
}
