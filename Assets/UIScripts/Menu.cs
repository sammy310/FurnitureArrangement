using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu instance;

    public GameObject allPanel;
    public GameObject bookMarkPanel;
    public GameObject btnPanel;
    public GameObject infoPanel;

    void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        btnPanel.SetActive(true);

        allPanel.SetActive(true);
        bookMarkPanel.SetActive(true);

        allPanel.SetActive(false);
        bookMarkPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void AllBtnClicked()
    {
        allPanel.SetActive(true);
        bookMarkPanel.SetActive(false);
        infoPanel.SetActive(false);

        allPanel.GetComponent<AllPanel>().RedrawSlotUI();
    }

    public void BookMarkBtnClicked()
    {
        allPanel.SetActive(false);
        bookMarkPanel.SetActive(true);
        infoPanel.SetActive(false);

        bookMarkPanel.GetComponent<BookMarkPanel>().RedrawSlotUI();
    }

    public void HomeBtnClicked()
    {
        allPanel.SetActive(false);
        bookMarkPanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void InfoBtnClicked(int num)
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponent<InfoPanel>().UpdateInfo(num);
    }

    public void RedrawAllPanel()
    {
        allPanel.GetComponent<AllPanel>().RedrawSlotUI();
    }

    public void RedrawBookMarkPanel()
    {
        bookMarkPanel.GetComponent<BookMarkPanel>().RedrawSlotUI();
    }


}
