using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Enums;
using Assets.Scripts.Units;
using System.Linq;

public class UnitFactory : MonoBehaviour
{
    public UnitDictonary[] UnitPrefabs;

    public UnitTypePrefabsEnum devType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            this.Spawn(this.devType, Vector3.zero);
        }
    }

    void Spawn(UnitTypePrefabsEnum type, Vector3 position)
    {
        var unit = this.UnitPrefabs.FirstOrDefault(up => up.type == type);

        if (unit != null)
        {
            GameObject.Instantiate(unit.prefab).transform.position = position;
        }
        else
        {
            Debug.LogWarning($"[UnityFactory] PREFAB {type} NOT FOUND");
        }
    }
}
