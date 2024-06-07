using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    public GameObject text;
    private bool activated = false;
    public GameObject panel;
    public int sceneChage;
    private bool interacting = false;
    public GameObject player;
    public TMP_Text uiText; // Referencia al componente Text en el UI
    public float delay = 0.1f; // Retardo entre cada letra
    public List<string> Text;
    private int textIndex = 0;
    public DataManager dataManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !interacting)
        {
            text.SetActive(true);
            activated = true;
        }
    }

    private void Update()
    {
        if (activated)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                panel.SetActive(true);
                Cursor.visible = true;
                interacting = true;
                player.GetComponent<PlayerMovement>().enabled = false;
                text.SetActive(false);
                StartTypewriterEffect(Text[textIndex]);
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Hola mundo");
            }
        }
    }

    public void Back()
    {
        panel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        interacting = false;
        player.GetComponent<PlayerMovement>().enabled = true;
    }

    public void Start_Puzzle()
    {
        dataManager.SaveData();
        SceneManager.LoadScene(sceneChage);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            text.SetActive(false);
            activated = false;
        }
    }

     

    // Método público para iniciar el efecto de escritura
    public void StartTypewriterEffect(string fullText)
    {
       StartCoroutine(TypeText(fullText));
    }

    // Corutina que muestra el texto letra por letra
    private IEnumerator TypeText(string fullText)
    {
       uiText.text = ""; // Asegúrate de que el texto esté vacío al inicio

       foreach (char letter in fullText.ToCharArray())
       {
          uiText.text += letter;
          yield return new WaitForSeconds(delay);
       }
    }
    


}
