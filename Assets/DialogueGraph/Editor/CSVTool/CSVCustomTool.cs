using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class CSVCustomTool
{

    [MenuItem("/LaniakeaTools/SaveGraphToCSV")]
    public static void SaveToCSV()
    {
        SaveCSV saveCSV = new SaveCSV();
        saveCSV.Save();
        Debug.Log("<color=green> Saved CSV!</color>");
    }

    [MenuItem("/LaniakeaTools/UpdateGraphLanguage")]
    public static void UpdateGraphLanguage()
    {
        LanguageUpdater languageUpdater = new LanguageUpdater();
        languageUpdater.UpdateLanguage();

        Debug.Log("<color=green> Updated Language!</color>");
    }
}
