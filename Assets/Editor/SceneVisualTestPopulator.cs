using System;
using UnityEditor;
using UnityEngine;
using Unity.Cinemachine;

/// <summary>
/// LaniakeaCode.Editor.SceneVisualTestPopulator
/// Editor tool per generare un ambiente placeholder "Last Night style" (2.5D)
/// SENZA toccare la gerarchia esistente della scena.
/// Crea tutto sotto un root dedicato "VisualTest_DoNotShip", facilmente
/// eliminabile/disattivabile prima del commit.
///
/// USO:
/// 1. Salva questo file in Assets/Editor/SceneVisualTestPopulator.cs
/// 2. Apri la scena TwoDotFiveSceneBase
/// 3. Menu: Tools/Laniakea/Build Visual Test Environment
/// 4. Per rimuovere: Tools/Laniakea/Clear Visual Test Environment
/// </summary>
public static class SceneVisualTestPopulator
{
    private const string RootName = "VisualTest_DoNotShip";

    // Offset per non collidere con la gerarchia esistente (player/camera sono ~ y:117-155)
    private static readonly Vector3 BaseOrigin = new Vector3(0f, -1f, 0f);

    [MenuItem("Tools/Laniakea/Build Visual Test Environment")]
    public static void BuildTestEnvironment()
    {
        ClearTestEnvironment(); // idempotente

        GameObject root = new GameObject(RootName);
        root.transform.position = Vector3.zero;

        BuildLighting(root.transform);
        BuildGroundPlane(root.transform);
        BuildMidgroundBuildings(root.transform);
        BuildBackgroundSkyline(root.transform);
        BuildForegroundProps(root.transform);
        BuildTestCinemachineVCam(root.transform);

        Selection.activeGameObject = root;
        Debug.Log("[SceneVisualTestPopulator] Ambiente di test creato sotto '" + RootName + "'.");
    }

    [MenuItem("Tools/Laniakea/Clear Visual Test Environment")]
    public static void ClearTestEnvironment()
    {
        GameObject existing = GameObject.Find(RootName);
        if (existing != null)
        {
            UnityEngine.Object.DestroyImmediate(existing);
            Debug.Log("[SceneVisualTestPopulator] Ambiente di test rimosso.");
        }
    }

    // ---------------------------------------------------------------
    // LIGHTING
    // ---------------------------------------------------------------
    private static void BuildLighting(Transform parent)
    {
        GameObject lightGroup = new GameObject("Lighting_Test");
        lightGroup.transform.SetParent(parent);

        // Sole low-angle, tipico taglio "golden hour" cinematico stile The Last Night
        GameObject sun = new GameObject("Sun_KeyLight");
        sun.transform.SetParent(lightGroup.transform);
        sun.transform.position = BaseOrigin + new Vector3(0, 20f, -10f);
        sun.transform.rotation = Quaternion.Euler(35f, -130f, 0f);
        Light sunLight = sun.AddComponent<Light>();
        sunLight.type = LightType.Directional;
        sunLight.color = new Color(1f, 0.85f, 0.65f); // caldo
        sunLight.intensity = 1.4f;
        sunLight.shadows = LightShadows.Soft;

        // Rim/fill fredda per contrasto cromatico (arancio vs ciano)
        GameObject rim = new GameObject("Rim_FillLight");
        rim.transform.SetParent(lightGroup.transform);
        rim.transform.position = BaseOrigin + new Vector3(0, 15f, 8f);
        rim.transform.rotation = Quaternion.Euler(20f, 60f, 0f);
        Light rimLight = rim.AddComponent<Light>();
        rimLight.type = LightType.Directional;
        rimLight.color = new Color(0.5f, 0.75f, 1f); // freddo
        rimLight.intensity = 0.5f;
        rimLight.shadows = LightShadows.None;

        // Point light "neon sign" per accento locale, coerente con reference urbane
        GameObject neon = new GameObject("Neon_AccentLight");
        neon.transform.SetParent(lightGroup.transform);
        neon.transform.position = BaseOrigin + new Vector3(-4f, 3f, -2f);
        Light neonLight = neon.AddComponent<Light>();
        neonLight.type = LightType.Point;
        neonLight.color = new Color(0.2f, 0.9f, 1f);
        neonLight.intensity = 3f;
        neonLight.range = 6f;

        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = new Color(0.55f, 0.6f, 0.68f);
        RenderSettings.fogStartDistance = 15f;
        RenderSettings.fogEndDistance = 60f;
    }

    // ---------------------------------------------------------------
    // GROUND (piano di gioco reale, quello con cui il player collide)
    // ---------------------------------------------------------------
    private static void BuildGroundPlane(Transform parent)
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.name = "Ground_TestFloor";
        ground.transform.SetParent(parent);
        ground.transform.position = BaseOrigin + new Vector3(0, -0.5f, 0);
        ground.transform.localScale = new Vector3(30f, 1f, 3f);
        ApplyFlatColorMaterial(ground, new Color(0.22f, 0.22f, 0.25f), "Mat_TestGround");
        Collider existingCollider = ground.GetComponent<Collider>();
            if (existingCollider != null)
        {
            UnityEngine.Object.DestroyImmediate(existingCollider);
        }

        BoxCollider2D groundCol = ground.AddComponent<BoxCollider2D>();
        groundCol.size = new Vector2(30f, 1f);
        ground.layer = LayerMask.NameToLayer("Default");
    }

    // ---------------------------------------------------------------
    // MIDGROUND: "palazzi" 3D low-poly (cubi scalati) su cui il player
    // NON cammina direttamente ma che danno massa/profondità alla scena,
    // ispirati ai riferimenti Solarian City / block-out architetture.
    // ---------------------------------------------------------------
    private static void BuildMidgroundBuildings(Transform parent)
    {
        GameObject group = new GameObject("Midground_Buildings");
        group.transform.SetParent(parent);
        group.transform.position = BaseOrigin + new Vector3(0, 0, 2f);

        Color[] palette = new Color[]
        {
            new Color(0.32f, 0.34f, 0.38f),
            new Color(0.27f, 0.29f, 0.33f),
            new Color(0.38f, 0.36f, 0.34f),
        };

        float x = -12f;
        for (int i = 0; i < 6; i++)
        {
            float h = UnityEngine.Random.Range(3f, 8f);
            float w = UnityEngine.Random.Range(1.5f, 3.5f);
            GameObject b = GameObject.CreatePrimitive(PrimitiveType.Cube);
            b.name = "Building_Mid_" + i;
            b.transform.SetParent(group.transform);
            b.transform.position = new Vector3(x, h / 2f, 0f) + group.transform.position;
            b.transform.localScale = new Vector3(w, h, w * 0.8f);
            ApplyFlatColorMaterial(b, palette[i % palette.Length], "Mat_MidBuilding_" + i);
            Collider bCollider = b.GetComponent<Collider>();

            if (bCollider != null){
            UnityEngine.Object.DestroyImmediate(bCollider);
            } 
            // Finestre = quad con colore neon, semplice billboard non ruotato (facciata fissa lato camera)
            AddWindowRow(b.transform, h, w);

            x += w + UnityEngine.Random.Range(1.5f, 3f);
        }
    }

    private static void AddWindowRow(Transform buildingRoot, float height, float width)
    {
        int rows = Mathf.Max(1, Mathf.FloorToInt(height / 1.2f));
        for (int r = 0; r < rows; r++)
        {
            GameObject window = GameObject.CreatePrimitive(PrimitiveType.Quad);
            window.name = "Window_" + r;
            window.transform.SetParent(buildingRoot);
            window.transform.localPosition = new Vector3(0f, (r - rows / 2f) * 0.35f, -0.51f);
            window.transform.localScale = new Vector3(0.6f, 0.15f, 1f);
            window.transform.localRotation = Quaternion.identity;
            Color neonColor = (r % 2 == 0)
                ? new Color(1f, 0.75f, 0.3f, 1f)
                : new Color(0.3f, 0.8f, 1f, 1f);
            ApplyEmissiveMaterial(window, neonColor, "Mat_Window_" + r);
        }
    }

    // ---------------------------------------------------------------
    // BACKGROUND: skyline lontano, silhouette flat (stile heavy-pxls
    // reference), pensato come layer parallax passivo (no collider).
    // ---------------------------------------------------------------
    private static void BuildBackgroundSkyline(Transform parent)
    {
        GameObject group = new GameObject("Background_Skyline");
        group.transform.SetParent(parent);
        group.transform.position = BaseOrigin + new Vector3(0, 4f, 20f);

        Color silhouette = new Color(0.45f, 0.5f, 0.58f, 1f);
        float x = -20f;
        for (int i = 0; i < 8; i++)
        {
            float h = UnityEngine.Random.Range(6f, 16f);
            float w = UnityEngine.Random.Range(2f, 5f);
            GameObject b = GameObject.CreatePrimitive(PrimitiveType.Cube);
            b.name = "Skyline_" + i;
            b.transform.SetParent(group.transform);
            b.transform.position = new Vector3(x, h / 2f, 0f) + group.transform.position;
            b.transform.localScale = new Vector3(w, h, 1f); // piatto: solo silhouette
            ApplyFlatColorMaterial(b, silhouette, "Mat_Skyline_" + i);
            x += w + UnityEngine.Random.Range(1f, 3f);
        }
    }

    // ---------------------------------------------------------------
    // FOREGROUND: props interattivi (barrel/tavolo) con la logica
    // "GameplaySlice" discussa: mesh 3D + BoxCollider2D separato.
    // ---------------------------------------------------------------
    private static void BuildForegroundProps(Transform parent)
    {
        GameObject group = new GameObject("Foreground_Props");
        group.transform.SetParent(parent);
        group.transform.position = BaseOrigin + new Vector3(0, 0, -3f);

        // Barrel di test
        GameObject barrel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        barrel.name = "Prop_Barrel_TEST";
        barrel.transform.SetParent(group.transform);
        barrel.transform.position = group.transform.position + new Vector3(-3f, 0.5f, 0f);
        barrel.transform.localScale = new Vector3(0.6f, 0.5f, 0.6f);
        ApplyFlatColorMaterial(barrel, new Color(0.35f, 0.25f, 0.15f), "Mat_Barrel");
        UnityEngine.Object.DestroyImmediate(barrel.GetComponent<Collider>()); // rimuovi collider 3D di default

        GameObject barrelSlice = new GameObject("GameplaySlice");
        barrelSlice.transform.SetParent(barrel.transform);
        barrelSlice.transform.localPosition = Vector3.zero;
        BoxCollider2D barrelCol = barrelSlice.AddComponent<BoxCollider2D>();
        barrelCol.size = new Vector2(1f, 1f);
        barrelSlice.layer = LayerMask.NameToLayer("Default");

        // Tavolo di test
        GameObject table = GameObject.CreatePrimitive(PrimitiveType.Cube);
        table.name = "Prop_Table_TEST";
        table.transform.SetParent(group.transform);
        table.transform.position = group.transform.position + new Vector3(2f, 0.4f, 0f);
        table.transform.localScale = new Vector3(1.4f, 0.15f, 0.8f);
        ApplyFlatColorMaterial(table, new Color(0.3f, 0.28f, 0.25f), "Mat_Table");
        UnityEngine.Object.DestroyImmediate(table.GetComponent<Collider>());

        GameObject tableSlice = new GameObject("GameplaySlice");
        tableSlice.transform.SetParent(table.transform);
        tableSlice.transform.localPosition = new Vector3(0f, 0.5f, 0f);
        BoxCollider2D tableCol = tableSlice.AddComponent<BoxCollider2D>();
        tableCol.size = new Vector2(1.4f, 0.3f);
        tableSlice.layer = LayerMask.NameToLayer("Default");
    }

    // ---------------------------------------------------------------
    // CINEMACHINE: vcam dedicata di test, priorità bassa, per non
    // interferire con lo StateDrivenCamera già presente in scena.
    // ---------------------------------------------------------------
    private static void BuildTestCinemachineVCam(Transform parent)
    {
        GameObject vcamGO = new GameObject("VCam_VisualTest");
        vcamGO.transform.SetParent(parent);
        vcamGO.transform.position = BaseOrigin + new Vector3(0, 1.6f, -8f);

        CinemachineVirtualCamera vcam = vcamGO.AddComponent<CinemachineVirtualCamera>();
        vcam.Priority = -10; // non deve mai vincere sullo state driven camera esistente
        vcam.m_Lens.FieldOfView = 28f; // FOV ristretto = compressione prospettica, look "quasi ortografico"
        vcam.m_Lens.NearClipPlane = 0.1f;
        vcam.m_Lens.FarClipPlane = 100f;

        var composer = vcam.AddCinemachineComponent<CinemachineComposer>();
        composer.m_DeadZoneWidth = 0.2f;
        composer.m_DeadZoneHeight = 0.2f;

        var transposer = vcam.AddCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(0, 1.6f, -8f);
        transposer.m_XDamping = 1f;
        transposer.m_YDamping = 1f;
        transposer.m_ZDamping = 1f;
    }

    // ---------------------------------------------------------------
    // MATERIAL HELPERS
    // ---------------------------------------------------------------
    private static void ApplyFlatColorMaterial(GameObject go, Color color, string matName)
    {
        Renderer rend = go.GetComponent<Renderer>();
        if (rend == null) return;
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        Material mat = new Material(shader);
        mat.name = matName;
        if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", color);
        if (mat.HasProperty("_Color")) mat.SetColor("_Color", color);
        rend.sharedMaterial = mat;
    }

    private static void ApplyEmissiveMaterial(GameObject go, Color emissiveColor, string matName)
    {
        Renderer rend = go.GetComponent<Renderer>();
        if (rend == null) return;
        Shader shader = Shader.Find("Universal Render Pipeline/Lit");
        if (shader == null) shader = Shader.Find("Standard");
        Material mat = new Material(shader);
        mat.name = matName;
        Color baseCol = emissiveColor * 0.3f;
        if (mat.HasProperty("_BaseColor")) mat.SetColor("_BaseColor", baseCol);
        if (mat.HasProperty("_Color")) mat.SetColor("_Color", baseCol);
        mat.EnableKeyword("_EMISSION");
        if (mat.HasProperty("_EmissionColor")) mat.SetColor("_EmissionColor", emissiveColor * 2f);
        rend.sharedMaterial = mat;
    }
}
