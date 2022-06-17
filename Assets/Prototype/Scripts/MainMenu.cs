using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

[HelpURL("https://youtu.be/p-3S73MaDP8")]
public class MainMenu : MonoBehaviour
{
    PlaystationInput psInput;
    [SerializeField] private GameObject anyKeyPanel = null;
    [SerializeField] private GameObject mainMenuPanel = null;
    private bool anyKeyPanelActive;
    Vector2 move;
    [SerializeField] private GameObject continueButton = null;

    [Header("Demo Mode")]
    [SerializeField] private GameObject mainCamera = null;
    [SerializeField] private GameObject mainMenuBackground = null;
    //[SerializeField] private GameObject anyKeyTextPanel = null;
    [SerializeField] private GameObject mainMenuContentPanel = null;

    [SerializeField] private GameObject demoModeCamera = null;


    // Start is called before the first frame update
    void Awake()
    {
        psInput = new PlaystationInput();
        anyKeyPanelActive = true;

        psInput.MainMenuActionMap.Move.performed += context => move = context.ReadValue<Vector2>();
        psInput.MainMenuActionMap.Move.canceled += context => move = Vector2.zero;
        EventSystem.current.firstSelectedGameObject = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anyKeyPanelActive)
        {
            //psInput.MainMenuActionMap.AnyButton.performed += context => AnyKeyTextDeactivation();
            psInput.MainMenuActionMap.Options.performed += context => StartCoroutine(AnyKeyPanelDeactivation());
        }
        else
        {
            psInput.MainMenuActionMap.Cancel.performed += context => AnyKeyTextActivation();
        }
    }

    public void GoToContinue()
    {
        SceneManager.LoadScene("Continue");
    }

    public void GoToNewGame()
    {
        SceneManager.LoadScene("New");
    }

    public void GoToOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void GoToExtras()
    {
        SceneManager.LoadScene("Extras");
    }

    public void ExitApplication()
    {
        #region Debug.LogWarning Messages
        Debug.LogWarning("NOTICE: Exit Application Protocol has been engaged.");
        #endregion

        Application.Quit();
    }

    #region AnyKeyInput Detection Functions

    private void AnyKeyTextDeactivation()
    {
        anyKeyPanel.SetActive(false);
        anyKeyPanelActive = false;

        mainMenuPanel.SetActive(true);
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.SetSelectedGameObject(continueButton);
    }

    IEnumerator AnyKeyPanelDeactivation()
    {
        anyKeyPanel.SetActive(false);
        anyKeyPanelActive = false;

        mainMenuPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForSeconds(0.1f);
        EventSystem.current.SetSelectedGameObject(continueButton);
    }

    private void AnyKeyTextActivation()
    {
        
        mainMenuPanel.SetActive(false);

        anyKeyPanel.SetActive(true);
        anyKeyPanelActive = true;
    }

    #endregion

    private void OnEnable()
    {
        psInput.MainMenuActionMap.Enable();
    }

    private void OnDisable()
    {
        psInput.MainMenuActionMap.Disable();
    }
}
