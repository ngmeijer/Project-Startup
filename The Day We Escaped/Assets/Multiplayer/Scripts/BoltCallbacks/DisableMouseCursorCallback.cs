using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMouseCursorCallback : ConfigPlayerOwnerCallbackBase
{
    protected override void PlayerAttached(BoltEntity playerEntity)
    {
        Cursor.visible = false;
    }
}
