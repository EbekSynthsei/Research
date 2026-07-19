namespace LaniakeaCode.GraphSystem
{
    /// <summary>
    /// Contratto minimo per un evento raggiungibile durante il percorso di un 
    /// grafo di dialogo. Il plugin non conosce l'implementazione concreta — 
    /// il progetto host la fornisce, mantenendo GraphSystem portabile.
    /// </summary>
    public interface IRaisable
    {
        void Raise();
    }
}