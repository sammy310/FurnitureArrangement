using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GetPicture : MonoBehaviour
{

    // ������ ����� ��θ� ������ �� �ʿ�
#if !UNITY_EDITOR && UNITY_ANDROID
    private static AndroidJavaClass m_ajc = null;
    private static AndroidJavaClass AJC
    {
        get
        {
            if (m_ajc == null)
                m_ajc = new AndroidJavaClass("com.yasirkula.unity.NativeGallery");
 
            return m_ajc;
        }
    }
#endif

    // �ٸ� ��ũ��Ʈ���� GetPicture.GetLastPicturePath()�� ȣ��
    public static string GetLastPicturePath()
    {
        // ����̽����� �ٸ� ������
        string saveDir;
#if !UNITY_EDITOR && UNITY_ANDROID
        saveDir = AJC.CallStatic<string>( "GetMediaPath", "Shine Bright" );
#else
        saveDir = Application.persistentDataPath;
#endif
        // �����ο��� PNG���� ��� �˻�
        string[] files = Directory.GetFiles(saveDir, "*.png");
        // ���� PNG������ �ִٸ�, ������ ������ ��ȯ
        if (files.Length > 0)
        {
            return files[files.Length - 1];
        }
        // ���ٸ� null�� 
        return null;
    }


}