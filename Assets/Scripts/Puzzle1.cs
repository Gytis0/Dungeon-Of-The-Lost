using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    [SerializeField] Sprite lever_Sprite_Off, lever_Sprite_On;
    [SerializeField] GameObject altar;
    [SerializeField] List<Sprite> tiles = new List<Sprite>();
    [SerializeField] AudioSource audioSource;
    List<Tile> drawing = new List<Tile>();

    [SerializeField] CameraBehaviour camera;
    [SerializeField] GameObject puzzleArea;



    void Start()
    {
        int counter = 0;
        foreach (Transform tile in transform.GetChild(0).transform)
        {
            if (tile.name == "Horizontal")
            {
                drawing.Add(new Tile(tile.GetComponent<SpriteRenderer>().sprite, counter, 1));
                drawing[counter].icon = tiles[counter];
                counter++;
            }
                
            else if (tile.name == "Vertical")
            {
                drawing.Add(new Tile(tile.GetComponent<SpriteRenderer>().sprite, counter, 0));
                drawing[counter].icon = tiles[counter];
                counter++;
            }

            else
            {
                drawing.Add(new Tile(tile.GetComponent<SpriteRenderer>().sprite, counter, 2));
                drawing[counter].icon = tiles[counter];
                counter++;
            }

        }

        int seed;
        Tile temp;
        for (int i = 0; i < 25; i++)
        {
            seed = Random.Range(i, 25);
            temp = drawing[seed];
            drawing[seed] = drawing[i];
            drawing[i] = temp;
            
        }

        UpdateTiles();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) OpenAltar();
    }

    void UpdateTiles()
    {
        
        int counter = 0;
        foreach (Transform tile in transform.GetChild(0).transform)
        { 
            tile.GetComponent<SpriteRenderer>().sprite = drawing[counter].icon;
            counter++;
        }
    }

    public void ActivateLever(int index)
    {
        StartCoroutine(ChangeLeverStance(1.5f, index)); //play the 'animation'

        Tile temp;
        if (index < 5) //horizontal levers
        {
            for (int i = index + 20; i > 4; i-=5)
            {
                temp = drawing[i];
                drawing[i] = drawing[i - 5];
                drawing[i - 5] = temp;
            }
        }
        else
        {
            for (int i = (index - 5) * 5; i < ((index - 5) * 5) + 4; i++)
            {
                temp = drawing[i];
                drawing[i] = drawing[i + 1];
                drawing[i + 1] = temp;
            }
        }
        UpdateTiles();
        PlaySound();
        if (IsPuzzleCompleted()) OpenAltar();
    }

    bool IsPuzzleCompleted()
    {
        for (int i = 0; i < 25; i++)
        {
            //Debug.Log(i + " " + drawing[i].id);
            if (drawing[i].horizontal == 2 && i != drawing[i].id) return false;
            else if (drawing[i].horizontal == 1 && (i < 1 || i > 3) && (i < 21 || i > 23)) return false;
            else if (drawing[i].horizontal == 0 && (i != 5 && i != 9 && i != 10 && i != 14 && i != 15 && i != 19)) return false;
        }
        return true;
    }

    void OpenAltar()
    {
        altar.SetActive(true);
        foreach (Transform item in transform.GetChild(1))
        {
            item.GetComponent<BoxCollider2D>().enabled = false;
        }
        camera.ChangeCameraMode(0);
        puzzleArea.SetActive(false);
    }

    IEnumerator ChangeLeverStance(float time, int index)
    {
        transform.GetChild(1).GetChild(index).GetComponent<SpriteRenderer>().sprite = lever_Sprite_On;

        yield return new WaitForSeconds(time);
        transform.GetChild(1).GetChild(index).GetComponent<SpriteRenderer>().sprite = lever_Sprite_Off;
       
    }

    void PlaySound()
    {
        audioSource.Play();
    }

    class Tile 
    { 
    
        public Tile(Sprite _reference, int _id, int _horizontal)
        {
            icon = _reference;
            id = _id;
            horizontal = _horizontal;
        }

        public Tile(Sprite _reference, int _id)
        {
            icon = _reference;
            id = _id;
            horizontal = 2; //neither horizontal, neither vertical
        }
        public Sprite icon;
        public int id;
        public int horizontal;
    }
}
