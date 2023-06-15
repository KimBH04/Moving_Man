using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image gauge;
    public TextMeshProUGUI arrowKey;
    public TextMeshProUGUI timeTxt;
    public GameObject Panel;

    public float startSpeed = 0.001f;
    public float acceleration = 1000f;

    float startDelay = 3f;

    public Animator anime;
    private readonly int hashSpeed = Animator.StringToHash("Speed");

    private bool isDie;
    private AudioSource gameOver;

    private bool key;

    private float time;

    void Start()
    {
        gameOver = GetComponent<AudioSource>();
        anime.SetFloat(hashSpeed, 0f);
        Panel.SetActive(false);
        key = Random.Range(0, 2) > 0;
        timeTxt.text = "0s";
        Change();
    }

    void Update()
    {
        if (startDelay > 0f)
        {
            startDelay -= Time.deltaTime;
            return;
        }

        if (isDie)
        {
            return;
        }

        if (gauge.fillAmount == 0f)
        {
            isDie = true;
            Panel.SetActive(true);
            anime.SetFloat(hashSpeed, 0f);
            gameOver.Play();

            return;
        }

        if (key)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Change();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Change();
            }
        }
        
        gauge.fillAmount -= startSpeed;
        startSpeed += Time.deltaTime * acceleration;
        Timer();

        anime.SetFloat(hashSpeed, startSpeed * 200f);
    }

    void Change()
    {
        key = !key;
        arrowKey.text = key ? "<" : ">";
        gauge.fillAmount = 1f;
    }

    void Timer()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("0.000") + "s";
    }
}
