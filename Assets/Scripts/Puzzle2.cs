using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : MonoBehaviour
{
    int[] theCode = new int[4];
    int counter = 0;

    [SerializeField] Sprite Water_Off;
    [SerializeField] Sprite Water_On;
    [SerializeField] Sprite Claws_Off;
    [SerializeField] Sprite Claws_On;
    [SerializeField] Sprite Bird_Off;
    [SerializeField] Sprite Bird_On;
    [SerializeField] Sprite Fire_Off;
    [SerializeField] Sprite Fire_On;

    [SerializeField] GameObject altar;
    [SerializeField] GameObject tempWall;

    [SerializeField] CameraBehaviour camera;
    [SerializeField] GameObject puzzleArea;


    public void ChangeCode(int index)
    {
        theCode[counter] = index + 1;
        counter++;

        switch (index)
        {
            case 0:
                transform.GetChild(index).GetComponent<SpriteRenderer>().sprite = Water_On;
                transform.GetChild(index).GetComponent<BoxCollider2D>().enabled = false;
                break;
            case 1:
                transform.GetChild(index).GetComponent<SpriteRenderer>().sprite = Fire_On;
                transform.GetChild(index).GetComponent<BoxCollider2D>().enabled = false;
                break;
            case 2:
                transform.GetChild(index).GetComponent<SpriteRenderer>().sprite = Claws_On;
                transform.GetChild(index).GetComponent<BoxCollider2D>().enabled = false;
                break;
            case 3:
                transform.GetChild(index).GetComponent<SpriteRenderer>().sprite = Bird_On;
                transform.GetChild(index).GetComponent<BoxCollider2D>().enabled = false;
                break;
            default:
                break;
        }

        if (counter == 4 && IsPuzzleCompleted()) OpenAltar();
        else if(counter == 4) StartCoroutine(ResetPuzzle());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) OpenAltar();
    }

    bool IsPuzzleCompleted()
    {
        if (theCode[0] == 4 && theCode[1] == 2 && theCode[2] == 3 && theCode[3] == 1) return true;
        else return false;
    }

    void OpenAltar()
    {
        altar.SetActive(true);
        tempWall.SetActive(false);
        camera.ChangeCameraMode(0);
        puzzleArea.SetActive(false);
    }

    IEnumerator ResetPuzzle()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Water_Off;
        
        transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Fire_Off;

        transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = Claws_Off;

        transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = Bird_Off;

        counter = 0;
        for (int i = 0; i < 4; i++)
        {
            theCode[i] = 0;
        }

        yield return new WaitForSeconds(3f);
        transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
        transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
        transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = true;
        transform.GetChild(3).GetComponent<BoxCollider2D>().enabled = true;
    }

}
