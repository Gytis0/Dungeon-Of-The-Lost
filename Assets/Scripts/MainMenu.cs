using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioMixer audio;

    private void Start()
    {
        audio.SetFloat("Master", 0);
        audio.SetFloat("Music", 0);
        audio.SetFloat("Sound", 0);
    }
    public void Play()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

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
}
