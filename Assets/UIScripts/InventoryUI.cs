using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Data_Furniture furnitureInfos = null;

    public Slot[] slots;
    public Transform slotHolder;

    private void Awake() {
        slots = slotHolder.GetComponentsInChildren<Slot>();

        // CSL 
        // 10 == SlotCount
        furnitureInfos = PrefabManager.Instance.furnitureInfos;
        for (int i = 0; i < 10; i++)
        {
            if(i<furnitureInfos.furnitureInfo.Length)
            {
                slots[i].furnitureInfo = furnitureInfos.furnitureInfo[i];
                slots[i].UpdateSlotUI();
            }
            else
                slots[i].gameObject.SetActive(false);
        }
        /*
        
        inven.onSlotCountChange += SlotChange;

        for (int i = 0; i < inven.SlotCnt; i++)
        {
            if(i<inven.furnitures.Count)
            {
                slots[i].furniture = inven.furnitures[i];
                slots[i].UpdateSlotUI();
            }
            else
            slots[i].gameObject.SetActive(false);
        }
        */
    }

    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }



    /* CSL
    //나중에 가구를 임의로 추가할때,,,?
    private void SlotChange(int val)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            //GameObject slotBtnTrans = slots[i].transform.Find("SlotBtn").gameObject;
            if (i < inven.SlotCnt)
                slots[i].gameObject.SetActive(true);
            //slotBtnTrans.GetComponent<Button>().interactable = true;

            else
                slots[i].gameObject.SetActive(false);
            //slotBtnTrans.GetComponent<Button>().interactable = false;
        }
    }
    public void AddSlot()
    {
        inven.SlotCnt++;
    }
    */

    public void InitSlotUI()
    {
        this.furnitureInfos = furnitureInfos;
    }
    //CSL
    //Draw All Furniture UI
    public void RedrawSlotUI()
    {
        for(int i = 0;i<slots.Length;i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i = 0; i<furnitureInfos.furnitureInfo.Length;i++)
        {
            slots[i].furnitureInfo = furnitureInfos.furnitureInfo[i];
            slots[i].UpdateSlotUI();
        }
    }
}
