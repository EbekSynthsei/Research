    using LaniakeaCode.Events;
using UnityEngine.Events;

namespace LaniakeaCode.GraphSystem
{/// <summary>
    /// Componente da agganciare a porte, inventario, spawner, ecc. 
    /// per reagire a un GraphEvent raggiunto durante il percorso di un dialogo.
    /// Configurazione zero-codice tramite UnityEvent nell'Inspector.
    /// </summary>
    public class GraphEventListener : BaseGameEventListener<bool, GraphEvent, GraphUnityEvent>
    {
    }
}