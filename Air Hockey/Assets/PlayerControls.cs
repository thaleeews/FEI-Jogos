using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D rb2d; 
    
    public float boundY;
    public float boundX;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();     // Inicializa a raquete
    }

    // Update is called once per frame
    void Update()
    {
        //no update
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x > boundX || mousePos.x < -boundX) {
            mousePos.x = transform.position.x;
        }
        if (mousePos.y > -0.5 || mousePos.y < boundY) {
            mousePos.y = transform.position.y;
        }
        rb2d.MovePosition(mousePos);
    }
}
