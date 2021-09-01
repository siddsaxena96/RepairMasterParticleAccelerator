using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBehaviour : MonoBehaviour
{
    public bool itemPicked = true;
    public Action<ItemBehaviour> OnDestroy;

    private void Update()
    {
        if (itemPicked == true)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            transform.position = new Vector3(pos.x + 0.75f, pos.y + 0.75f, 3f);

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (transform.rotation.eulerAngles.z < 90 || transform.rotation.eulerAngles.z >= 270)
                    transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 15);
                else
                    transform.rotation = Quaternion.Euler(0, 0, -90);
                if (transform.rotation.eulerAngles.z == 360)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && !itemPicked)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100f);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    FindObjectOfType<ItemManager>().OnItemPicked(this, false);
                }
            }
        }
    }

    public void OnPickUpItem()
    {
        itemPicked = true;
        foreach (Collider2D temp in GetComponents<Collider2D>())
        {
            temp.enabled = false;
        }
    }

    public void OnItemPlaced(Vector3 position)
    {
        itemPicked = false;
        transform.position = new Vector3(position.x + 0.75f, position.y + 0.75f, 1f);
        foreach (Collider2D temp in GetComponents<Collider2D>())
        {
            temp.enabled = true;
        }
    }

    public void OnItemDestroy()
    {
        itemPicked = false;
        OnDestroy?.Invoke(this);
    }
}
