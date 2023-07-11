using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillateBrick : MonoBehaviour
{
    private float range = 0.4f;
    private float speed = 0.5f;
    private bool movingRight;
    private Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        //movingRight = RandomBool();
        movingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        OscillatePeg();
    }

    private void OscillatePeg()
    {
        if (movingRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            if (transform.position.x >= startingPosition.x + range)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (transform.position.x <= startingPosition.x + -range)
                movingRight = true;
        }
    }

    private bool RandomBool()
    {
        if (Random.value >= 0.5f)
            return true;
        else
            return false;
    }
}
