using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set;}
     
    public Transform player;
    
    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
