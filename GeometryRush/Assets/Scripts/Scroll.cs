using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private float speed;
    private Vector2 offset;
    private float backgroundTimer;
    [SerializeField] GameObject player;
    private Camera mc;


    // Start is called before the first frame update
    void Start()
    {
        speed = 0.18f;
        offset = new Vector2(Time.time * speed, 0);
        mc = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            backgroundTimer += Time.deltaTime;
            offset = new Vector2(backgroundTimer * speed, 0);

        }
        GetComponent<Renderer>().material.mainTextureOffset = offset;
        transform.position = new Vector3(mc.transform.position.x, 7f, transform.position.z);

    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }
}
