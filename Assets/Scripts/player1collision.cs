using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1collision : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = player.gameObject.transform.position + new Vector3 (0, 0.55f, 0);
    }
}
