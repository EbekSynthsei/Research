using LaniakeaCode.GraphSystem;
using LaniakeaCode.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Class responsible for loading CSV data into dialogue nodes and ports.
/// </summary>
public class LoadCSV
{
    private string csvDirectoryName = "Resources/DialogueGraphsCSV";
    private string csvFileName = "DialogueCSV_Load.csv";
    
    public List<string> GetAllCSVFiles()
    {
        string directoryPath = $"{Application.dataPath}/{csvDirectoryName}";
        if (Directory.Exists(directoryPath))
        {
            return Directory.GetFiles(directoryPath, "*.csv").Select(Path.GetFileName).ToList();
        }
        return new List<string>();
    }
    
    /// <summary>
    /// Loads the CSV data and updates the dialogue nodes and ports.
    /// </summary>
    /// <param name="selectedFileName">Optional selected file name.</param>
    public void Load(string selectedFileName = null)
    {
        // Use the provided file name or default to csvFileName
        string fileNameToUse = selectedFileName ?? csvFileName;

        // Parse the CSV file
        List<List<string>> result = ParseCSV(File.ReadAllText($"{Application.dataPath}/{ csvDirectoryName}/{fileNameToUse}"));

        // Get the headers from the CSV
        List<string> headers = result[0];

        // Find all graph trees
        List<GraphTree> graphTrees = GenericHelper.FindAllGraphs();

        // Iterate through each graph tree and update nodes and ports
        foreach(GraphTree tree in graphTrees)
        {
            foreach(DialogueNodeData nodeData in tree.dialogueNodeDatas)
            {
                LoadIntoNode(result, headers, nodeData);
                foreach(DialogueNodePort nodePort in nodeData.dialogueNodePorts)
                {
                    LoadIntoNodePort(result, headers, nodePort);
                }
            }
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }
    }

    /// <summary>
    /// Loads data into a dialogue node from the CSV.
    /// </summary>
    /// <param name="_result">Parsed CSV data.</param>
    /// <param name="_headers">CSV headers.</param>
    /// <param name="_nodeData">Dialogue node data to be updated.</param>
    private void LoadIntoNode(List<List<string>> _result, List<string> _headers, DialogueNodeData _nodeData)
    {
        foreach(List<string> line in _result)
        {
            if(line[0] == _nodeData.nodeGUID)
            {
                for (int i = 0; i < line.Count; i++)
                {
                    foreach(LanguageType languageType in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
                    {
                        if (_headers[i] == languageType.ToString())
                        {
                            _nodeData
                                .textBox_languages
                                .Find(text => text
                                .LanguageType == languageType)
                                .LanguageGenericType = line[i];
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Loads data into a dialogue node port from the CSV.
    /// </summary>
    /// <param name="_result">Parsed CSV data.</param>
    /// <param name="_headers">CSV headers.</param>
    /// <param name="_nodePort">Dialogue node port to be updated.</param>
    private void LoadIntoNodePort(List<List<string>> _result, List<string> _headers, DialogueNodePort _nodePort)
    {
        foreach (List<string> line in _result)
        {
            if (line[0] == _nodePort.PortGUID)
            {
                for (int i = 0; i < line.Count; i++)
                {
                    foreach (LanguageType languageType in (LanguageType[])Enum.GetValues(typeof(LanguageType)))
                    {
                        if (_headers[i] == languageType.ToString())
                        {
                            _nodePort
                                .ChoiceText_List
                                .Find(text => text
                                .LanguageType == languageType)
                                .LanguageGenericType = line[i];
                        }
                    }
                }
            }
        }
    }

    // flag for processing a CSV
    private enum ParsingMode
    {
        // default(treat as null)
        None,
        // processing a character which is out of quotes 
        OutQuote,
        // processing a character which is in quotes
        InQuote
    }

    /// <summary>
    /// Parses the CSV string.
    /// </summary>
    /// <returns>A two-dimensional list. First index indicates the row, second index indicates the column.</returns>
    /// <param name="src">Raw CSV contents as string.</param>
    public List<List<string>> ParseCSV(string src)
    {
        var rows = new List<List<string>>();
        var cols = new List<string>();
        var buffer = new StringBuilder();

        ParsingMode mode = ParsingMode.OutQuote;
        bool requireTrimLineHead = false;
        var isBlank = new Regex(@"\s");

        int len = src.Length;

        for (int i = 0; i < len; ++i)
        {
            char c = src[i];

            // remove whitespace at beginning of line
            if (requireTrimLineHead)
            {
                if (isBlank.IsMatch(c.ToString()))
                {
                    continue;
                }
                requireTrimLineHead = false;
            }

            // finalize when c is the last character
            if ((i + 1) == len)
            {
                // final char
                switch (mode)
                {
                    case ParsingMode.InQuote:
                        if (c == '"')
                        {
                            // ignore
                        }
                        else
                        {
                            // if close quote is missing
                            buffer.Append(c);
                        }
                        cols.Add(buffer.ToString());
                        rows.Add(cols);
                        return rows;

                    case ParsingMode.OutQuote:
                        if (c == ',')
                        {
                            // if the final character is comma, add an empty cell
                            // next col
                            cols.Add(buffer.ToString());
                            cols.Add(string.Empty);
                            rows.Add(cols);
                            return rows;
                        }
                        if (cols.Count == 0)
                        {
                            // if the final line is empty, ignore it. 
                            if (string.Empty.Equals(c.ToString().Trim()))
                            {
                                return rows;
                            }
                        }
                        buffer.Append(c);
                        cols.Add(buffer.ToString());
                        rows.Add(cols);
                        return rows;
                }
            }

            // the next character
            char n = src[i + 1];

            switch (mode)
            {
                case ParsingMode.OutQuote:
                    // out quote
                    if (c == '"')
                    {
                        // to in-quote
                        mode = ParsingMode.InQuote;
                        continue;

                    }
                    else if (c == ',')
                    {
                        // next cell
                        cols.Add(buffer.ToString());
                        buffer.Remove(0, buffer.Length);

                    }
                    else if (c == '\r' && n == '\n')
                    {
                        // new line(CR+LF)
                        cols.Add(buffer.ToString());
                        rows.Add(cols);
                        cols = new List<string>();
                        buffer.Remove(0, buffer.Length);
                        ++i; // skip next code
                        requireTrimLineHead = true;

                    }
                    else if (c == '\n' || c == '\r')
                    {
                        // new line
                        cols.Add(buffer.ToString());
                        rows.Add(cols);
                        cols = new List<string>();
                        buffer.Remove(0, buffer.Length);
                        requireTrimLineHead = true;

                    }
                    else
                    {
                        // get one char
                        buffer.Append(c);
                    }
                    break;

                case ParsingMode.InQuote:
                    // in quote
                    if (c == '"' && n != '"')
                    {
                        // to out-quote
                        mode = ParsingMode.OutQuote;

                    }
                    else if (c == '"' && n == '"')
                    {
                        // get "
                        buffer.Append('"');
                        ++i;

                    }
                    else
                    {
                        // get one char
                        buffer.Append(c);
                    }
                    break;
            }
        }
        return rows;
    }
}