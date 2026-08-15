using UnityEngine;
using LaniakeaCode.Utilities;

namespace LaniakeaCode.Utilities
{
    [CreateAssetMenu(fileName = "Activate Action", menuName = "LaniakeaTools/Actions/Activate")]
    public class ActivateAction : ScriptableAction
    {
        public override void PerformAction(GameObject agent, GameObject subject)
        {
            if (subject.TryGetComponent<IActivatable>(out var activatable) &&
                agent.TryGetComponent<Entity>(out var entity))
            {
                activatable.Activate(entity);
            }
            else
            {
                Debug.LogWarning($"ActivateAction: subject '{subject.name}' non implementa IActivatable " +
                    $"o agent '{agent.name}' non ha componente Entity.", subject);
            }
        }
    }
}