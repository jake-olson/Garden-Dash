using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeWall : MonoBehaviour
{
    [SerializeField] GameObject beeObject;
    private ArrayList bees;
    private Rigidbody2D rbd;
    private Vector2 beeVelocity;
    private Vector2 startPosition;
  

    void Start()
    {
        startPosition = new Vector2(-5, 5);
        transform.position = startPosition;
        rbd = GetComponent<Rigidbody2D>();
        beeVelocity = new Vector2(4, 0);
        rbd.velocity = beeVelocity;
        bees = new ArrayList();

        // Creates thirty bees in a vertical line
        for (int i = 0; i < 30; i++)
        {
            Debug.Log(i);
            Rigidbody2D clone = Instantiate(beeObject.GetComponent<Rigidbody2D>(), new Vector3(-5, i*0.5f + 0.5f, 0), Quaternion.identity);
            clone.velocity = beeVelocity;
            bees.Add(clone);
        }
    }

    // Reset the position of the wall of bees
    public void resetPosition()
    {
        rbd.position = startPosition;

        for(int i = 0; i < 30; i ++)
        {
            (bees[i] as Rigidbody2D).position = new Vector3(-5, i * 0.5f + 0.5f, 0);
        }
    }

}
