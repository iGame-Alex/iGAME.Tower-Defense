using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private int _towerLimit;
    [SerializeField] internal Tower _towerPrefab;

    private Queue<Tower> _towerQueue = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {                           
        int towersCount = _towerQueue.Count;
        
        Debug.Log(towersCount +1);
        
        if(towersCount < _towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveTowerToNewPosition(baseWaypoint);
        }
    }
    
    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(_towerPrefab,baseWaypoint.transform.position,Quaternion.identity);
        newTower.transform.parent = transform;

        baseWaypoint.isPlaceable = false;
        newTower.baseWaypoint = baseWaypoint;
        
        _towerQueue.Enqueue(newTower);
    }

    private void MoveTowerToNewPosition(Waypoint newBaseWaypoint)
    {
        Debug.Log("Move the tower to a new position");
        Tower oldTower = _towerQueue.Dequeue();
        
        oldTower.transform.position = newBaseWaypoint.transform.position;
        oldTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;
        oldTower.baseWaypoint = newBaseWaypoint;

        _towerQueue.Enqueue(oldTower);
    }

}
