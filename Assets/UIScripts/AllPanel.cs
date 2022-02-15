using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllPanel : MonoBehaviour
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
