using UnityEngine;

[SelectionBase]
public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _towerTop;
    [SerializeField] private Transform _targetEnemy;
    [SerializeField] private ParticleSystem _bulletParticle;

   [HideInInspector] public Waypoint baseWaypoint;

   private void Update()
    {
        SetTargetEnemy();
        if(_targetEnemy){ 
            
            _towerTop.LookAt(_targetEnemy);
        }
    }

    #region SetTargetEnemy

    private void SetTargetEnemy()
    {
    
        var sceneEnemies = FindObjectsOfType<EnemyDamage>(); // Get all enemies
    
        if(sceneEnemies.Length == 0) {return;}
        Transform closestEnemy = sceneEnemies[0].transform;  // First enemy
        foreach(EnemyDamage test in sceneEnemies) // Compare all enemies and point first
        {
            closestEnemy = GetClosestEnemy(closestEnemy.transform,test.transform);
        } 
        _targetEnemy = closestEnemy;
    }

    #endregion

    private Transform GetClosestEnemy(Transform enemyA, Transform enemyB)
    {
        var distToA = Vector3.Distance(enemyA.position,transform.position);
        var distToB = Vector3.Distance(enemyB.position,transform.position);
        
        if(distToA < distToB)
        {
            return enemyA;
        }
        return enemyB;
    }
    
}
