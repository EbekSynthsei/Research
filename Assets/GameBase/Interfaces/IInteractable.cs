namespace LaniakeaCode.Utilities
{
    public interface IInteractable
    {
        float HoldDuration { get; }
        bool HoldInteract { get; }
        bool MultipleUse { get; }
        bool IsInteractable { get; }

        void OnInteract(Entity interactor);
        void OnFocus(Entity interactor);
        void OnLoseFocus(Entity interactor);
    }
}