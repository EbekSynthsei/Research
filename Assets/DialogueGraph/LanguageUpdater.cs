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
        List<GraphTree> graphTrees = GenericHelper.FindAllObjectsFromResources<GraphTree>();
        foreach(GraphTree graph in graphTrees)
        {
            foreach(DialogueNodeData uINodeData in graph.dialogueNodeDatas)
            {
                uINodeData.textBox_languages = UpdateLanguageGeneric(uINodeData.textBox_languages);
                uINodeData.audioClips_List = UpdateLanguageGeneric(uINodeData.audioClips_List);
                
                foreach(DialogueNodePort uINodePort in uINodeData.dialogueNodePorts)
                {
                    uINodePort.ChoiceText_List = UpdateLanguageGeneric(uINodePort.ChoiceText_List);
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
