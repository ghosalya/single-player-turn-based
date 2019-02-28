using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getPointedCell();
    }

    void getPointedCell()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if( Physics.Raycast(ray, out hit, 100.0f))
        {
            GridPosition gridPos = GetComponent<GridPosition>();
            gridPos.row = (int) ((hit.point.z - GridPosition.rowYOffset) / GridPosition.rowYFactor);
            gridPos.column = (int) ((hit.point.x - GridPosition.columnXOffset) / GridPosition.columnXFactor);
        }
    }
}
