using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoomType { 
    BATTLE = 0, 
    REST = 1, 
    SHOP = 2
}

public class WorldTile : MonoBehaviour
{
    public bool visited, hasUp, hasDown, hasLeft, hasRight;
    public RoomType tileRoomType;

    [SerializeField]
    GameObject upConnector, downConnector, leftConnector, rightConnector;

    [SerializeField]
    Material unvisitedMat, visitedMat;


    // Start is called before the first frame update
    void Start()
    {
        renderVisitStatus();
        displayConnectors();
    }

    public void displayConnectors() {
        upConnector.SetActive(hasUp);
        downConnector.SetActive(hasDown);
        leftConnector.SetActive(hasLeft);
        rightConnector.SetActive(hasRight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void markVisited() {
        visited = true;
        renderVisitStatus();
    }

    void renderVisitStatus() {
        if (visited) {
            GetComponent<Renderer>().material = visitedMat;
        } else {
            GetComponent<Renderer>().material = unvisitedMat;
        }
    }

    public Vector2 getCoor() {
        return new Vector2(
            (int)transform.position.x / 2,
            (int)transform.position.z / 2
        );
    }


}
