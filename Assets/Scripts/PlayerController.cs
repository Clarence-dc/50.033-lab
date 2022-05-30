using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    // Called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")){
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
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
        // when jumping, and Gomba is near Mario and we haven't registered our score
        if (!onGroundState && countScoreState)
        {
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }
    }

    // FixedUpdate may be called once per frame
    void FixedUpdate()
    {   
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            marioBody.velocity = new Vector2(0, marioBody.velocity.y);
        }
        if (Input.GetKeyDown("space") && onGroundState){
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true; //check if Gomba is underneath
        }

        moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0  && (Input.GetKey("a") || Input.GetKey("d"))){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed){
                marioBody.AddForce(movement * speed);
            }
        }
    }
    
    // on collision trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Gomba!");
            Time.timeScale = 0.0f;
        }
    }
}
