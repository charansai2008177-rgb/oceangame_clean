using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.InteropServices.WindowsRuntime;


public class slots : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public bool hovering;
    private itemso helditem;
    private int itemamount;
    private Image iconimage;
    private TextMeshProUGUI amountTxt;

    private void Awake()
    {
        iconimage = transform.GetChild(0).GetComponent<Image>();
        amountTxt = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        // No need to set icon here; Updateslot() will handle it.
    }

    public itemso GetItem()
    {
        return helditem;
    }
    public int GetAmount()
    {
        return itemamount;
    }
    public void SetItem(itemso item, int amount = 1)
    {
        helditem = item;
        itemamount = amount;

        Updateslot();
    }


    public void Updateslot()
    {
        if (helditem != null)
        {
            iconimage.enabled = true;
            iconimage.sprite = helditem.icon;
            amountTxt.text = itemamount.ToString();

        }
        else
        {
            iconimage.enabled = false;
            amountTxt.text = "";
        }
    }
    public int AddAmount(int amountToAdd)
    {
        itemamount += amountToAdd;
        Updateslot();
        return itemamount;
    }
    public int RemoveAmount(int amountToRemove)
    {
        itemamount -= amountToRemove;
        if (itemamount <= 0)
        {
            Clearslot();
        }
        else
        {
            Updateslot();
        }
        return itemamount;

    }
    public void Clearslot()
    {
        helditem = null;
        itemamount = 0;
        Updateslot();

    }
    public bool hasItem()
    {
        return helditem != null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
}


       



