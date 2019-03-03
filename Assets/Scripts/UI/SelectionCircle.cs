using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCircle : MonoBehaviour
{
    public Vector3 hitcoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getPointedCell();
        if(Input.GetMouseButtonDown(0)) {
            selectPosition();
        }
    }

    void getPointedCell()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if( Physics.Raycast(ray, out hit, 100.0f))
        {
            hitcoor = hit.point;
            GridPosition gridPos = GetComponent<GridPosition>();
            gridPos.row = Mathf.RoundToInt((hit.point.z - GridPosition.rowYOffset) / GridPosition.rowYFactor);
            gridPos.column = Mathf.RoundToInt((hit.point.x - GridPosition.columnXOffset) / GridPosition.columnXFactor);
        }
    }

    void selectPosition() {
        PlayerController pcon = GameObject.FindGameObjectWithTag("Battle").GetComponent<PlayerController>();
        GridPosition gridPos = GetComponent<GridPosition>();
        pcon.cellSelected = new int[2] {gridPos.column, gridPos.row};
        gameObject.SetActive(false);
    }
}
