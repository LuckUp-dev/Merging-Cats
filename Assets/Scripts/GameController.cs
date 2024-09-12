using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameController : MonoBehaviour
{

    [SerializeField] AudioSource musicBackground;
    [SerializeField] AudioSource cutSound;
    [SerializeField] AudioSource pumpSound;
    [SerializeField] Image showNextCat;
    [SerializeField] Sprite[] nextCatImage;
    [SerializeField] GameObject particlesSpawn;
    [SerializeField] Score score;

    [SerializeField] GameObject musicOffPictures;
    [SerializeField] GameObject soundOffPictures;


    public GameObject[] allCatsPreffabs;
    public GameObject[] catsWithoutMouse;

    Scene scene;

    int numberOfNextCat;
    bool hasSpawned = false;
    public bool needToChose = false;            //нужно выбрать кота дл€ спавна
    public bool mouseOnAdvertising = false;     //курсор на кнопке рекламы
    public bool mouseOnButton = false;          //курсор на кнопке
    public bool lose = false;
    public bool isRestart = false;
    public bool soundEnabled;
    public bool pumpEnabled;             //отвечает за все звуки
    public bool canDrop = false;
    public bool delayAfterAdvertising = true;

    private bool isCorotine = false;

    private float minXPos = -0.45f; //ограничение передвижени€ по оси X
    private float maxXPos = 4f;


    private void OnEnable() => YandexGame.GetDataEvent += CheckSDK;


    private void OnDisable() => YandexGame.GetDataEvent -= CheckSDK;
    private void Awake()
    {

        if (YandexGame.SDKEnabled == true)
        {
            //scene = SceneManager.GetActiveScene();
            //if (YandexGame.EnvironmentData.deviceType != "desktop" && scene.name != "ForMobile" )
            //{
            //    SceneManager.LoadScene("ForMobile");
            //}

            // ≈сли запустилс€, то запускаем ¬аш метод
            CheckSDK();
        }
    }


    void Start()
    {
        SpawnFirstCat();
        NextCatSpawn();
    }
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePosition.x >= -1.5f && mousePosition.x <= 5.5f &&
           mousePosition.y >= -3.9f && mousePosition.y <= 6f)
        {
            canDrop = true;
            if (Input.GetMouseButtonUp(0) && !hasSpawned && !needToChose && !mouseOnAdvertising && !lose && delayAfterAdvertising)
            {
                if (pumpEnabled)
                {
                    CutSound();
                }
                hasSpawned = true;
                StartCoroutine(SpawnTimer());
            }
        }
        else
        {
            canDrop = false;
        }
    }


    public void CheckSDK()
    {
        scene = SceneManager.GetActiveScene();

        if (YandexGame.EnvironmentData.deviceType != "desktop" && scene.name != "ForMobile")
        {
            SceneManager.LoadScene("ForMobile");
        }

        soundEnabled = YandexGame.savesData.musicOn;
        if (soundEnabled)
        {
            MusicPlay();
            musicOffPictures.SetActive(false);
        }
        else if (!soundEnabled)
        {
            musicOffPictures.SetActive(true);
        }

        pumpEnabled = YandexGame.savesData.soundOn;
        if (pumpEnabled)
        {
            soundOffPictures.SetActive(false);
        }
        else if (!pumpEnabled)
        {
            soundOffPictures.SetActive(true);
        }
    }

    public void NextCatSpawn()
    {
        numberOfNextCat = Random.Range(0, 4);
        showNextCat.sprite = nextCatImage[numberOfNextCat];
    }

    public void SpawnCat()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float clampedX = Mathf.Clamp(mousePos.x, minXPos, maxXPos);
        Instantiate(allCatsPreffabs[numberOfNextCat], new Vector3(clampedX, 4f, 0f), Quaternion.identity);
    }

    public void SpawnFirstCat()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //ѕомен€ть потом кол-во макс спавна

        float clampedX = Mathf.Clamp(mousePos.x, minXPos, maxXPos);
        Instantiate(allCatsPreffabs[Random.Range(0, 4)], new Vector3(clampedX, 4f, 0f), Quaternion.identity);
    }

    public void SpawnRewardCat(int digitCat)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float clampedX = Mathf.Clamp(mousePos.x, minXPos, maxXPos);
        Instantiate(allCatsPreffabs[digitCat], new Vector3(clampedX, 4f, 0f), Quaternion.identity);
    }

    public void PumpSound()
    {
        if (pumpEnabled)
        {
            pumpSound.Play();
        }
    }
    public void MusicStop()
    {
        musicBackground.Stop();
    }
    public void MusicPlay()
    {
        if (soundEnabled)
        {
            musicBackground.Play();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        MuteSounds();
    }
    private void OnApplicationPause(bool pause)
    {
        MuteSounds();
    }
    private void MuteSounds()
    {
        musicBackground.mute = !musicBackground.mute;
        if (pumpEnabled)
        {
            pumpEnabled = false;
        }
        else
            pumpEnabled = true;
    }

    private void CutSound()
    {
        cutSound.Play();
    }

    public void DoMerge(Collision2D collision, GameObject gameObject, int i)
    {
        if (!isCorotine)
        {
            isCorotine = true;
            StartCoroutine(Merge());
            if (gameObject.tag == "xOmega Cat" || collision.gameObject.tag == "xOmega Cat")
            {
                score.score += 945;
                Instantiate(particlesSpawn, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
            score.ScoreValue(i * 5 + 5);
            Destroy(collision.gameObject);
            Instantiate(catsWithoutMouse[++i], (gameObject.transform.position + collision.transform.position) / 2, Quaternion.identity);
            Instantiate(particlesSpawn, (gameObject.transform.position + collision.transform.position) / 2, Quaternion.identity);
            PumpSound();
            Destroy(gameObject);
        }
    }
    public void DelayCouratine()
    {
        StartCoroutine(DontDropOnAdvertising());
    }

    private IEnumerator Merge()
    {
        yield return new WaitForSeconds(0.001f);
        isCorotine = false;
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnCat();
        NextCatSpawn();
        hasSpawned = false;
    }

    private IEnumerator DontDropOnAdvertising()
    {
        delayAfterAdvertising = false;
        yield return new WaitForSeconds(0.15f);
        delayAfterAdvertising = true;
    }

}
