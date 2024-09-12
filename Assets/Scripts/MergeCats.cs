using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeCats : MonoBehaviour
{
    [SerializeField] GameObject particlesSpawn;
    PointEnter pointEnter;
    Advertising advertising;

    Rigidbody2D rb;
    GameController gameController;

    //Score score;

    //Vector3 catPos;

    bool isFollowing = true;

    private float minXPos = -0.45f; //ограничение передвижения по оси X
    private float maxXPos = 4f;
    private int crosshairChild = 0;  //Crosshair найти дочерний объект и уничтожить 1 раз
    //private float sensitivity = 1f;
    //private bool touchWas = false;


    private void Start()
    {
        advertising = FindObjectOfType<Advertising>();
        pointEnter = FindObjectOfType<PointEnter>();
        rb = GetComponent<Rigidbody2D>();
        gameController = FindObjectOfType<GameController>();
        //score = FindObjectOfType<Score>();
        rb.simulated = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            //catPos = transform.position;
            //Destroy(collision.gameObject);
            if (GetInstanceID() < collision.gameObject.GetInstanceID())
            {
                for (int i = 0; i <= gameController.catsWithoutMouse.Length; i++)
                {
                    if (tag == gameController.catsWithoutMouse[i].tag)
                    {
                        //score.ScoreValue(i * 5 + 5);
                        gameController.DoMerge(collision, gameObject, i);

                        //Instantiate(gameController.catsWithoutMouse[++i], catPos, Quaternion.identity);
                        //Instantiate(particlesSpawn, catPos, Quaternion.identity);
                        //gameController.PumpSound();
                        //Destroy(gameObject);
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
        if (isFollowing)
        {
            if (advertising.isAdvertising || gameController.lose)      //Может не работать если у человека стоит AdBlock
            {
                Destroy(gameObject);
            }

            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cursorPosition.z = 0f;
            float clampedX = Mathf.Clamp(cursorPosition.x, minXPos, maxXPos);
            transform.position = new Vector3(clampedX, 4f, 0f);
        }
        if (gameController.canDrop)
        {
            if (Input.GetMouseButtonUp(0) && !pointEnter.mouseOnButton && !gameController.mouseOnButton && gameController.delayAfterAdvertising == true)
            {
                isFollowing = false;
                rb.simulated = true;
                if (crosshairChild == 0)
                {
                    crosshairChild++;
                    Transform firstChild = transform.GetChild(0);   //Crosshair найти дочерний объект и уничтожить 1 раз
                    Destroy(firstChild.gameObject); //Crosshair найти дочерний объект и уничтожить 1 раз
                }
            }
        }
    }
}
