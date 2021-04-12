using UnityEngine;

public class LoseColider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            FindObjectOfType<Level>().ExtraBallsMethods(); //destroy ball without losing life, if there is more than one
        }
        else
        {
            Destroy(collision.gameObject); //destroi qualquer objeto que colide
        }
    }
}
