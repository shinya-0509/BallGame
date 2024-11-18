using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BALL_TYPE
{
    GolfBall = 1,
    TableTennis,
    TennisBall,
    BaseBall,
    VolleyBall,
    SoccerBall,
    Boling,
    BasketBall,
    RugbyBall,
    BalanceBall,
    BigBall,
}
public class BallController : MonoBehaviour
{
    public BALL_TYPE ballType;
    private static int ball_serial = 0;
    private int mySerial;
    public bool isDestroyed;

    //public static UnityEvent OnGameOver = new UnityEvent();
    //private bool isInside = false;
    

    public AudioClip se;
    AudioSource aud;

    [SerializeField] private BallController nextBall;
    [SerializeField] private int score;
    private GameDirector gameDirector;
    private Transform ballsObj;

    public static UnityEvent<int> OnScoreAdded = new UnityEvent<int>();

    public int Score { get => score; set => score = value; }
    public AudioClip Se { get=> se; set => se = value; }
    public Transform BallsObj { get => ballsObj; set => ballsObj = value; }

    public void SetGameDirector(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;
    }

    // Start is called before the first frame update
    IEnumerable Start()
    {
        Application.targetFrameRate = 60;
        //this.aud = GetComponent<AudioSource>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        while (rb.isKinematic)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f);
    }

    private void Update()
    {
        if ((int)ballType > 4)
        {
            Debug.Log("balltype"+(int)ballType);
            SetGameDirector(gameDirector);
        }
    }

    private void Awake()
    {
        mySerial = ball_serial;
        ball_serial++;

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (isDestroyed) return;

        if (other.gameObject.TryGetComponent(out BallController otherBall))
        {
            //É{Å[ÉãÇ™ìØÇ∂Ç∆Ç´è¡Ç¶ÇÈèàóù
            if (otherBall.ballType == ballType)
            {

                if (mySerial < otherBall.mySerial)
                {
                    isDestroyed = true;
                    otherBall.isDestroyed = true;
                    Destroy(gameObject);
                    Destroy(other.gameObject);

                    if (nextBall == null) return;

                    //ãììÆÇÃèCê≥
                    Vector3 center = (transform.position + other.transform.position) / 2;
                    Quaternion rotation = Quaternion.Lerp(transform.rotation, other.transform.rotation, 0.5f);
                    BallController next = Instantiate(nextBall, center, rotation);
                    next.transform.SetParent(BallsObj);
                    next.SetGameDirector(gameDirector);

                    Rigidbody2D nextRb = next.GetComponent<Rigidbody2D>();
                    Vector3 velocity = (GetComponent<Rigidbody2D>().velocity + other.gameObject.GetComponent<Rigidbody2D>().velocity) / 2;
                    nextRb.velocity = velocity;

                    float angularVelocity = (GetComponent<Rigidbody2D>().angularVelocity + other.gameObject.GetComponent<Rigidbody2D>().angularVelocity) / 2;
                    nextRb.angularVelocity = angularVelocity;

                    gameDirector.AddPoint(next.Score);

                }
            }
        }
    }

    

    public void CollisionOff()
    {
        CircleCollider2D cc = GetComponent<CircleCollider2D>();
        cc.enabled = false;
    }

}
