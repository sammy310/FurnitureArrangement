using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookMarkUI : MonoBehaviour
{
    public Data_FurnitureInfo furnitureInfo;
    Data_Furniture furnitureInfos = null;

    public Slot[] slots;
    public Transform slotHolder;

    private void Awake() {
        furnitureInfos = PrefabManager.Instance.furnitureInfos;
        slots = slotHolder.GetComponentsInChildren<Slot>();
    }

    void Start()
    {
        RedrawSlotUI();
    }

/*CSL
    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //GameObject slotBtnTrans = slots[i].transform.Find("SlotBtn").gameObject;

            //10 -> slotCount
            if (i < 10)
                slots[i].gameObject.SetActive(true);
            //slotBtnTrans.GetComponent<Button>().interactable = true;

            else
                slots[i].gameObject.SetActive(false);
            //slotBtnTrans.GetComponent<Button>().interactable = false;
        }
    }
*/


    public void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }

        int slotIndex = 0;
        
        for (int i = 0; i < furnitureInfos.furnitureInfo.Length; i++)
        {
            if (furnitureInfos.furnitureInfo[i].bookMark)
            {
                slots[slotIndex].gameObject.SetActive(true);
                slots[slotIndex].furnitureInfo = furnitureInfos.furnitureInfo[i];
                slots[slotIndex].UpdateSlotUI();

                slotIndex++;
            }
        }

        for(int i = slotIndex;i<10;i++)
        {
            slots[i].gameObject.SetActive(false);
        }
    }
}
