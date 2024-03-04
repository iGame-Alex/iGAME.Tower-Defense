using UnityEngine;
using UnityEngine.UI;
[SelectionBase]
public class EnemyDamage : MonoBehaviour
{
    [SerializeField]  [Range(0,8)] private int _enemyDamage;
    [SerializeField]  [Range(0,10)] private int _hitPoints ;
    
    [SerializeField] private ParticleSystem _hitParticles;
    [SerializeField] private ParticleSystem _deathParticles;
    
    [SerializeField] private AudioClip _hitEnemySoundFX;
    [SerializeField] private AudioClip _deathEnemySoundFX;

   private Text _scoreText;
   private int _currentScore;
   private AudioSource _audioSource;

    private void Start()
    {
        _scoreText = GameObject.Find("Score").GetComponent<Text>();  
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnParticleCollision(GameObject play)
    {
        ProcessHit();
        EnemyHpDamage();
    }
    
    private void ProcessHit()
    {
        _audioSource.PlayOneShot(_hitEnemySoundFX);
        _hitParticles.Play();
    }

   private void EnemyHpDamage()
    {
        _hitPoints -= _enemyDamage;

        if (_hitPoints<=0)
        {
            DestroyEnemy(_deathParticles,true);
        }
    }
   
    #region DestroyEnemy
    public void DestroyEnemy(ParticleSystem fx,bool addScore)
    {
        if(addScore)
        {
            _currentScore = int.Parse(_scoreText.text);
            _currentScore++;
            _scoreText.text = _currentScore.ToString();
        }

        var destroyFX = Instantiate(fx,transform.position,Quaternion.identity);
        destroyFX.Play();
        float destroyFXDuration = destroyFX.main.duration;

        AudioSource.PlayClipAtPoint(_deathEnemySoundFX,Camera.main.transform.position);

        Destroy(destroyFX.gameObject,destroyFXDuration);
        Destroy(gameObject);
    }
    #endregion
}



