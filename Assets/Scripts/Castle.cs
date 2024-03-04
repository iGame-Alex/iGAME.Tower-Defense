using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Castle : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private int _castleLife;
    [SerializeField] [Range(0, 5)] private int _castleDamage;
    [HideInInspector] public Text _textLife;
    
    private void Awake()
    {
        _textLife.text = _castleLife.ToString();
    }
    
    private void Update()
    {
        if (_castleLife <= 0)
        {
            SceneManager.LoadScene("Scenes/Game");
        }
    }
    
    public void Damage()
    {
        _castleLife -= _castleDamage;
        _textLife.text = _castleLife.ToString();
    }
    
    
}
