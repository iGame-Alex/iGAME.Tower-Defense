using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    private Waypoint _waypoint;

    private void Awake()
    {
        _waypoint = GetComponent<Waypoint>();
    }

    private void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }
    private void SnapToGrid()
    {
        int gridSize = Waypoint.GetGridSize();
        transform.position = new Vector3(_waypoint.GetGridPos().x*gridSize,0f,_waypoint.GetGridPos().y*gridSize);
    }
    private void UpdateLabel()
    {
        TextMesh lebel = GetComponentInChildren<TextMesh>();
        string labelName = _waypoint.GetGridPos().x  + "," + _waypoint.GetGridPos().y ;
        lebel.text = labelName;
        gameObject.name = labelName;
    }
}
