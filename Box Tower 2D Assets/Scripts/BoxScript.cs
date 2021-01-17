using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private float min_X = -5f, max_X = 5f;

    private bool canMove;
    public float move_Speed = 4f;

    private Rigidbody2D myBody;

    private bool gameOver;
    private bool ignoreCollision;
    private bool ignoreTrigger;

    public AudioSource tickSource;
    public void Initalize(float speed)
    {
        move_Speed = speed;

    }




     void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.gravityScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        tickSource = GetComponent<AudioSource>();
        canMove = true;

        if(Random.Range(0,2) > 0)
        {
            move_Speed *= -1f;
        }

        GameplayController.instance.currentBox = this;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBox();
    }

    void MoveBox()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;

            temp.x += move_Speed * Time.deltaTime;

            if(temp.x > max_X)
            {
                move_Speed *= -1f;
            } else if(temp.x < min_X)
            {
                move_Speed *= -1f;
            }

            transform.position = temp;
        }
    }

    public void DropBox()
    {
        canMove = false;
        myBody.gravityScale = Random.Range(2, 4);
    }


    void Landed()
    {
        if (gameOver)
        {
            return;
        }

        ignoreCollision = true;
        ignoreTrigger = true;

        GameplayController.instance.SpawnNewBox();
        GameplayController.instance.MoveCamera();
    }

    void RestartGame()
    {
        GameplayController.instance.RestartGame();
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;

        if (target.gameObject.tag == "Platform")
        {
            Invoke("Landed", 2f);
            ignoreCollision = true;
            //platSource.Play(); //here
        }

        if (target.gameObject.tag == "Box")
        {
            Invoke("Landed", 2f);
            ignoreCollision = true;
            tickSource.Play(); //here
            ScoreScript.scoreValue += 1;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        
        if (ignoreTrigger)
            return;
        
        if (target.tag == "GameOver")
        {
            
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;
            RestartGame();
        }
    }

}
