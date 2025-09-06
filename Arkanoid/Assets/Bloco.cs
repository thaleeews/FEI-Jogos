using UnityEngine;

public class Bloco : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Ball") {
            GameController.instance.AddPoints(10);
            Destroy(gameObject);
        }
    }
}
