using UnityEngine;

public class LoseColider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            FindObjectOfType<Level>().ExtraBallsMethods();
        }
        else
        {
            Destroy(collision.gameObject); //destroi qualquer objeto que colide
        }
    }
}
