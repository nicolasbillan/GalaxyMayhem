using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;

public class ShipSelection : MonoBehaviour {
    
    public void ShipSelected(int ship)
    {
        GameObject.Find("ScenesLoader").GetComponent<ScenesLoader>().Load("Level1", "PlayerShip", ship.ToString());
    }
}
