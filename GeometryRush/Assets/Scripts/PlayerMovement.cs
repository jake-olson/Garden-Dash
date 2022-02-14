using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This class controls the player's movement and its related aspects
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 playerVelocity;
    [SerializeField] Vector2 startPosition;
    [SerializeField] GameObject background;
    [SerializeField] GameObject soil;
    [SerializeField] GameObject beeWall;
    [SerializeField] GameObject endTextPro;
    [SerializeField] Canvas canvas;
    private Rigidbody2D rbd;
    private GameObject endText;
    private GameObject newText;
    private Camera mc;
    private bool contact;


    // Start is called before the first frame update
    void Start()
    {
        contact = true;
        rbd = GetComponent<Rigidbody2D>();
        rbd.velocity = playerVelocity;
        endText = new GameObject("Lose Text");
        mc = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Handles if the rabbit gets stuck on a pot
        if(rbd.velocity.x < 4)
        {
            background.GetComponent<Scroll>().setSpeed(rbd.velocity.x / 4 * 0.18f);
            soil.GetComponent<GroundScroll>().setSpeed(rbd.velocity.x / 4 * 0.18f);
            
        }
        rbd.velocity = new Vector2(4, rbd.velocity.y);

        // Win condition
        if (rbd.position.x > 126.2f)
        {
            reset("You Win");
        }

        // Lose condition
        if(beeWall.GetComponent<Rigidbody2D>().position.x >= rbd.position.x - transform.localScale.x / 2)
        {
            reset("Try Again");
        }

        // Attempt to jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(contact)
            {
                rbd.velocity = new Vector2(rbd.velocity.x, 8);
                contact = false;
            }
            // Unpause the game after a win/loss
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                Destroy(newText);
                contact = true;
            }
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Surface" || collision.gameObject.tag == "Ground")
        {
            contact = true;
        }
        if (collision.gameObject.tag == "Shovel")
        {
            reset("Try Again");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            rbd.velocity = new Vector2(4, rbd.velocity.y);
            background.GetComponent<Scroll>().setSpeed(0.18f);
            soil.GetComponent<GroundScroll>().setSpeed(0.18f); 
        }
    }

    // Reset the game
    private void reset(string text)
    {
        beeWall.GetComponent<BeeWall>().resetPosition();
        Time.timeScale = 0;
        newText = Instantiate(endTextPro, new Vector3(mc.transform.position.x, mc.transform.position.y, 0), Quaternion.identity, canvas.transform);
        newText.GetComponent<TextMeshProUGUI>().text = text;
        newText.GetComponent<TextMeshProUGUI>().color = Color.red;
        rbd.velocity = playerVelocity;
        rbd.position = startPosition;
        background.GetComponent<Scroll>().setSpeed(0.18f);
        soil.GetComponent<GroundScroll>().setSpeed(0.18f);
    }
}