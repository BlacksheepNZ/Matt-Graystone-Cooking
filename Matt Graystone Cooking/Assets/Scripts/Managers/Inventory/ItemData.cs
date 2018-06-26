using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// </summary>
public class ItemData : MonoBehaviour, 
                        IBeginDragHandler, 
                        IDragHandler, 
                        IEndDragHandler, 
                        IPointerEnterHandler, 
                        IPointerExitHandler, 
                        IPointerClickHandler
{
    /// <summary>
    /// 
    /// </summary>
    //[HideInInspector]
    public Item Item;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int count;

    /// <summary>
    /// 
    /// </summary>
    [HideInInspector]
    public int slot;

    /// <summary>
    /// 
    /// </summary>
    private Vector2 offSet;

    /// <summary>
    /// 
    /// </summary>
    private ToolTip toolTip;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {
        if (Item != null)
        {
            toolTip = Inventory.Instance.GetComponent<ToolTip>();
            Item.GetAttachedRecipes();
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        if(Inventory.Instance.GUI_Panel_Split_Stack.activeInHierarchy == true)
        {
            Inventory.Instance.GUI_Panel_Split_Stack.GetComponent<SplitStack>().OnValueChanged(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            Inventory.Instance.HideSplitStack();

            offSet = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            this.transform.SetParent(GameObject.Find("GUI_Item_Holder").transform);// this.transform.parent.parent);
            this.transform.position = eventData.position; //+offset
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            Cursor.visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            this.transform.position = eventData.position; //+offset
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            this.transform.SetParent(Inventory.Instance.slots[slot].transform);
            this.transform.position = Inventory.Instance.slots[slot].transform.position;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        //Crafting_DragDrop.Instance.CheckSlot();

        Cursor.visible = true;
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        Inventory.Instance.ActivateToolTip(this);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.Instance.DeActivateToolTip();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Item != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                Inventory.Instance.HideSplitStack();
            else if (eventData.button == PointerEventData.InputButton.Right)
                Inventory.Instance.ShowSplitStack(this, this.transform.parent.gameObject);
        }
    }
}
