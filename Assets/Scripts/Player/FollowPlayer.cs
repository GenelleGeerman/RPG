using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;

    private void Start()
    {
         
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        catch (System.Exception)
        {
            Debug.Log("No Player");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + new Vector3(0, 0, -10f);
        }
    }
}
