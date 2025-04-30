using System;
using System.Collections.Generic;
using UnityEngine;
using Classes;

namespace ScriptableObjects.Scripts
{
    [CreateAssetMenu(fileName = "Powers", menuName = "Powers")]
    public class Powers_SO : ScriptableObject
    {
        [SerializeField] private Power[] powers;
    }
}