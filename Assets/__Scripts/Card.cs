using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Decorator {
    public string type; 
    public Vector3 loc; //location of sprite on the card
    public bool flip = false; 
    public float scale = 1f;
}

[System.Serializable]
public class CardDefinition {
    public string face; 
    public int rank;
    public List<Decorator> pip = new List<Decorator>();
}