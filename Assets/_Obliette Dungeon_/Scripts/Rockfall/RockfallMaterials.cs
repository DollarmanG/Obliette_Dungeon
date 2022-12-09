using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rockfall
{
    public class RockfallMaterials : MonoBehaviour
    {
        [SerializeField]
        public Material[] rockfallStateMaterials;

        public Material setStateMaterial(int materialIndex)
        {
            return rockfallStateMaterials[materialIndex];
        }
    }
}

    

