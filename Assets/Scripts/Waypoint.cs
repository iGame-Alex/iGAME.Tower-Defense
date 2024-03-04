using UnityEngine;

[SelectionBase]
public class Waypoint : MonoBehaviour
{
   [HideInInspector] internal Waypoint exploredFrom;
   
    public bool isExplored = false;
    public bool isPlaceable = true;

    private Vector2Int _gridPos;
    private const int GridSize = 10;
    
    public static int GetGridSize()
    {
        return GridSize;
    }
    
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / GridSize),
            Mathf.RoundToInt(transform.position.z / GridSize)
            );
    }

    public void SetTopColor(Color color)
    {
       MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();
       topMeshRenderer.material.color = color;
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(isPlaceable)
            {
               FindObjectOfType<TowerFactory>().AddTower(this);
            }
           
        }
    }
}
