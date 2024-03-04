using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startPoint, endPoint;
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    
    Dictionary<Vector2Int,Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    
    bool _isRunning = true;
    Waypoint searchPoint;


    private Vector2Int[] _directions = { 
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    public List<Waypoint> GetPath()
    {
        if(path.Count == 0)
        {
            CalculatePath();
        }
        return path;
    }
    
    private void CalculatePath()
    {
        LoadBlocks();
        SetColorStartAndEnd(); // Move out
        PathfindAlgorithm();
        CreatePath();
    }
    private void CreatePath()
    {
        AddPointToPath(endPoint);

        Waypoint prevPoint = endPoint.exploredFrom;
        
        while(prevPoint != startPoint)
        {
            prevPoint.SetTopColor(Color.gray);
            prevPoint = prevPoint.exploredFrom;
            
            AddPointToPath(prevPoint);
        }
        
        AddPointToPath(startPoint);
        path.Reverse();
    }
    
    private void AddPointToPath(Waypoint waypoint)
    {
        path.Add(waypoint);
        
        waypoint.isPlaceable = false;
    }
    
    private void PathfindAlgorithm()
    {
        queue.Enqueue(startPoint);
        
        while(queue.Count > 0 && _isRunning == true)
        {
              searchPoint = queue.Dequeue();
              searchPoint.isExplored = true;
              
              CheckForEndpoint();
              ExploreNearPoints();
        }
    }

    private void CheckForEndpoint()
    {
        if(searchPoint == endPoint)
        {
            _isRunning = false;
        }
    }
    
    private void AddPointToQueue(Waypoint nearPoint)
    {
        if(nearPoint.isExplored || queue.Contains(nearPoint))
        {
            return;
        }
        else
        {
            queue.Enqueue(nearPoint);
            nearPoint.exploredFrom = searchPoint;
        }
    }
    
    private void SetColorStartAndEnd()
    {
        startPoint.SetTopColor(Color.green);
        endPoint.SetTopColor(Color.red);
    }
    
   

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach(Waypoint waypoint in waypoints)
        {
            Vector2Int gridPos = waypoint.GetGridPos();
            if(grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Block replay : " + waypoint);
            }
            else
            {
                grid.Add(gridPos,waypoint);
            }
        }
    }
    
    private void ExploreNearPoints()
    {
        if(!_isRunning) { return; }
        
        foreach(Vector2Int direction in _directions)
        {
            Vector2Int nearPointCoordinates = searchPoint.GetGridPos() + direction;

            if(grid.TryGetValue(nearPointCoordinates, out var nearPoint))
            {
                AddPointToQueue(nearPoint);
            }
        }
    }
}
