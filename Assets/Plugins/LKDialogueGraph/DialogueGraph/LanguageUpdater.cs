using LaniakeaCode.GraphSystem;
using LaniakeaCode.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for updating the language data in dialogue nodes.
/// </summary>
public class LanguageUpdater
{
    /// <summary>
    /// Updates the language data for all dialogue nodes in all graph trees.
    /// </summary>
    public void UpdateLanguage()
    {
        List<GraphTree> graphTrees = GenericHelper.FindAllGraphs();
        foreach(GraphTree graph in graphTrees)
        {
            foreach(DialogueNodeData dialogueNodeData in graph.dialogueNodeDatas)
            {
                // Update the language data for text boxes and audio clips
                dialogueNodeData.textBox_languages = UpdateLanguageGeneric(dialogueNodeData.textBox_languages);
                dialogueNodeData.audioClips_List = UpdateLanguageGeneric(dialogueNodeData.audioClips_List);
                
                // Update the language data for choice texts in dialogue node ports
                foreach(DialogueNodePort dialogueNodePort in dialogueNodeData.dialogueNodePorts)
                {
                    dialogueNodePort.ChoiceText_List = UpdateLanguageGeneric(dialogueNodePort.ChoiceText_List);
                }
            }
        }
    }

    /// <summary>
    /// Updates the language data for a given list of language generics.
    /// </summary>
    /// <typeparam name="T">The type of the language generic.</typeparam>
    /// <param name="languageGenerics">The list of language generics to update.</param>
    /// <returns>A new list of updated language generics.</returns>
    private List<LanguageGeneric<T>> UpdateLanguageGeneric<T>(List<LanguageGeneric<T>> languageGenerics)
    {
        List<LanguageGeneric<T>> tmp = new List<LanguageGeneric<T>>();

        // Initialize the list with all possible language types
        foreach(LanguageType languageType in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
        {
            tmp.Add(new LanguageGeneric<T>{
                LanguageType = languageType
            });
        }

        // Update the language generics with existing data
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
