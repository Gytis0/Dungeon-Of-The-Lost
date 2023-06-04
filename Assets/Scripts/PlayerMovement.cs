using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    Transform player;

    [SerializeField] List<AudioClip> walkingClips = new List<AudioClip>();
    [SerializeField] AudioSource audioSource;

    [Range(0.01f, 1)][SerializeField] float speed = 1f;
     float horizontalMove;
     float verticalMove;
    public bool Paused = false;
    [SerializeField] float timer = 0.65f;

    void Start()
    {
        player = GetComponentInParent<Transform>();
    }

    void Update()
    {

        if(!Paused)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");
        }

        timer -= Time.deltaTime;

        if ((horizontalMove != 0 || verticalMove != 0) && timer <=0)
        {

            audioSource.clip = walkingClips[Random.Range(0, walkingClips.Count)];
            audioSource.Play();
        }

        if (timer <= 0) timer = .65f;


        if (horizontalMove > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (horizontalMove < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void FixedUpdate()
    {
       
        player.position = new Vector3(horizontalMove * speed, verticalMove * speed, 0) + player.position;

        animator.SetInteger("Horizontal", (int)horizontalMove);
        animator.SetInteger("Vertical", (int)verticalMove);
       
    }

   
}
