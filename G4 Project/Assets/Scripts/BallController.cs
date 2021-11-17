using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    [SerializeField]
    private int thrust;

    private Rigidbody2D rb;

    private int ownerID;

    // Start is called before the first frame update
    void Start()
    {
        ownerID = Random.Range(1, 2);
        rb = GetComponent<Rigidbody2D>();
        float randomDirection = Random.Range(0, 2) * 2 - 1;
        rb.AddForce(new Vector2(randomDirection, 5 * randomDirection) * thrust, ForceMode2D.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetBall()
    {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        float randomDirection = Random.Range(0, 2) * 2 - 1;
        rb.AddForce(new Vector2(3 * randomDirection, 15 * randomDirection) * thrust, ForceMode2D.Force);
    }

    // This ResetBall function will place the ball in front of the player whose paddle it passed.
    // Maybe we can remove the ResetBall() method later, not sure.
    // -Connor
    public void ResetBall(int ownerID)
    {
        rb.velocity = Vector2.zero;

        // Randomly generate the angle that the ball will move after respawning.
        float randomAngle;
        if (ownerID == 1)
        {
            randomAngle = Random.Range(-1.0f/4.0f * Mathf.PI, 1.0f/4.0f * Mathf.PI);
            transform.position = new Vector2(-6, 0);
        }
        else
        {
            randomAngle = Random.Range(3.0f/4.0f * Mathf.PI , 5.0f/4.0f * Mathf.PI);
            transform.position = new Vector2(6, 0);
        }

        rb.AddForce(new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * thrust, ForceMode2D.Force);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<PaddleController>() != null)
        {
            PaddleController paddle = col.gameObject.GetComponent<PaddleController>();
            ownerID = paddle.getOwnerID();
        }
    }
}
