using UnityEngine;

public class ItemSys : Controller, IUpdatable
{
    public GameObject[] heldItems;
    public override void OnStart()
    {
    
    }
    public void OnUpdate(float dt)
    {

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
                return;
            }
        }
    }
}
