using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float movingSpeed = 5f;
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * parallaxEffect;
        if(transform.position.x < -length )
            transform.position = new Vector3(length, transform.position.y, transform.position.z);
    }
}
