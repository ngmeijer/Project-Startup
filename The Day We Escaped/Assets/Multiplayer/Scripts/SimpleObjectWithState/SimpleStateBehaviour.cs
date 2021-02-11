using UnityEngine;
using UnityTemplateProjects.CustomUnityEvents;

public class SimpleStateBehaviour : Bolt.EntityEventListener<ISimpleIntState>
{
    public int initialState = 0;
    public IntUnityEvent[] notifier;
    
    public override void Attached()
    {
        if (BoltNetwork.IsServer)
            state.StateNumber = initialState;
        
        state.AddCallback("StateNumber", () =>
        {
            notifier[state.StateNumber]?.Invoke(state.StateNumber);
        });
    }
    
    public void SetStateInServer(int pStateNumber)
    {
        state.StateNumber = pStateNumber % notifier.Length;
    }
}