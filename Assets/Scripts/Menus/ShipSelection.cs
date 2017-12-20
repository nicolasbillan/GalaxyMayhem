using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Constants;

public class ShipSelection : MonoBehaviour {
    
    public void ShipSelected(int ship)
    {
        GameObject.Find(GameObjectNames.ScenesLoader).GetComponent<ScenesLoader>().Load(SceneNames.LevelSelection, ParametersKeys.PlayerShip, ship.ToString());
    }
}
