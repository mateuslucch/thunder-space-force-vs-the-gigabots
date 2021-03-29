using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //usar SceneManager para trocar scene
using TMPro;

public class LoseColider : MonoBehaviour
{

    //[SerializeField] TextMeshProUGUI gameStatus; //antiga, quando lose collider mandava em toda derrota
    //[SerializeField] AudioClip youLoseSound;
    //float numberBalls = 1f;

    //cached
    //[SerializeField] Ball ballObject;
    
    void Start()
    {
        //Debug.Log("Number Balls(sums count) " + numberBalls);
        //ballObject = FindObjectOfType<Ball>();
    }
    

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

   
    /* ORIGINAL DAS AULAS
    SceneManager.LoadScene("Game Over"); //ENTRE ASPAS, AI POE O NOME DA SCENE
    SceneManager.LoadScene(currentSceneIndex - 2); //DO NW UI
    */
    /* MEU ORIGINAL, CASO AS VIDAS NÃO DEEM CERTO NO LEVEL.cs
    AudioSource.PlayClipAtPoint(youLoseSound, Camera.main.transform.position);
    gameStatus.text = ("Game Over!!");
    StartCoroutine(Pausa());


}


IEnumerator Pausa()
{
    yield return new WaitForSecondsRealtime(3);
    SceneManager.LoadScene("Game Over");

}
*/

}
