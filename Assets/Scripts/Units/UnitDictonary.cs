using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Units
{
    [Serializable]
    public class UnitDictonary
    {
        public UnitTypePrefabsEnum type;
        public GameObject prefab;
    }
}
