using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition : MonoBehaviour
{
    public static float columnXOffset = 1;
    public static float columnXFactor = 0.25f;
    public static float rowYOffset = 1;
    public static float rowYFactor = 0.25f;

    public float moveSpeed = 100f;
    private int _column;  // Leftmost column is column 0
    public int column
    {
        get { return _column; }
        set { _column = Mathf.Clamp(value, 0, 3); }
    }
    private int _row;  // Nearest row is row 0
    public int row
    {
        get { return _row; }
        set { _row = Mathf.Clamp(value, 0, 4); }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveToPosition();
    }

    void moveToPosition()
    {
        // Animation
        transform.Translate(
            Vector3.MoveTowards(
                transform.position,
                new Vector3(
                    columnXOffset + columnXFactor * column,
                    0,
                    rowYOffset + rowYFactor * row
                ),
                moveSpeed
            )
        );
    }

    
}
