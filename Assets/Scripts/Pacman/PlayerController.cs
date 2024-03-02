using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    // private Animator animator;
    public Rigidbody2D rb;
    private SpriteRenderer _spriteRender;
    private int dotCount = 0;
    public bool ateAll = false;
    int numberOfDotsToEat;
    public bool canMove;

    private void Start()
    {
        _spriteRender = gameObject.GetComponent<SpriteRenderer>();
        GameObject dotsParent = GameObject.Find("DotsParent");
        numberOfDotsToEat = dotsParent.transform.childCount;
        ateAll = false;
        canMove = true;
    }
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float h = moveHorizontal * speed;
        float v = moveVertical * speed;
        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        //float newVelocityX = 0f;
        if (canMove)
        {
            if (moveHorizontal < 0) // press left and stationary or moving left
            {
                //newVelocityX = -speed;
                rb.AddForce(-Vector2.right * speed);
            }
            else if (moveHorizontal > 0) // press right and stationary or movin
            {
                //newVelocityX = speed;
                rb.AddForce(Vector2.right * speed);
            }
            else
            {
                //newVelocityX = 0;
            }
            //float newVelocityY = 0f;
            if (moveVertical < 0)
            {
                //newVelocityY = -speed;
                rb.AddForce(Vector2.down * speed);
            }
            else if (moveVertical > 0)
            {
                //newVelocityY = speed;
                rb.AddForce(Vector2.up * speed);
            }

            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(h, v);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (dotCount >= numberOfDotsToEat)
        {
            ateAll = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Consumable")
        {
            Debug.Log("hit");
            StartCoroutine(MoveConsumable(collider.gameObject));
        }

    }

    IEnumerator MoveConsumable(GameObject consumable)
    {
        while (consumable.transform.position != transform.position)
        {
            consumable.transform.position = Vector3.MoveTowards(consumable.transform.position, gameObject.transform.position, Time.deltaTime * speed);
            yield return null;

        }
        Destroy(consumable);
        dotCount++;
    }

}
