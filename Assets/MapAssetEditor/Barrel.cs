using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LaniakeaCode.Utilities;

public class Barrel : Entity
{
    [Range(0.1f, 2.0f)]
    public float size = 1;
    public string barrelname = "New Map Object";
    public Color color = Color.red;
    MaterialPropertyBlock mpb;
    static readonly int shPropColor = Shader.PropertyToID("_Color");
    
    [SerializeField]
    public BarrelTypeData typeData;
    

    
    void ApplyColor()
    {
        Debug.Log("Barrel Enabled");
    }
    
    private void Awake()
    {
        //GetComponent<SpriteRenderer>().sharedMaterial.color = this.color;
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
