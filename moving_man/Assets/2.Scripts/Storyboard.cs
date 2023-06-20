using System.Collections;
using UnityEngine;

public class Storyboard : MonoBehaviour
{
    private AudioSource m_AudioSource;

    public GameObject[] boards;
    public AudioClip[] sounds;

    public float[] Seconds;

    void Awake()
    {
        foreach (var board in boards)
        {
            board.SetActive(false);
        }
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        for (int i = 0; i < boards.Length; i++)
        {
            StartCoroutine(PlayStory(i));
        }
    }

    IEnumerator PlayStory(int i)
    {
        yield return new WaitForSeconds(Seconds[i]);
        boards[i].SetActive(true);
        m_AudioSource.clip = sounds[i];
        m_AudioSource.Play();
    }

    IEnumerator Run()
    {
        yield return new WaitForSeconds(8);
        m_AudioSource.clip = sounds[^1];
        m_AudioSource.Play();
        yield return new WaitForSeconds(2);

        //무빙
    }
}
