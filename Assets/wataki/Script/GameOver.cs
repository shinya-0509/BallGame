using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameDirector.IsDead = true;
    }
}
