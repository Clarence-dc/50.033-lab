using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableMushroomSimpleController : MonoBehaviour
{
    private Rigidbody2D mushroomBody;
    private Vector2 currentDirection;
    public float speed;
    private bool isColliding;
    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        // get starting position
        currentDirection = new Vector2(Random.Range(0,2)*2-1, 0);
        mushroomBody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        mushroomBody.AddForce(currentDirection * speed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Player")){
            speed = 0;
            isColliding = true;
            mushroomBody.velocity = new Vector2(0, mushroomBody.velocity.y);
            StartCoroutine(DisableHittable());
        }else if (col.gameObject.CompareTag("Pipe")){
            currentDirection = new Vector2(currentDirection.x * -1, 0);
            mushroomBody.velocity = new Vector2(0, mushroomBody.velocity.y);
            mushroomBody.AddForce(currentDirection * speed, ForceMode2D.Impulse);
        }
    }

    void OnCollisionExit2D(Collision2D col){
        if (col.gameObject.CompareTag("Player")){
            isColliding = false;
        }
    }

    bool  ObjectMovedAndStopped(){
        return Mathf.Abs(mushroomBody.velocity.y) == 0.0;
    }

    IEnumerator  DisableHittable(){
        if (!ObjectMovedAndStopped() && isColliding){
            yield  return  new  WaitUntil(() =>  ObjectMovedAndStopped() && !isColliding);
        }

        //continues here when the ObjectMovedAndStopped() returns true and Object is not colliding with "Player"
        mushroomBody.bodyType  =  RigidbodyType2D.Static; // make the mushroom unaffected by Physics
    }

    void  OnBecameInvisible(){
        Destroy(gameObject);	
    }
}
