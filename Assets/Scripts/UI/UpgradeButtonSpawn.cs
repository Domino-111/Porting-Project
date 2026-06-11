using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeButtonSpawn : Spawner
{
    override protected void Spawn()
    {
        //Spawn an upgrade button at our current position
        Instantiate(prefabToSpawn, transform.position, Quaternion.identity, transform.parent);
    }
}
