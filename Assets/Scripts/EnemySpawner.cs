using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
     private float _spawnInterval = 1.65f;
    [SerializeField] private  EnemyMovement _enemyPrefab;    
    [SerializeField] private AudioClip _enemySpawnSoundFX;

    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }
    
   private IEnumerator EnemySpawn()
    {
        while(true) 
        {
            GetComponent<AudioSource>().PlayOneShot(_enemySpawnSoundFX);
            var newEnemy = Instantiate(_enemyPrefab,transform.position,Quaternion.identity);
            newEnemy.transform.parent = transform;
            yield return new WaitForSeconds(_spawnInterval);      
        }     
    }
}
