using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float acceleration = 1000f;

    public GameObject kane;
    private Animator anime;
    private Transform trans;

    [Header("UI")]
    public Image gauge;
    public TextMeshProUGUI arrowKey;
    public TextMeshProUGUI timeTxt;
    public TextMeshProUGUI countDownTxt;
    public GameObject Panel;

    public Transform background;

    [Header("Audio")]
    public AudioClip chunjat;
    public AudioClip dudududu;
    public AudioClip aigonan;
    public AudioClip uuu;
    public AudioClip yonsa;
    private AudioSource audioSource;

    float startDelay = 3f;

    private readonly int hashSpeed = Animator.StringToHash("Speed");

    private bool isDie;
    private bool key;

    private float time;

    private float speed;
    private float accele;
    private float speedScale;

    IEnumerator Start()
    {
        anime = kane.GetComponent<Animator>();
        trans = kane.GetComponent<Transform>();

        accele = Time.time;
        StartCoroutine(CountDown());
        StartCoroutine(UU_U());
        audioSource = GetComponent<AudioSource>();

        anime.SetFloat(hashSpeed, 0f);
        Panel.SetActive(false);

        key = Random.Range(0, 2) > 0;
        Change();

        timeTxt.text = "0s";

        background.position = new(10f, 0f);

        yield return new WaitForSeconds(startDelay);
        audioSource.PlayOneShot(chunjat);
        audioSource.clip = dudududu;
        audioSource.Play();
    }

    IEnumerator CountDown()
    {
        for (int i = 3; i > 0; i--)
        {
            countDownTxt.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countDownTxt.gameObject.SetActive(false);
    }

    IEnumerator UU_U()
    {
        yield return new WaitForSeconds(startDelay + 10f);
        audioSource.PlayOneShot(uuu, 0.5f);

        yield return new WaitForSeconds(10f);
        audioSource.PlayOneShot(yonsa, 0.4f);
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
            StopAllCoroutines();

            isDie = true;
            Panel.SetActive(true);
            anime.SetFloat(hashSpeed, 0f);
            audioSource.Stop();
            audioSource.PlayOneShot(aigonan, 0.5f);

            return;
        }

        if (Input.GetKeyDown((KeyCode)275 + (key ? 1 : 0)))
        {
            Change();
        }

        speed = (Time.time - accele) / 1500f * speedScale;

        gauge.fillAmount -= speed;
        Timer();

        background.position -= new Vector3(speed * 10f, 0f);
        if (background.position.x < -10f)
        {
            background.position = new(10f, 0f);
        }

        trans.position += new Vector3(Time.deltaTime / 7f, 0f);
        anime.SetFloat(hashSpeed, speed * 500f);
    }

    public void SpeedScale(float speed)
    {
        speedScale = (speed + 0.1f) * 2f;
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
