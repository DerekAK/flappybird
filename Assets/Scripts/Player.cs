using UnityEngine;

public class Player : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite[] spritesToCycleThrough;
    private int spriteIndex;
    private Vector3 direction;

    public float gravity = -9.8f;

    public float strength = 5f;

    // calls this the first frame object is initialized
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // calls this just before first frame update, after Awake() has been ran 
    // private void Start(){
    //     InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f); // will call AnimateSprite() every .15 seconds
    // }

    private void AnimateSprite(){
        spriteIndex ++;
        if (spriteIndex >= spritesToCycleThrough.Length){
            spriteIndex = 0;
        }

        spriteRenderer.sprite = spritesToCycleThrough[spriteIndex];
    }

    // unity calls automatically every frame this script is running, usually where we handle input
    private void Update(){
        // this line will detect if either space bar or left click on mouse is pressed (left click is indexed at 0, right is 1?)
        // this will update the direction variable, which is type Vector3
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            direction = Vector3.up * strength;
        }

        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0); //left click

            if (touch.phase == TouchPhase.Began) { //so when user just touches phone, direction is applied up to sprite
                direction = Vector3.up * strength;
            }
        }
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Obstacle"){
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Scoring"){
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

    private void OnEnable(){
        // Player is enabled/disabled in GameManager with pause and play
        // This just resets the position and direction vectors to zero basically
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        
        direction = Vector3.zero;
    }
}
