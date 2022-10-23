using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    public float speed;
    public Text score;
    public Text Lives;
    private int scoreValue = 0;
    private int LivesValue = 3;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        Lives.text = LivesValue.ToString();
        WinTextObject.SetActive(false);
        LoseTextObject.SetActive(false);
        Lives.text = "Lives: " + LivesValue.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (scoreValue == 4)
        {
            LivesValue = 3;
            Lives.text = "Lives: " + LivesValue.ToString();
            transform.position = new Vector2(0.0f, 0.0f);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
       else if (collision.collider.tag == "Enemy")
        {
            LivesValue = LivesValue - 1;
            Lives.text = "Lives: " + LivesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                anim.SetInteger("State", 3);
            }
        }
    }
    void Update()
    {
        if(scoreValue >= 8)
        {
            WinTextObject.SetActive(true);
        }
        if(LivesValue == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(this);
            speed = 0;
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 2);
            gameObject.transform.localScale = new Vector2(0.267f, .234f);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
          anim.SetInteger("State", 1);
          gameObject.transform.localScale = new Vector2(-0.267f, .234f);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
          anim.SetInteger("State", 0);
        }
    }
}