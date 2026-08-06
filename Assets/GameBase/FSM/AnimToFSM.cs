using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimToFSM : MonoBehaviour
{
    public State thisState;
    private void AnimationTrigger()
    {
        thisState.AnimationTrigger();
    }

    private void AnimationFinishTrigger() { thisState.AnimationFinishTrigger(); }
    
    // Renamed method to avoid conflict with AnimToWeapon
    private void FSMAnimationFinishTrigger() { thisState.AnimationFinishTrigger(); }
}
