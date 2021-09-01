using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public bool itemPicked = false;
    private ItemBehaviour currentItem = null;
    public EasyTween itemPanelTween;
    public GameObject openButton;
    public AudioSource menuAudioSource;
    public AudioClip onItemPick;
    public AudioClip onItemPlace;

    public void OnItemPicked(ItemBehaviour item, bool fromMenu = true)
    {
        if (itemPicked)
            return;
        itemPicked = true;
        currentItem = item;
        currentItem.OnPickUpItem();
        menuAudioSource.PlayOneShot(onItemPick);
        if (fromMenu)
        {
            itemPanelTween?.OpenCloseObjectAnimation();
            openButton?.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && itemPicked)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100f);

            if (hit.collider != null && hit.collider.tag == "GameArea")
            {
                Debug.Log(hit.collider);
                currentItem.OnItemPlaced(hit.point);
                menuAudioSource.PlayOneShot(onItemPlace);
                itemPicked = false;
            }
        }

        if (Input.GetMouseButtonDown(1) && itemPicked)
        {
            currentItem.OnItemDestroy();
            itemPicked = false;
        }
    }
}
