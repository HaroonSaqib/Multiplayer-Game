using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Reconnection : MonoBehaviour
{
    void OnEnable()
    {
        // Enable background sending of keep alive packets
        PhotonNetwork.KeepAliveInBackground = 1000f; // send keep alive packets every 10 seconds
    }

    void OnDisable()
    {
        // Disable background sending of keep alive packets
        PhotonNetwork.KeepAliveInBackground = 0f;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
