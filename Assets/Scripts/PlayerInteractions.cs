using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] AudioMixer audio;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioSourcePL;
    [SerializeField] AudioClip JEBAITclip;
    [SerializeField] AudioClip pressurePlateClip;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] CameraBehaviour camera;
    [SerializeField] GameObject pauseMenu;
    PlayerMovement playerMovement;
    bool leverInteractivity = false;
    bool JEBAITinteractivity = false;
    bool paused = false;
    Transform currentLever;

    private void Start()
    {
        playerMovement = transform.GetComponent<PlayerMovement>();
        transform.GetChild(2).GetComponent<AudioSource>().Play();
        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && leverInteractivity)
        {
            currentLever.parent.parent.GetComponent<Puzzle1>().ActivateLever(currentLever.GetSiblingIndex());
        }

        if (Input.GetKeyDown(KeyCode.E) && JEBAITinteractivity)
        {
            camera.PlayInstantBlackout();
            audioSource.clip = JEBAITclip;
            audioSource.Play();
            transform.position = new Vector3(-3, -5, 0);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            pauseMenu.SetActive(paused);

            playerMovement.Paused = paused;
            
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Lever"))
        {
            EnableLeverInteractivity();
            currentLever = collision.transform;
        }
        
        if(collision.CompareTag("1#puzzleArea"))
        {
            camera.ChangeCameraMode(1);
        }

        if (collision.CompareTag("2#puzzleArea"))
        {
            camera.ChangeCameraMode(2);
        }

        if (collision.CompareTag("PressurePlate"))
        {
            audioSourcePL.clip = pressurePlateClip;
            audioSourcePL.volume = 0.15f;
            audioSourcePL.Play();
            collision.transform.parent.GetComponent<Puzzle2>().ChangeCode(collision.transform.GetSiblingIndex());
        }

        if (collision.CompareTag("Placeholder"))
        {
            collision.transform.parent.GetComponent<FinalGate>().EnableInteractivity();
        }

        if (collision.CompareTag("JEBAIT"))
        {
            EnableJEBAITinteractivity();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lever"))
        {
            DisableLeverInteractivity();
            currentLever = null;
        }

        if (collision.CompareTag("1#puzzleArea"))
        {
            camera.ChangeCameraMode(0);
        }

        if (collision.CompareTag("2#puzzleArea"))
        {
            camera.ChangeCameraMode(0);
        }

        if (collision.CompareTag("Placeholder"))
        {
            collision.transform.parent.GetComponent<FinalGate>().DisableInteractivity();

        }

        if (collision.CompareTag("JEBAIT"))
        {
            DisableJEBAITinteractivity();
        }
    }

    void EnableLeverInteractivity()
    {
        text.transform.gameObject.SetActive(true);
        leverInteractivity = true;
    }

    void DisableLeverInteractivity()
    {
        text.transform.gameObject.SetActive(false);
        leverInteractivity = false;
    }

    void EnableJEBAITinteractivity()
    {
        JEBAITinteractivity = true;
    }

    void DisableJEBAITinteractivity()
    {
        JEBAITinteractivity = false;
    }

    //mainmenu stuff
    public void ChangeMasterVolume(float _value)
    {
        audio.SetFloat("Master", _value);
    }

    public void ChangeMusicVolume(float _value)
    {
        audio.SetFloat("Music", _value);
    }

    public void ChangeSoundVolume(float _value)
    {
        audio.SetFloat("Sound", _value);
    }

    public void Resume()
    {
        paused = false;
        pauseMenu.SetActive(paused);

        playerMovement.Paused = paused;
    }
    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
