using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManager
{
    public const string SaveFileName_Bookmark = "/bookmark.fa";


    static string GetSavePath(string filename)
    {
        return Application.persistentDataPath + filename;
    }

    public static void SaveBookmarkData(Dictionary<string, UI_FurnitureSelectItem> bookmark)
    {
        if (bookmark == null) return;

        string savePath = GetSavePath(SaveFileName_Bookmark);

        if (bookmark.Count == 0)
        {
            if (File.Exists(savePath))
                File.Delete(savePath);
        }
        else
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Create);

            List<string> bookmarkData = new List<string>();
            foreach (var dic in bookmark)
            {
                bookmarkData.Add(dic.Key);
            }
            formatter.Serialize(stream, bookmarkData);
            stream.Close();
        }
    }

    public static List<string> LoadBookmarkData()
    {
        string savePath = GetSavePath(SaveFileName_Bookmark);
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(savePath, FileMode.Open);

            List<string> bookmarkData = formatter.Deserialize(stream) as List<string>;
            stream.Close();

            return bookmarkData;
        }

        return null;
    }


}
