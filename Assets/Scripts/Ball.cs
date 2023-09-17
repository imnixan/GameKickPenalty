using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Kicker kicker;

    [SerializeField]
    private AudioClip kick,
        hit,
        post;

    private Rigidbody rb;
    private bool kicked;
    private bool goalHappen;

    [SerializeField]
    private ParticleSystem firework;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        goalHappen = true;
        if (!firework.isPlaying)
        {
            firework.Play();
        }
    }

    public void Respawn()
    {
        goalHappen = false;
        transform.position = new Vector3(0, 1.5f, -9f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        kicked = false;
    }

    public void Kick(Vector3 pos)
    {
        PlaySound(kick);
        rb.AddForce((pos - transform.position) * 200);
        kicked = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (kicked)
        {
            StartCoroutine(NextShootCount());
            kicked = false;
        }
        if (collision.gameObject.CompareTag("Post"))
        {
            PlaySound(post);
        }
        else
        {
            PlaySound(hit);
        }
    }

    IEnumerator NextShootCount()
    {
        yield return new WaitForSeconds(1.5f);
        kicker.Restore(goalHappen);
    }

    private void PlaySound(AudioClip clip)
    {
        if (PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
}
