using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeCatsNoMouse : MonoBehaviour
{
    [SerializeField] GameObject particlesSpawn;

    GameController gameController;
    //bool checkCollision = false;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        //StartCoroutine(CheckCollision());
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag(tag))
            {
                if (GetInstanceID() < collision.gameObject.GetInstanceID())
                {
                    for (int i = 0; i <= gameController.catsWithoutMouse.Length; i++)
                    {
                        if (tag == gameController.catsWithoutMouse[i].tag)
                        {
                            gameController.DoMerge(collision, gameObject, i);

                            //score.ScoreValue(i * 5 + 5);
                            //Instantiate(gameController.catsWithoutMouse[++i], catPos, Quaternion.identity);
                            //print(gameController.catsWithoutMouse[i]);
                            //Instantiate(particlesSpawn, catPos, Quaternion.identity);
                            //gameController.PumpSound();
                            //Destroy(gameObject);
                            break;
                        }
                    }
                }
            }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag(tag))
            {
                if (GetInstanceID() < collision.gameObject.GetInstanceID())
                {
                    for (int i = 0; i <= gameController.catsWithoutMouse.Length; i++)
                    {
                        if (tag == gameController.catsWithoutMouse[i].tag)
                        {
                            gameController.DoMerge(collision, gameObject, i);
                            break;
                        }
                    }
                }
            }
    }


private void Update()
    {
        if (gameController.isRestart)
        {
            Destroy(gameObject);
        }
    }

    //private IEnumerator CheckCollision()
    //{
    //    yield return new WaitForSeconds(0.15f);
    //    checkCollision= true;
    //    yield return new WaitForSeconds(0.15f);
    //    checkCollision = true;
    //    yield return new WaitForSeconds(0.15f);
    //    checkCollision = true;
    //    yield return new WaitForSeconds(0.15f);
    //    checkCollision = true;
    //}
}
