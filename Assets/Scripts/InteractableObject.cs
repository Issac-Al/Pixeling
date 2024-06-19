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
    [SerializeField]
    private int textIndex = 0;
    public int level;
    public bool levelCompleted = false;
    public DataManager dataManager;
    public GameObject model;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Quaternion targetRotation;
    public bool faceUser = false;
    private bool isRotating = false, isReturning = false;
    private Coroutine typingCoroutine;
    public float rotationSpeed = 2f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !interacting && !levelCompleted)
        {
            text.SetActive(true);
            activated = true;
        }
    }

    public void Start()
    {
        originalPosition = model.transform.position;
        originalRotation = model.transform.rotation;
        foreach (int player_level in dataManager.playerDataSO.levelsAccomplished)
        {
            if (player_level == level)
                //Debug.Log(player_level);
                levelCompleted = true;
        }
    }


    private void Update()
    {
        if (activated && !levelCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                panel.SetActive(true);
                Cursor.visible = true;
                interacting = true;
                player.GetComponent<Animator>().SetBool("moving", false);
                player.GetComponent<PlayerMovement>().enabled = false;
                text.SetActive(false);
                StartTypewriterEffect(Text[textIndex]);
                //textIndex++;
                Cursor.lockState = CursorLockMode.None;
                Debug.Log("Hola mundo");
                if (faceUser)
                {
                    StartFacingPlayer();
                }
            }
        }
        if (isRotating)
        {
            RotateTowardsPlayer();
        }
        if (isReturning)
        {
            RotateTowardsStartPos();
        }
    }

    public void StartFacingPlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - model.transform.position).normalized;
        directionToPlayer.y = 0; // Ignorar la diferencia en altura
        targetRotation = Quaternion.LookRotation(directionToPlayer);
        isRotating = true;
    }

    private void RotateTowardsPlayer()
    {
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(model.transform.rotation, targetRotation) < 0.1f)
        {
            model.transform.rotation = targetRotation; // Asegurarse de que la rotación final sea exacta
            isRotating = false;
        }
    }

    private void RotateTowardsStartPos()
    {
        model.transform.rotation = Quaternion.Slerp(model.transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(model.transform.rotation, originalRotation) < 0.1f)
        {
            model.transform.rotation = originalRotation; // Asegurarse de que la rotación final sea exacta
            isReturning = false;
        }
    }

    public void NextText()
    {
        Debug.Log("Text Index inicial" + textIndex);
        Debug.Log("Tamano del texto" + Text.Count);
        if (textIndex < Text.Count-1)
        {
            textIndex++;
            StartTypewriterEffect(Text[textIndex]);
            Debug.Log(textIndex);
        }
        else
        {
            textIndex = 0;
            RotateTowardsStartPos();
            isReturning = true;
            //model.transform.position = originalPosition;
            //model.transform.rotation = originalRotation;
            Back();
        }
    }

    public void PreviewsText()
    {
        Debug.Log("textIndex previews text: " + textIndex);
        if (textIndex > 0)
        {
            textIndex--;
            StartTypewriterEffect(Text[textIndex]);
            Debug.Log(textIndex);
        }
        Debug.Log("textIndex previews text: " + textIndex);
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
        dataManager.playerDataSO.playerPosition = player.transform.position;
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
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(fullText));
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

        typingCoroutine = null;

    }

    public void FacePlayer()
    {
        model.transform.LookAt(player.transform);
    }
    


}
