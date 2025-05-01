using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour
{
    private Stack<string> powerNames = new();

    public void AddPowerUp(MonoScript power)
    {
        if ( power == null) return;
        powerNames.Push(power.name);
        gameObject.AddComponent(power.GetClass());
    }

    public void RemoveMostRecentPowerUp()
    {
        var component = gameObject.GetComponent(powerNames.Pop());
        if (component == null) return;
        Destroy(component);
    }
}
