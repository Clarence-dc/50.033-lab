using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed = 10;
    public float upSpeed;
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private float moveHorizontal;
    private bool onGroundState = true;
    private bool faceRightState = true;
    // Called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")){
            onGroundState = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30fps
        Application.targetFrameRate  = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
    }
    // Update may be called once per frame
    void Update()
    { 
        // toggle state
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
        }
        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
        }
    }

    // FixedUpdate may be called once per frame
    void FixedUpdate()
    {   
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            marioBody.velocity = Vector2.zero;
        }
        if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }

        moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed){
                marioBody.AddForce(movement * speed);
            }
        }
        
    }
}
