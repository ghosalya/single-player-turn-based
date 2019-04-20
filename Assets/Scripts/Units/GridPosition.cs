using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition : MonoBehaviour
{
    public static float columnXOffset = -14.5f;
    public static float columnXFactor = 9.75f;
    public static float rowYOffset = -21f;
    public static float rowYFactor = 7.85f;

    public float moveSpeed = 100f;
    public int column = 0;  // Leftmost column is column 0
    public int row = 0;  // Nearest row is row 0

    public bool isUpdatingPosition = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        correctCoordinate();
        if(isUpdatingPosition) {
            moveToPosition();
        }
    }

    void correctCoordinate()
    {
        column = Mathf.Clamp(column, 0, 3);
        row = Mathf.Clamp(row, 0, 6);
    }

    void moveToPosition()
    {
        // Animation
        Vector3 targetPosition = new Vector3(columnXOffset + (columnXFactor * column),
                                            0,
                                            rowYOffset + (rowYFactor * row));
        transform.position = Vector3.MoveTowards(
                                transform.position,
                                targetPosition,
                                moveSpeed * Time.deltaTime
                            );
        if(Vector3.Distance(targetPosition, transform.position) < 0.1) {
            transform.position = targetPosition;
            isUpdatingPosition = false;
        }
    }

    public void UpdateUI() {
        isUpdatingPosition = true;
    }

    
}
