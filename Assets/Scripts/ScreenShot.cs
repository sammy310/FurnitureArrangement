using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{
    //public GameObject blink;             // ���� ���� �� ������ ��
    //public GameObject shareButtons;      // ���� ��ư
    public GameObject UIobject;
    public GameObject flashObect;
    bool isCoroutinePlaying;             // �ڷ�ƾ �ߺ�����

    // ���� �ҷ��� �� �ʿ�
    string albumName = "Test";           // ������ �ٹ��� �̸�
    //[SerializeField]
    //GameObject panel;                    // ���� ������ �� �г�


    // ĸ�� ��ư�� ������ ȣ��
    public void Capture_Button()
    {
        // �ߺ����� bool
        if (!isCoroutinePlaying)
        {
            StartCoroutine("captureScreenshot");
        }
    }

    IEnumerator captureScreenshot()
    {
        isCoroutinePlaying = true;
        flashObect.SetActive(true);
        AnimManager.instance.StartFlash();
        // UI ���ش�...
        UIobject.SetActive(false);
        yield return new WaitForEndOfFrame();

        // ��ũ���� + ����������
        ScreenshotAndGallery();

        yield return new WaitForEndOfFrame();
        // ��ũ
        //BlinkUI();

        // ���� ���� �ֱ�...

        yield return new WaitForEndOfFrame();
        UIobject.SetActive(true);
        // UI �ٽ� ���´�...

        yield return new WaitForSecondsRealtime(0.3f);
        flashObect.SetActive(false);
        // ���� ������ ����
        //GetPirctureAndShowIt();
        isCoroutinePlaying = false;
    }

    // ��� ��ũ ����
    //void BlinkUI()
    //{
    //    GameObject b = Instantiate(blink);
    //    b.transform.SetParent(transform);
    //    b.transform.localPosition = new Vector3(0, 0, 0);
    //    b.transform.localScale = new Vector3(1, 1, 1);
    //}

    // ��ũ���� ��� �������� ����
    void ScreenshotAndGallery()
    {
        // ��ũ����
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        // ����������
        Debug.Log("" + NativeGallery.SaveImageToGallery(ss, albumName,
            "Screenshot_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + "{0}.png"));

        // To avoid memory leaks.
        // ���� �Ϸ�Ʊ� ������ ���� �޸� ����
        Destroy(ss);

    }
    // ���� ������ Panel�� �����ش�.
    //void GetPirctureAndShowIt()
    //{
    //    string pathToFile = GetPicture.GetLastPicturePath();
    //    if (pathToFile == null)
    //    {
    //        return;
    //    }
    //    Texture2D texture = GetScreenshotImage(pathToFile);
    //    Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    //    panel.SetActive(true);
    //    shareButtons.SetActive(true);
    //    panel.GetComponent<Image>().sprite = sp;
    //}
    // ���� ������ �ҷ��´�.
    Texture2D GetScreenshotImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
    }
}