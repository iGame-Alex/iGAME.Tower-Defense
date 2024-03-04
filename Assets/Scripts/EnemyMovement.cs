using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _enemyRigidbody;
    [SerializeField] private ParticleSystem _castleDamageParticles;
    
    [SerializeField]  [Range(0.5f,1f)]  private float _speedEnemy;
    [SerializeField] [Range(3,5)] private int moveStep = 5;


    private Pathfinder _pathfinder;
    private EnemyDamage _enemyDamage;
    private Castle _castle;
    private Vector3 _targetPosition;

    private void OnValidate()
    {
       _enemyRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _castle = FindObjectOfType<Castle>();
        _enemyDamage = GetComponent<EnemyDamage>();
        _pathfinder = FindObjectOfType<Pathfinder>();
        var path = _pathfinder.GetPath();
        StartCoroutine(EnemyMove(path)); 
    }
    
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position,_targetPosition,Time.deltaTime*moveStep);
    }
    
    private IEnumerator EnemyMove(List<Waypoint> path)
    {
        // ("Player starts to move");
        foreach(Waypoint waypoint in path)
        {
             transform.LookAt(waypoint.transform);
            _targetPosition = waypoint.transform.position;
             yield return new WaitForSeconds(_speedEnemy);
        }
        
        // ("Player finished movement");
        _castle.Damage();
        _enemyDamage.DestroyEnemy(_castleDamageParticles,false);

    }
}
