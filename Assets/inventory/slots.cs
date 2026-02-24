using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Unity.VisualScripting;
using JetBrains.Annotations;


public class slots : MonoBehaviour
{
    public bool hovering;
    private itemso helditem;
    private int itemamount;
    private Image iconimage;
    private TextMeshProUGUI amounttext;

    private void Awake()
    {
        iconimage = transform.GetChild(0).GetComponent<Image>();
        amounttext = transform.GetChild(1).GetComponent<TextMeshProUGUI>();

    }

    public itemso GetItem() { return helditem;
    }
    public int GetAmount() { return itemamount;
    }
    public void SetItem(itemso item, int amount = 1)
    {
        helditem = item;
        itemamount = amount;

        Updateslot();


            public void Updateslot()
    {
        if (helditem != null)
        {
            iconimage.sprite = helditem.icon;
            iconimage.enabled = true;
            if (itemamount > 1)
            {
                amounttext.text = itemamount.ToString();
                amounttext.enabled = true;
            }
            else
            {
                amounttext.enabled = false;
            }
        }
        else
        {
            iconimage.sprite = null;
            iconimage.enabled = false;
            amounttext.enabled = false;
        }
    }


}
    }


       



