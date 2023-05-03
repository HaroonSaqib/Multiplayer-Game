using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Android;

public class ScriptforReconnection : MonoBehaviour
{
    private string dataFilePath;

    private float playerPosX;
    private float playerPosY;

    public void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Time.timeScale = 0; // Pause the game
            SaveData();
        }
        else
        {
            Time.timeScale = 1; // Resume the game
         //   ReconnectToPhotonCloud();
            LoadData();
        }
    }


    void SaveData()
    {
        // Get the player's position
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        playerPosX = playerPos.x;
        playerPosY = playerPos.y;

        // Convert the player's position to JSON
        string jsonData = JsonUtility.ToJson(new PlayerData1(playerPosX, playerPosY));

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
            PlayerData1 playerData = JsonUtility.FromJson<PlayerData1>(jsonData);

            // Set the player's position
            GameObject.Find("Player").transform.position = new Vector3(playerData.posX, playerData.posY, 0);
        }
        else
        {
            Debug.Log("Data file not found!");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        dataFilePath = Application.persistentDataPath + "/GameData.json";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



public class PlayerData1
{
    public float posX;
    public float posY;

    public PlayerData1(float posX, float posY)
    {
        this.posX = posX;
        this.posY = posY;
    }
}