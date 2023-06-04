using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class FinalGate : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject enableOnCompletion;
    [SerializeField] GameObject disableOnCompleteion;
    [SerializeField] CameraBehaviour camera;
    [SerializeField] AudioSource audioSource;

    [SerializeField] Sprite BlueGem;
    [SerializeField] Sprite OrangeGem;
    [SerializeField] Sprite PurpleGem;
    bool interactive = false;
    int counter = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactive)
        {
            PlaceGem();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(EndGame());
    }

    void PlaceGem()
    {
        for (int i = 0; i < 5; i++)
        {
            if(inventory.itemSlots[i].id == 1)
            {
                transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = BlueGem;
                inventory.DeleteItem(i);
                counter++;
                if (CheckForCompletion())
                {
                    OpenTheGate();
                }
                return;
            }
            else if (inventory.itemSlots[i].id == 2)
            {
                transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = OrangeGem;
                inventory.DeleteItem(i);
                counter++;
                if (CheckForCompletion())
                {
                    OpenTheGate();
                }
                return;
            }
            else if (inventory.itemSlots[i].id == 3)
            {
                transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = PurpleGem;
                inventory.DeleteItem(i);
                counter++;
                if (CheckForCompletion())
                {
                    OpenTheGate();
                }
                return;
            }
        }

        
    }

    bool CheckForCompletion()
    {
        if (counter == 3) return true;
        else return false;
    }

    void OpenTheGate()
    {
        audioSource.Play();
        enableOnCompletion.SetActive(true);
        disableOnCompleteion.SetActive(false);
    }

    IEnumerator EndGame()
    {
        camera.PlayFadeOut();
        yield return new WaitForSeconds(2f);
        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void EnableInteractivity()
    {
        interactive = true;
        text.enabled = true;
    }

    public void DisableInteractivity()
    {
        interactive = false;
        text.enabled = false;
    }
}
