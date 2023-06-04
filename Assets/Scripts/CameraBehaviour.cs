using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject blackout;
    Camera cameraComp;
    [SerializeField] Vector3 Puzzle1CameraLocation;
    [SerializeField] Vector3 Puzzle2CameraLocation;
    [SerializeField] Animator animator;
    float lerpValue = 0f;

    int CameraMode = 0;

    private void Start()
    {
        cameraComp = GetComponent<Camera>();
    }

    void Update()
    {
        if(CameraMode == 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y,-10), lerpValue);
            cameraComp.orthographicSize = Mathf.Lerp(6.5f, 3.5f, lerpValue);
            if (lerpValue != 1) lerpValue += 0.005f;
        }
           
        else if(CameraMode == 1)
        {
            transform.position = Vector3.Lerp(transform.position, Puzzle1CameraLocation, lerpValue);
            cameraComp.orthographicSize = Mathf.Lerp(3.5f, 6.5f, lerpValue);
            if (lerpValue != 1) lerpValue += 0.005f;
        }

        else if (CameraMode == 2)
        {
            transform.position = Vector3.Lerp(transform.position, Puzzle2CameraLocation, lerpValue);
            cameraComp.orthographicSize = Mathf.Lerp(3.5f, 6.5f, lerpValue);
            if (lerpValue != 1) lerpValue += 0.005f;
        }
    }

    public void ChangeCameraMode(int _mode)
    {
        CameraMode = _mode;
        lerpValue = 0f;
    }

    public void PlayFadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void PlayFadeIn()
    {
        animator.SetTrigger("FadeIn");
    }

    public void PlayInstantBlackout()
    {
        StartCoroutine(InstantBlackout());
    }

    IEnumerator InstantBlackout()
    {
        blackout.SetActive(true);
        yield return new WaitForSeconds(1f);
        blackout.SetActive(false);
    }
}
