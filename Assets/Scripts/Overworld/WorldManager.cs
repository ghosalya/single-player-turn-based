using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPosition;
    Vector3 playerProceedingTarget;
    [SerializeField]
    GameObject tileSelector;
    [SerializeField]
    GameObject WorldTilePrefab;

    SessionData sessionData;

    public int worldHeight = 5, worldWidth = 5;

    WorldTile[,] generatedWorld;
    // Start is called before the first frame update
    void Start()
    {
        SessionData savedData = SessionData.Load();

        if ( savedData == null ) {
            generateWorld();
            saveWorld();
        } else {
            loadWorld(savedData);
        }
    }

    void generateWorld() {
        generatedWorld = new WorldTile[worldWidth, worldHeight];
        
        generateTile(0, 0);
    }

    void generateTile(int x, int y) {
        // Debug.Log("[WorldGeneration] Generating at " + x.ToString() + ", " + y.ToString());
        if (x >= worldWidth) return;
        if (y >= worldHeight) return;

        if (generatedWorld[x, y] == null) {
            generatedWorld[x, y] = instantiateTile(x, y);

            if (Random.Range(0f, 1f) < 0.6) {
                generateTile(x+1, y);
            }

            if (Random.Range(0f, 1f) < 0.6) {
                generateTile(x, y+1);
            }
        }
    }

    WorldTile instantiateTile(int x, int y) {
        GameObject tile = Instantiate(WorldTilePrefab, new Vector3(x, 0, y), Quaternion.Euler(0, 0, 0));

        float r = Random.Range(0f, 1f);
        if (r < 0.7f) {
            tile.GetComponent<WorldTile>().tileRoomType = RoomType.BATTLE;
        } else if (r < 0.85f) {
            tile.GetComponent<WorldTile>().tileRoomType = RoomType.REST;
        } else {
            tile.GetComponent<WorldTile>().tileRoomType = RoomType.SHOP;
        }

        tile.transform.position = new Vector3(x*2, 0, y*2);
        if (x == 0 && y == 0) {
            tile.GetComponent<WorldTile>().markVisited();
        }
        return tile.GetComponent<WorldTile>();
    }

    void saveWorld() {
        if (sessionData == null) sessionData = new SessionData();
        // sessionData.worldData.randomSeed = Random.state;
        sessionData.worldData.playerX = getPlayerX();
        sessionData.worldData.playerY = getPlayerY();

        sessionData.worldData.generatedWorld = new int[worldWidth, worldHeight];
        sessionData.worldData.visited = new bool[worldWidth, worldHeight];
        for (int i = 0; i < worldWidth; i++) {
            for (int j = 0; j < worldHeight; j++) {
                WorldTile tile = generatedWorld[i, j];
                if (tile != null) {
                    sessionData.worldData.generatedWorld[i, j] = (int)tile.tileRoomType;
                    sessionData.worldData.visited[i, j] = tile.visited;
                } else {
                    sessionData.worldData.generatedWorld[i, j] = -1; // -1 means room does not exists
                }
            }
        }
        SessionData.Save(sessionData);
    }

    void loadWorld(SessionData data) {
        sessionData = data;
        playerProceedingTarget = new Vector3(
            data.worldData.playerX * 2,
            0,
            data.worldData.playerY * 2
        );
        playerPosition.transform.position = playerProceedingTarget;

        generatedWorld = new WorldTile[worldWidth, worldHeight];
        for (int i = 0; i < worldWidth; i++) {
            for (int j = 0; j < worldHeight; j++) {

                if (sessionData.worldData.generatedWorld[i, j] >= 0) {
                    GameObject tileObject = Instantiate(WorldTilePrefab, new Vector3(i*2, 0, j*2), Quaternion.Euler(0, 0, 0));
                    WorldTile tile = tileObject.GetComponent<WorldTile>();

                    tile.tileRoomType = (RoomType)sessionData.worldData.generatedWorld[i, j];
                    tile.visited = sessionData.worldData.visited[i, j];

                    generatedWorld[i, j] = tile;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        checkSelection();
        playerMove();

        if (Input.GetMouseButtonDown(0)) {
            proceedToSelected();
        }
        
    }

    int getPlayerX() { return (int)playerPosition.transform.position.x / 2; }
    int getPlayerY() { return (int)playerPosition.transform.position.z / 2; }
    Vector2 getPlayerCoor() { return new Vector2(getPlayerX(), getPlayerY()); }

    void checkSelection() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if( Physics.Raycast(ray, out hit, 200.0f))
        {
            WorldTile pointedTile = hit.transform.GetComponent<WorldTile>();
            if (pointedTile != null) {
                float coordif = Mathf.Abs((pointedTile.getCoor() - getPlayerCoor()).magnitude);
                if ( coordif <= 1 ) {
                    tileSelector.transform.position = new Vector3(
                        pointedTile.transform.position.x, 
                        0, 
                        pointedTile.transform.position.z
                    );
                }
            }
        }
    }

    void proceedToSelected() {
        playerProceedingTarget = tileSelector.transform.position;
    }

    void playerMove() {
        if (playerProceedingTarget != playerPosition.transform.position) {
            playerPosition.transform.position = Vector3.MoveTowards(
                playerPosition.transform.position,
                playerProceedingTarget,
                10 * Time.deltaTime
            );
        } else {
            WorldTile currentTile = generatedWorld[getPlayerX(), getPlayerY()];
            if (!currentTile.visited) {
                currentTile.markVisited();
                saveWorld();
                string[] roomNames = new string[]{"Battle", "Rest", "Shop"};
                Debug.Log("Transition to " + roomNames[(int)currentTile.tileRoomType]);

                if(currentTile.tileRoomType == RoomType.BATTLE) {
                    SceneManager.LoadScene("BattleSandbox");
                }
            }
        }
    }
}
