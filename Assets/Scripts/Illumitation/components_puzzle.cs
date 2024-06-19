using System.Collections;
using System.Collections.Generic;
using UnityEditor.TestTools.CodeCoverage;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class components_puzzle : MonoBehaviour
{
    public List<Material> originalMaterials;
    public List<Material> playerMaterials; // Asigna el material desde el inspector o por código
    public int level = 0;
    // Valores para componentes ambiental, difusa y especular
    public Color diffuseColor = Color.white;
    public Color specularColor = Color.white;
    public float shininess = 0.5f;
    //public float diffuse_min, diffuse_max, specular_min, specular_max, shiness_min, shiness_max;
    public Slider diffuseSliderR, diffuseSliderG, diffuseSliderB,  specularSliderR, specularSliderG, specularSliderB, shinessSlider;
    public float tries = 0;
    public List<GameObject> examples;
    public List<GameObject> objects;
    public TMP_Text intentos, precision;
    public TMP_Text calificacion;
    public GameObject panel;
    public DataManager dataManager;
    public Emailer emailer;

    void Start()
    {
        UpdateLevel();
        if (playerMaterials[level] != null)
        {
            // Modificar color difuso
            playerMaterials[level].SetColor("_Color", diffuseColor);
            Debug.Log("Diffuse Color Set to: " + diffuseColor);

            // Modificar color especular
            playerMaterials[level].SetColor("_SpecColor", specularColor);
            Debug.Log("Specular Color Set to: " + specularColor);

            // Modificar brillo (shininess)
            playerMaterials[level].SetFloat("_Glossiness", shininess);
            Debug.Log("Glossiness Set to: " + shininess);
        }
        else
        {
            Debug.LogError("Material is not assigned!");
        }
    }

    public void ChangeDiffuse()
    {
        Debug.Log(diffuseSliderR.value);
        Debug.Log(diffuseSliderG.value);
        Debug.Log(diffuseSliderB.value);
        diffuseColor = new Color(diffuseSliderR.value, diffuseSliderG.value, diffuseSliderB.value);
        if (playerMaterials[level] != null)
        {
            // Modificar color difuso
            playerMaterials[level].SetColor("_Color", diffuseColor);
            Debug.Log("Diffuse Color Set to: " + diffuseColor);
        }
    }

    public void ChangeSpecular()
    {
        Debug.Log(specularSliderR.value);
        Debug.Log(specularSliderG.value);
        Debug.Log(specularSliderB.value);
        specularColor = new Color(specularSliderR.value, specularSliderG.value, specularSliderB.value);
        if (playerMaterials != null)
        {
            // Modificar color especular
            playerMaterials[level].SetColor("_SpecColor", specularColor);
            Debug.Log("Specular Color Set to: " + specularColor);
        }
    }

    public void ChangeShiness()
    {
        shininess = shinessSlider.value;
        if (playerMaterials[level] != null)
        {
            // Modificar brillo (shininess)
            playerMaterials[level].SetFloat("_Glossiness", shininess);
            Debug.Log("Glossiness Set to: " + shininess);
        }
    }

    public void CheckResult()
    {
        // Obtener las propiedades de los materiales
        Color originalDiffuse = originalMaterials[level].GetColor("_Color");
        Color playerDiffuse = playerMaterials[level].GetColor("_Color");

        Color originalSpecular = originalMaterials[level].GetColor("_SpecColor");
        Color playerSpecular = playerMaterials[level].GetColor("_SpecColor");

        float originalSmoothness = originalMaterials[level].GetFloat("_Glossiness");
        float playerSmoothness = playerMaterials[level].GetFloat("_Glossiness");

        // Comparar las propiedades
        float diffuseScore = CompareColors(originalDiffuse, playerDiffuse);
        float specularScore = CompareColors(originalSpecular, playerSpecular);
        float smoothnessScore = 1.0f - Mathf.Abs(originalSmoothness - playerSmoothness);

        // Calcular el porcentaje de exactitud
        float accuracy = (diffuseScore + specularScore + smoothnessScore) / 3.0f * 100.0f;

        Debug.Log("Porcentaje de exactitud: " + accuracy + "%");
        precision.text = accuracy.ToString();
        if (accuracy > 93.3)
        {
            if (level < objects.Count - 1)
            {
                level++;
                UpdateLevel();
                diffuseSliderR.value = 0;
                diffuseSliderG.value = 0;
                diffuseSliderB.value = 0;
                specularSliderR.value = 0;
                specularSliderG.value = 0;
                specularSliderB.value = 0;
                shinessSlider.value = 0;
            }
            else
            {
                float score = 10 - tries / 5;
                calificacion.text = "Calificación: " + score.ToString();
                panel.SetActive(true);
                Debug.Log("Nivel superado!!");
                dataManager.playerDataSO.levelsAccomplished.Add(2);
                emailer.PlayerProgress("materiales", dataManager.playerDataSO.username, dataManager.playerDataSO.accountNumber, score.ToString());
                emailer.SendEmail(dataManager.playerDataSO.professorEmail);
                dataManager.playerDataSO.tries = 0;
                dataManager.SaveData();
            }
        }
        else
        {
            tries++;
        }
        intentos.text = tries.ToString();
    }


    public void ReturnToMaiNScene()
    {
        SceneManager.LoadScene(1);
    }


    float CompareColors(Color a, Color b)
    {
        float difference = Mathf.Abs(a.r - b.r) + Mathf.Abs(a.g - b.g) + Mathf.Abs(a.b - b.b) + Mathf.Abs(a.a - b.a);
        float maxDifference = 4.0f; // La diferencia máxima posible es 1 por cada canal de color (RGBA)

        return 1.0f - (difference / maxDifference);
    }

    public void UpdateLevel()
    {
        foreach(GameObject puzzle in objects)
        {
            puzzle.SetActive(false);
        }
        foreach(GameObject example in examples)
        {
            example.SetActive(false);
        }

        objects[level].SetActive(true);
        examples[level].SetActive(true);
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }

}
