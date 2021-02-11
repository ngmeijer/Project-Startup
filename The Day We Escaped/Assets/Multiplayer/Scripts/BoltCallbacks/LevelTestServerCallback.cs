using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;
using UnityEngine.UI;

[BoltGlobalBehaviour(BoltNetworkModes.Server, "Level Test")]
public class LevelTestServerCallback : Bolt.GlobalEventListener
{
    public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token)
    {
        
    }

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        var serverPlayerToken = token as IntBoltToken;
        int playerType = 1;
        if (serverPlayerToken == null)
        {
            BoltLog.Warn("Null SceneLoadLocalDone token received");
        }
        else
        {
            playerType = serverPlayerToken.intVal;
        }

        var spawnPosition = Vector3.zero;
        GameObject spawnPoint;
        
        BoltLog.Warn($"{this} Server | playerType: " + playerType);
        
        if (playerType == 1)
        {
            //Instantiate player at Cell
            spawnPoint = GameObject.Find("SpawnPoint Cell");
        }
        else
        {
            //Instantiate player at Morgue
            spawnPoint = GameObject.Find("SpawnPoint Morgue");
        }

        spawnPosition = (spawnPoint != null) ? spawnPoint.transform.position : spawnPosition;
        
        //TODO: factory pattern
        var serverPlayer = BoltNetwork.Instantiate(BoltPrefabs.PlayerTDWERigid, spawnPosition, Quaternion.identity);
    }
}
