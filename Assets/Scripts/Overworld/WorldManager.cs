using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    GameObject playerPosition;
    Vector3 playerProceedingTarget;
    [SerializeField]
    GameObject tileSelector;
    [SerializeField]
    GameObject WorldTilePrefab;

    public int worldHeight = 5, worldWidth = 5;

    WorldTile[,] generatedWorld;
    // Start is called before the first frame update
    void Start()
    {
        generateWorld();
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
                string[] roomNames = new string[]{"Battle", "Rest", "Shop"};
                Debug.Log("Transition to " + roomNames[(int)currentTile.tileRoomType]);
            }

        }
    }
}
