using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{
    private Stack<string> powerNames = new();

    public void AddPowerUp(MonoBehaviour power)
    {
        if ( power == null) return;
        powerNames.Push(power.name);
        gameObject.AddComponent(power.GetType());
    }
}
