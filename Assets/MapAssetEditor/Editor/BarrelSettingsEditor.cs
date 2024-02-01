using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class BarrelSettingsEditor : EditorWindow
{
    static EntityType entityTypeSettings;
    static BarrelSettingsEditor window;
    static BarrelEntityEditor editor;
    public static void OpenWindow(BarrelEntityEditor editor, EntityType setting)
    {
        entityTypeSettings = setting;
        window = (BarrelSettingsEditor)GetWindow(typeof(BarrelSettingsEditor));
        window.minSize = new Vector2(480, 270);
        window.Show();
    }
}
