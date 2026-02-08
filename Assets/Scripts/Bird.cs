
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private AudioClip jumpSound;

    [Header("Particles")]
    [SerializeField] private ParticleSystem jumpParticle;

    private AudioSource audioSource;
    private bool jumping = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(jumpSound);
            jumping = true;
            jumpParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            jumpParticle.Play(true);
        }
    }

    private void FixedUpdate()
    {
        if (jumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.Instance.GameOver();
    }
}
