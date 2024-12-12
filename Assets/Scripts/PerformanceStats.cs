using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.Profiling;
using UnityEngine;

public class PerformanceStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fps_Text;          // Affichage FPS
    [SerializeField] private TextMeshProUGUI triangle_Text;     // Affichage triangles
    [SerializeField] private TextMeshProUGUI vertices_Text;     // Affichage vertices
    [SerializeField] private TextMeshProUGUI batches_Text;      // Affichage batches

    ProfilerRecorder drawCallsCountRecorder;

    private float avgFrameRate;

    void Start()
    {
        InvokeRepeating("DisplayFrameRate", 1f, 1f);  // Appel toutes les secondes pour le framerate
        InvokeRepeating("CountAllStats", 1f, 5f);     // Appel toutes les 5 secondes pour les statistiques

    }

    private void Update()
    {
        var sb = new StringBuilder(500);
        sb.AppendLine($"Batches: {drawCallsCountRecorder. LastValue}");
        batches_Text.text = sb.ToString();
    }

    private void DisplayFrameRate()
    {
        float current = 1f / Time.unscaledDeltaTime; // Calcul du FPS instantané
        avgFrameRate = (int)current; // Conversion en entier pour un affichage propre
        fps_Text.text = avgFrameRate.ToString() + " FPS"; // Affichage des FPS dans le TextMeshPro
    }

    private void CountAllStats()
    {
        CountAllTriangles();
        CountAllVertices();
    }

    private void CountAllTriangles()
    {
        int totalTriangles = 0;

        // Trouver tous les MeshFilters dans la scène
        MeshFilter[] allMeshFilters = UnityEngine.Object.FindObjectsOfType<MeshFilter>();
        foreach (MeshFilter meshFilter in allMeshFilters)
        {
            // Vérifier si le mesh est lisible
            if (meshFilter.sharedMesh != null && meshFilter.sharedMesh.isReadable)
            {
                totalTriangles += meshFilter.sharedMesh.triangles.Length / 3;
            }
        }

        // Affichage du total de triangles dans le TextMeshPro
        triangle_Text.text = "Total triangles: " + totalTriangles;
    }

    private void CountAllVertices()
    {
        int totalVertices = 0;

        // Trouver tous les MeshFilters dans la scène
        MeshFilter[] allMeshFilters = UnityEngine.Object.FindObjectsOfType<MeshFilter>();
        foreach (MeshFilter meshFilter in allMeshFilters)
        {
            // Vérifier si le mesh est lisible
            if (meshFilter.sharedMesh != null && meshFilter.sharedMesh.isReadable)
            {
                totalVertices += meshFilter.sharedMesh.vertexCount;
            }
        }

        // Affichage du total de vertices dans le TextMeshPro
        vertices_Text.text = "Total vertices: " + totalVertices;
    }

    private void OnEnable()
    {
        drawCallsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
    }

    private void OnDisable()
    {
        drawCallsCountRecorder.Dispose();
    }
}
