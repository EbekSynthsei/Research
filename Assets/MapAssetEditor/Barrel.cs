using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Utilities;
public class Barrel : MonoBehaviour
{
    [Range(0.1f, 2.0f)]
    public float size = 1;
    public string barrelname = "New Map Object";
    public Color color = Color.red;
    MaterialPropertyBlock mpb;
    static readonly int shPropColor = Shader.PropertyToID("_Color");
    public MaterialPropertyBlock Mpb
    {

        get
        {
            if (mpb == null)
            {
                mpb = new MaterialPropertyBlock();
            }
            return mpb;
        }
    }
    
    [SerializeField]
    public List<InteractableBase> interactableBases;
    
    void ApplyColor()
    {
        SpriteRenderer rnd = GetComponent<SpriteRenderer>();
        Mpb.SetColor(shPropColor, color);
        rnd.SetPropertyBlock(Mpb);
    }
    
    public string Interaction = "Nuova Interazione";
    private void Awake()
    {
        GetComponent<SpriteRenderer>().sharedMaterial.color = this.color;
    }
    void OnEnable()
    {
        ApplyColor();
        BarrelManager.barrels.Add(this);
    }
    void OnDisable() => BarrelManager.barrels.Remove(this);

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, size);
    }
}
