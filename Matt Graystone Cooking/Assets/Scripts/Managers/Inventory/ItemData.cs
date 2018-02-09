using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Item Item;
    public int count;
    private Vector2 offSet;
    public int slot;

    private ToolTip toolTip;

    public void SetBorderImage(Sprite imageToSet)
    {
    }

    private void Start()
    {
        toolTip = Inventory.Instance.GetComponent<ToolTip>();
    }

    public void Update()
    {
        if(Inventory.Instance.Split_Stack_Prefab.activeInHierarchy == true)
        {
            Inventory.Instance.Split_Stack_Prefab.GetComponent<SplitStack>().OnValueChanged(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            Inventory.Instance.HideSplitStack(this);

            offSet = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(GameObject.Find("Item_Holder").transform);// this.transform.parent.parent);
            this.transform.position = eventData.position; //+offset
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            Cursor.visible = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            this.transform.position = eventData.position; //+offset
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            this.transform.SetParent(Inventory.Instance.slots[slot].transform);
            this.transform.position = Inventory.Instance.slots[slot].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        Cursor.visible = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTip.Activate(Item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //toolTip.DeActivate();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Item != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                Inventory.Instance.HideSplitStack(this);
            else if (eventData.button == PointerEventData.InputButton.Right)
                Inventory.Instance.ShowSplitStack(this, this.transform.parent.gameObject);
        }
    }
}
