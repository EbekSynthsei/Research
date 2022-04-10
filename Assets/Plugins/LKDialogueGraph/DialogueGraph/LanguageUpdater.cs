using LaniakeaCode.GraphSystem;
using LaniakeaCode.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageUpdater
{
    public void UpdateLanguage()
    {
        List<GraphTree> graphTrees = GenericHelper.FindAllGraphs();
        foreach(GraphTree graph in graphTrees)
        {
            foreach(DialogueNodeData dialogueNodeData in graph.dialogueNodeDatas)
            {
                dialogueNodeData.textBox_languages = UpdateLanguageGeneric(dialogueNodeData.textBox_languages);
                dialogueNodeData.audioClips_List = UpdateLanguageGeneric(dialogueNodeData.audioClips_List);
                
                foreach(DialogueNodePort dialogueNodePort in dialogueNodeData.dialogueNodePorts)
                {
                    dialogueNodePort.ChoiceText_List = UpdateLanguageGeneric(dialogueNodePort.ChoiceText_List);
                }
            }
        }
    }

    private List<LanguageGeneric<T>> UpdateLanguageGeneric<T>(List<LanguageGeneric<T>> languageGenerics)
    {
        List<LanguageGeneric<T>> tmp = new List<LanguageGeneric<T>>();

        foreach(LanguageType languageType in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
        {
            tmp.Add(new LanguageGeneric<T>{
                LanguageType = languageType
            });
        }
        foreach(LanguageGeneric<T> languageGeneric in languageGenerics)
        {
            if(tmp.Find(language => language.LanguageType == languageGeneric.LanguageType) != null)
            {
                tmp.Find(language => language
                .LanguageType == languageGeneric
                .LanguageType)
                    .LanguageGenericType = languageGeneric
                    .LanguageGenericType;
            }
        }
        return tmp;
    }
}
