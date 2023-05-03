using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Realtime;
using System.IO;
using UnityEngine.Android;
public class ConnectToServer : MonoBehaviourPunCallbacks
{

    private string dataFilePath;

    private float playerPosX;
    private float playerPosY;

    private GameObject[] objectsToPreserve;
    private bool isDisconnected = false;
    bool connected = false;

    public void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
             Time.timeScale = 0; // Pause the game
            //SaveData();
           // PhotonNetwork.Disconnect();
        }
        else
        {
            //Time.timeScale = 1; // Resume the game
            //ReconnectToPhotonCloud();
            PhotonNetwork.ConnectUsingSettings();
            //LoadData();
        }
    }





    void SaveData()
    {
        // Get the player's position
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        playerPosX = playerPos.x;
        playerPosY = playerPos.y;

        // Convert the player's position to JSON
        string jsonData = JsonUtility.ToJson(new PlayerData(playerPosX, playerPosY));

        // Write the JSON data to a file
        File.WriteAllText(dataFilePath, jsonData);
    }

    void LoadData()
    {
        // Check if the data file exists
        if (File.Exists(dataFilePath))
        {
            // Read the JSON data from the file
            string jsonData = File.ReadAllText(dataFilePath);

            // Convert the JSON data to a PlayerData object
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(jsonData);

            // Set the player's position
            GameObject.Find("Player").transform.position = new Vector3(playerData.posX, playerData.posY, 0);
        }
        else
        {
            Debug.Log("Data file not found!");
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        PhotonNetwork.ConnectUsingSettings();
        objectsToPreserve = GameObject.FindGameObjectsWithTag("Preserve");
        dataFilePath = Application.persistentDataPath + "/GameData.json";

    }

    private void Update()
    {
        //if (!PhotonNetwork.IsConnected)
        //{
        //    ReconnectToPhotonCloud();
        //}
        //if(isDisconnected && !PhotonNetwork.IsConnected )
        //{
        //    PhotonNetwork.Reconnect();
        //}
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        connected = true;
    }


    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Multiplayer");
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        
        //GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Destroy");
        //foreach (GameObject obj in objectsToDestroy)
        //{
        //    Destroy(obj);
        //}

        //foreach (GameObject obj1 in objectsToPreserve)
        //{
        //    PhotonNetwork.AllocateViewID(obj1.GetComponent<PhotonView>());
        //    DontDestroyOnLoad(obj1);
        //}
        ////PhotonNetwork.Disconnect();
        //connected = false;
        // Attempt to reconnect to the server
        //PhotonNetwork.Reconnect();
        //PhotonNetwork.LoadLevel("NextScene");
        // ReconnectToPhotonCloud();
    }


    public void ReconnectToPhotonCloud()
    {
        PhotonNetwork.Reconnect();
    }

    //void RequestStoragePermission()
    //{
    //    if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
    //    {
    //        // Permission is already granted, do nothing
    //    }
    //    else
    //    {
    //        // Request permission
    //        Permission.RequestUserPermission(Permission.ExternalStorageWrite);
    //    }
    //}

}


public class PlayerData
{
    public float posX;
    public float posY;

    public PlayerData(float posX, float posY)
    {
        this.posX = posX;
        this.posY = posY;
    }
}