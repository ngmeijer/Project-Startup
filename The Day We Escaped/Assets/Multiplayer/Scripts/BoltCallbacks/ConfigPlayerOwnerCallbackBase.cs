using System.Collections;
using UnityEngine;

public abstract class ConfigPlayerOwnerCallbackBase : Bolt.GlobalEventListener
{
    public override void EntityAttached(BoltEntity entity)
    {
        if (entity.StateIs<IPlayerTDWEState>() && entity.IsOwner)
        {
            StartCoroutine(ExecuteAtEndOfFrame());
        }
    }

    private IEnumerator ExecuteAtEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        
        
    }

    protected abstract void PlayerAttached(BoltEntity playerEntity);
}