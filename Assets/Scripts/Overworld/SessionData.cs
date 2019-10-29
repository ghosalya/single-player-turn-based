using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.Runtime.Serialization.Formatters.Binary;
 using System.IO;

[System.Serializable]
public class SessionData
{
    public SessionPlayerData playerData;
    public SessionWorldData worldData;

    public SessionData() {
        this.playerData = new SessionPlayerData();
        this.worldData = new SessionWorldData();
    }

    public static void Save(SessionData data, string saveName) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(getSavePath(saveName));
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game Saved as " + saveName + ".dat");
    }

    public static void Save(SessionData data) { Save(data, "quicksave"); }

    public static SessionData Load(string saveName) {
        string savePath = getSavePath(saveName);

        if (!File.Exists(savePath)) {
            Debug.Log(savePath + " - Save game doesn't exists.");
            return null;
        }

        // if file exists, load and return
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(savePath, FileMode.Open);
        SessionData data = (SessionData)bf.Deserialize(file);
        file.Close();
        
        Debug.Log("Game loaded from " + savePath);

        return data;
    }

    public static SessionData Load() { return Load("quicksave"); }

    private static string getSavePath(string saveName) {
        return Application.persistentDataPath + "/" + saveName + ".dat";
    }
    
}

[System.Serializable]
public class SessionWorldData
{
    public Random.State randomSeed;
    public int[,] generatedWorld;
    public bool[,] visited;

    public int playerX, playerY;

    public SessionWorldData() {
        this.playerX = 0;
        this.playerY = 0;
        // this.randomSeed = Random.state;
    }
}

[System.Serializable]
public class SessionPlayerData
{
    public int currentHealth, maxHealth, maxEnergy;
    public CardSerial[] deck;

    public SessionPlayerData() {
        this.maxEnergy = 150;
        this.maxHealth = 500;
        this.currentHealth = this.maxHealth;

        this.deck = new CardSerial[]{
            new CardSerial("Strike"),
            new CardSerial("Strike"),
            new CardSerial("Strike"),
            new CardSerial("Strike"),
            new CardSerial("Block"),
            new CardSerial("Block"),
            new CardSerial("Block"),
            new CardSerial("Block"),
            new CardSerial("Hyperblast"),
        };
    }
}

[System.Serializable]
public class CardSerial
{
    public string cardName, description;  // mapped from cardName
    public int baseCost;
    public bool needsTarget;

    public CardSerial(string cardName) {
        this.cardName = cardName;
    }
}
