using TMPro;
using UnityEngine;

public class ItemSys : Controller, IUpdatable
{
    public GameObject[] heldItems;
    public GameObject[] itemsDisplayed;

    public GameObject imagePrefab;
    public Transform itemDisplay;
    public override void OnStart()
    {
    
    }
    public void OnUpdate(float dt)
    {
        ItemDisplay();
    }
    public bool GetKey(InteractiveBaseEnviroment target)
    {
        if (target == null) return false;
        if (heldItems == null || heldItems.Length == 0) return false;

        for (int i = 0; i < heldItems.Length; i++)
        {
            if (heldItems[i] != null && target.CheckKey(heldItems[i]))
            {
                heldItems[i] = null;
                Destroy(itemDisplay.GetChild(0).gameObject);
                return true;
            }
        }
        return false;
    }
    public void AddItem(GameObject item)
    {
        for (int i = 0; i < heldItems.Length; i++)
        {
            if (heldItems[i] == null)
            {
                heldItems[i] = item;
                item.SetActive(false);
                ItemDisplay();
                return;
            }
        }
    }
    private void ItemDisplay()
    {
        Instantiate(imagePrefab, itemDisplay);
    }
}
