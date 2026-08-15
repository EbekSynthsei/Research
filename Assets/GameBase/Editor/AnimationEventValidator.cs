#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

/// <summary>
/// Menu tool per verificare che le clip di attacco/abilità contengano
/// gli Animation Event richiesti dal pattern FSM (AnimToFSM/AnimToWeapon).
/// Uso: Tools > Validate Animation Events, selezionando un AnimatorController.
/// </summary>
public static class AnimationEventValidator
{
    private static readonly string[] RequiredEndEvents = { "AnimationFinishTrigger" };

    [MenuItem("Tools/Validate Animation Events On Selected Controller")]
    private static void Validate()
    {
        var controller = Selection.activeObject as AnimatorController;
        if (controller == null)
        {
            Debug.LogWarning("Seleziona un AnimatorController nel Project window prima di eseguire questo tool.");
            return;
        }

        var clips = controller.animationClips.Distinct();
        int missing = 0;

        foreach (var clip in clips)
        {
            var events = AnimationUtility.GetAnimationEvents(clip);
            var eventNames = events.Select(e => e.functionName).ToHashSet();

            foreach (var required in RequiredEndEvents)
            {
                if (clip.isLooping)
                {
                    continue; // idle/move looping non necessitano di FinishTrigger
                }

                if (!eventNames.Contains(required))
                {
                    Debug.LogWarning($"[AnimationEventValidator] Clip '{clip.name}' non ha l'event '{required}'. " +
                        "Se è una clip di stato non-looping (attacco, dash, jump), potrebbe bloccare la FSM.", clip);
                    missing++;
                }
            }
        }

        Debug.Log(missing == 0
            ? $"Tutte le {clips.Count()} clip verificate hanno gli event richiesti."
            : $"Trovate {missing} clip con event mancanti su {clips.Count()} totali.");
    }
}
#endif