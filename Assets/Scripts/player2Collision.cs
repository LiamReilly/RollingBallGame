using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player2Collision : MonoBehaviour
{
    public GameObject player2;
    


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player2.gameObject.transform.position + new Vector3(0, 0.55f, 0);
    }
}
