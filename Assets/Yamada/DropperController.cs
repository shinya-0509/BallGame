using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropperController : MonoBehaviour
{
    [SerializeField] GameDirector gameDirector;
    [SerializeField] float speed;
    [SerializeField] bool isHaving=false;
    GameObject haveBall = null;
    Rigidbody2D rigid2D;
    Rigidbody2D rigid;
    Vector2 position;
    private float delta;

    void Start()
    {
        this.position = transform.position;
        rigid2D = this.gameObject.GetComponent<Rigidbody2D>();
        this.delta = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        this.delta += Time.deltaTime;
        if (!this.isHaving)
        {
            this.haveBall = this.gameDirector.Queue.Dequeue();
            this.isHaving = true;
        }
        else
        {
            this.haveBall.transform.position = this.position;
            this.haveBall.transform.position = new Vector3(this.position.x, this.position.y, -1);
            this.haveBall.GetComponent<SpriteRenderer>().enabled = true;

        }
        
        if (Input.GetKey(KeyCode.A))
        {
            if(this.position.x < 4.0f && this.position.x > -4.0f)
            {
                this.position.x -= Time.deltaTime * speed;
                this.transform.position = new Vector2(this.position.x, this.position.y);
            }
            else if(this.position.x >= 4.0f)
            {
                this.position.x = 3.95f;
            }
            else if(this.position.x <= -4.0f)
            {
                this.position.x = -3.95f;
            }
 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (this.position.x < 4.0f && this.position.x > -4.0f)
            {
                this.position.x += Time.deltaTime * speed;
                this.transform.position = new Vector2(this.position.x, this.position.y);
            }
            else if (this.position.x >= 4.0f)
            {
                this.position.x = 3.95f;
            }
            else if (this.position.x <= -4.0f)
            {
                this.position.x = -3.95f;
            }

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(this.delta > 0.8f)
            {
                this.haveBall.GetComponent<BallController>().enabled = true;
                this.haveBall.GetComponent<Rigidbody2D>().isKinematic = false;
                this.haveBall.GetComponent<CircleCollider2D>().enabled = true;
                this.isHaving = false;
                this.delta = 0;

            }

        }
    }
}
