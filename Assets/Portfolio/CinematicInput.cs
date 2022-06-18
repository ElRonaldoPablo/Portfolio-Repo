using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CinematicInput : MonoBehaviour
{
    PlaystationInput psInput;
    private float hold;

    [Space]
    [SerializeField] private GameObject radialBarObject = null;
    [SerializeField] private Image skipRadialBar = null;
    [SerializeField] private string sceneToLoad = "";

    [Space]
    [SerializeField] [Range(0.0f, 100.0f)] private float fillRate = 45.0f;
    [SerializeField][Range(0.0f, 100.0f)] private float currentFill = 0.0f;
    [SerializeField][Range(100.0f, 1000.0f)] private float maxFill = 100.0f;

    [Space]
    [SerializeField] private bool skip = false;

    // Start is called before the first frame update
    void Awake()
    {
        skip = false;
        psInput = new PlaystationInput();
    }

    void Start()
    {
        //psInput.CinematicActionMap.Skip.started += context => hold = 1.0f;
        psInput.CinematicActionMap.Skip.performed += context => hold = context.ReadValue<float>();
        psInput.CinematicActionMap.Skip.canceled += context => hold = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float skipPercentage = currentFill / maxFill;
        skipRadialBar.fillAmount = skipPercentage;

        if (!skip && hold == 1.0f)
        {
            IncreaseSkipRadialValue();

            if (currentFill >= maxFill)
            {
                StartCoroutine(SkipCinematic());
            }
        }
        else if (!skip && hold == 0.0f)
        {
            currentFill = 0.0f;
            radialBarObject.SetActive(false);
        }
    }

    IEnumerator SkipCinematic()
    {
        skip = true;
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneToLoad);
    }

    private void IncreaseSkipRadialValue()
    {
        radialBarObject.SetActive(true);

        if (hold == 1.0f)
        {
            currentFill = currentFill + fillRate * Time.deltaTime;
        }
    }

    #region OnEnable & OnDisable Functions

    private void OnEnable()
    {
        psInput.CinematicActionMap.Enable();
    }

    private void OnDisable()
    {
        psInput.CinematicActionMap.Disable();
    }

    #endregion
}
