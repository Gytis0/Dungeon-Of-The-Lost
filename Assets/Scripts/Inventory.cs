using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] Sprite emptyIcon;
    [SerializeField] Canvas inventoryCanvas;
    [SerializeField] Sprite purpleGem;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip gemPickUp;

    public Item[] itemSlots = new Item[5];
    List<int> freeSlots = new List<int>();

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            freeSlots.Add(i);
            itemSlots[i] = new Item(emptyIcon, -1);
        }
    }

    public void GetItem(Sprite icon, int id)
    {
        itemSlots[freeSlots[0]] = new Item(icon, id);
        UpdateInventory(freeSlots[0]);

        if (id != 0)
        {
            audioSource.clip = gemPickUp;
            audioSource.Play();
            freeSlots.RemoveAt(0);
            if (CheckForPurpleGem())

            {
                for (int i = 0; i < 5; i++)
                {
                    if (itemSlots[i].id > 3)
                    {
                        DeleteItem(i);
                    }
                }

                GetItem(purpleGem, 3);
            }
        }
        else if(id == 0)
        {
            DeleteItem(0);
        }
    }
   
    public void DeleteItem(int place)
    {
        itemSlots[place] = new Item(emptyIcon);
        UpdateInventory(place);
        freeSlots.Add(place);
    }

    void UpdateInventory(int id)
    {
        inventoryCanvas.transform.GetChild(id).GetChild(1).GetComponent<Image>().sprite = itemSlots[id].icon;
    }

    bool CheckForPurpleGem()
    {
        int counter = 0;
        for (int i = 0; i < 5; i++)
        {
            if (itemSlots[i].id > 3) counter++;
        }

        if (counter == 3) return true;
        else return false;
    }
      public class Item
    {
        public Item()
        {
            id = -1;
        }
        public Item(Sprite _icon)
        {
            icon = _icon;
        }
        public Item (Sprite _icon, int _id)
        {
            icon = _icon;
            id = _id;
        }
        public Sprite icon;
        public int id;
        enum Items { empty, gem_blue, gem_orange, gem_purple, gem_purple1, gem_purple2, gem_purple3 };
    }
}

