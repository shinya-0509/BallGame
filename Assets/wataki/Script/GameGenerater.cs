using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGenerater : MonoBehaviour
{
    [SerializeField] private GameObject[] ballPrefab;
    [SerializeField] private GameDirector gameDirector;
    [SerializeField] private Transform ballsObj;

    private GameObject reservedBall;
    private void Start()
    {
        Pop();
    }

    public GameObject Pop()
    {

         int index = Random.Range(0, ballPrefab.Length);
        GameObject go = Instantiate(ballPrefab[index]);
        go.transform.SetParent(ballsObj);        
        BallController ballController = go.GetComponent<BallController>();
        ballController.SetGameDirector(gameDirector);
        ballController.BallsObj = ballsObj;
        ballController.enabled = false;
        go.GetComponent<Rigidbody2D>().isKinematic = true;
        go.GetComponent<CircleCollider2D>().enabled = false;
        go.GetComponent<SpriteRenderer>().enabled = false;
        
        return go;
    }
}
