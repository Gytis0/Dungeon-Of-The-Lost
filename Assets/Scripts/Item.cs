using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public enum Items { empty, gem_blue, gem_orange, gem_purple, gem_purple1, gem_purple2, gem_purple3 };
    public Items item;


    [SerializeField] TextMeshPro text;

    Sprite icon;
    int itemID;
    bool interactable = false;

    Inventory inventory;
    

    

    void Start()
    {
        icon = GetComponent<SpriteRenderer>().sprite;
        itemID = (int)item;
    }


    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable)
        {
            inventory.GetItem(icon, itemID);
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        inventory = collision.GetComponent<Inventory>();
        EnableInteractivity();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DisableInteractivity();
    }

    void EnableInteractivity()
    {
        text.gameObject.SetActive(true);
        interactable = true;
    }

    void DisableInteractivity()
    {
        text.gameObject.SetActive(false);
        interactable = false;
    }

    
}
