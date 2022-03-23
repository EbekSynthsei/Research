using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using LaniakeaCode.GraphSystem;
using LaniakeaCode.Utilities;
public class SaveCSV
{
    private string csvDirectoryName = "Resources/DialogueGraphsCSV";
    private string csvFileName = "DialogueCSV_Save.csv";
    private string csvSeparator = ",";
    private List<string> csvHeader;
    private string idName = "Guid_ID";
    public void Save()
    {
        List<GraphTree> graphItems = GenericHelper.FindAllObjectsFromResources<GraphTree>();
        
        CreateFile();
        foreach (GraphTree item in graphItems)
        {

            foreach (DialogueNodeData dialogueNodeData in item.dialogueNodeDatas)
            {

                List<string> texts = new List<string>();
                texts.Add(dialogueNodeData.nodeGUID);
                foreach (LanguageType languageType in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
                {
                    string tmp = dialogueNodeData
                        .textBox_languages
                        .Find(language => language
                        .LanguageType == languageType)
                        .LanguageGenericType
                        .Replace("\"", "\"\"");                        ;
                    
                    texts.Add($"\"{tmp}\"");
                }

                foreach (DialogueNodePort nodePort in dialogueNodeData.dialogueNodePorts)
                {
                    
                    texts.Add(nodePort.PortGUID);
                    foreach (LanguageType languageType in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
                    {
                        string tmp = nodePort
                            .ChoiceText_List
                            .Find(language => language
                            .LanguageType == languageType)
                            .LanguageGenericType
                            .Replace("\"", "\"\"");
                        
                        texts.Add($"\"{tmp}\"");
                    }
                }
                AppendToFile(texts);
            }
        }
    }
    private void AppendToFile(List<string> _strings)
    {
        using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string finalString = "";
            foreach (string text in _strings)
            {
                if (text != null)
                {
                    finalString += text + csvSeparator;
                }
            }
            sw.WriteLine(finalString);
        }
    }
    private void CreateFile()
    {
        VerifyDirectory();
        MakeHeader();
        using (StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finalString = "";
            foreach (string header in csvHeader)
            {
                finalString += header;
                if (finalString != null)
                {
                    finalString += csvSeparator;
                }
            }
            sw.WriteLine(finalString);
        }
    }
    private void MakeHeader()
    {
        List<string> headerText = new List<string>();
        headerText.Add(idName);
        foreach (LanguageType language in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
        {
            headerText.Add(language.ToString());
        }
        csvHeader = headerText;
    }
    private void VerifyDirectory()
    {
        string directory = GetDirectoryPath();
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
    private string GetDirectoryPath()
    {
        return $"{(Application.dataPath)}/{csvDirectoryName}";
    }
    private string GetFilePath()
    {
        return $"{GetDirectoryPath()}/{(csvFileName)}";
    }

}
