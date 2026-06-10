using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSys : Controller, IUpdatable
{
    public List<GameObject> heldItems = new();
    public List<GameObject> itemsDisplayed = new();

    public GameObject imagePrefab;
    public Transform itemDisplay;
    public override void OnStart()
    {
    
    }
    public void OnUpdate(float dt)
    {

    }
    public bool GetKey(InteractiveBaseEnviroment target)
    {
        if (target == null) return false;
        if (heldItems == null || heldItems.Count == 0) return false;

        for (int i = 0; i < heldItems.Count; i++)
        {
            if (heldItems[i] != null && heldItems[i].TryGetComponent(out Unit unit) && target.CheckKey(unit.UnitSO))
            {
                heldItems.RemoveAt(i);
                Destroy(itemDisplay.GetChild(0).gameObject);
                return true;
            }
        }
        return false;
    }
    public void AddItem(GameObject item)
    {
        heldItems.Add(item);
        item.SetActive(false);

        ItemDisplay();
    }
    private void ItemDisplay()
    {
        Instantiate(imagePrefab, itemDisplay);
    }
}
