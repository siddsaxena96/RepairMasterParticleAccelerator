using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    public GameObject itemGameObject;
    public bool itemSelected;

    public void SpawnItem()
    {
        GameObject refObj = Instantiate(itemGameObject, Vector3.zero, itemGameObject.transform.rotation);
        FindObjectOfType<ItemManager>().OnItemPicked(refObj.GetComponent<ItemBehaviour>());
        refObj.GetComponent<ItemBehaviour>().OnDestroy += OnItemDestroy;
        this.GetComponent<Button>().interactable = false;
    }

    private void OnItemDestroy(ItemBehaviour refObj)
    {
        refObj.OnDestroy -= OnItemDestroy;
        Destroy(refObj.gameObject);
        this.GetComponent<Button>().interactable = true;
    }

}
