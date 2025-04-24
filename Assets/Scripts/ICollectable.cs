using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface ICollectable
{
    public void OnCollect(Transform collector);
    
    private void OnTriggerEnter(Collider other)
    {
        OnCollect(other.transform);
    }
}
