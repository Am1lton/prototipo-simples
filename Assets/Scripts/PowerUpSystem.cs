using System.Collections.Generic;
using UnityEngine;
using Classes;

public class PowerUpSystem : MonoBehaviour
{
    private Stack<Power> powers = new();

    public void AddPowerUp(Power power)
    {
        if ( power == Power.Empty) return;
        powers.Push(power);
    }
}
