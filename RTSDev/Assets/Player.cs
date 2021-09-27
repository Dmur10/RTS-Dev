using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;
    public List<Transform> selectedUnits;
    public List<Transform> units;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    } 
    // Update is called once per frame
    void Update()
    {
        
    }
}
