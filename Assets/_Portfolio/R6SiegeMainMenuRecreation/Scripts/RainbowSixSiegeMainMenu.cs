using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RainbowSixSiegeMainMenu : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private RectTransform _debugSlidingPanel;
    [SerializeField] private Button _debugToggleButton;
    [SerializeField] private Button _clickOutButton;
    [SerializeField] private Image _debugButtonIcon;
    private bool _isDebugPanelOn = false;

    [Space]
    [SerializeField] private Image _mockUp;
    [SerializeField] private Image _mockUpButtonImage;
    private bool _isMockUpOn = false;

    [Space]
    [SerializeField] private GameObject[] _uiElements;
    [SerializeField] private Image _uiButtonImage;
    private bool _isUIOn = true;

    [Space]
    [SerializeField] private RawImage _animatedBG;
    [SerializeField] private Image _animBGButtonImage;
    private bool _isAnimatedBGOn = false;

    [Header("BGM")]
    [SerializeField] private AudioSource _BGM;
    [SerializeField] private Image _BGMButtonImage;
    private bool _isBGMPlaying = true;

    [Header("Splash")]
    [SerializeField] private GameObject _splashPanel;
    [SerializeField] private Image _splashBackgroundImage;
    [SerializeField] private Image _splashFadeImage;
    [SerializeField] private TextMeshProUGUI _splashString;
    [SerializeField] private TextMeshProUGUI _projectDetailsString;

    private Sequence _completeSplashSequence;

    // Start is called before the first frame update
    private void Start()
    {
        //InitializeSplashSequence();
        //PlaySplashSequence();

        InitializeDebugToggle();

        _BGM.Play();

        _isBGMPlaying = true;
        _isMockUpOn = false;
        _isUIOn = true;
        _isAnimatedBGOn = false;
        _isDebugPanelOn = false;
    }

    #region Debug Toggles

    private void InitializeDebugToggle()
    {
        var buttonImage = _debugToggleButton.GetComponent<Image>();

        _debugToggleButton.gameObject.SetActive(true);
        _clickOutButton.gameObject.SetActive(false);

        _debugSlidingPanel.localPosition = new Vector3(1039.5f, 0.0f, 0.0f);
        buttonImage.color = new Color(0.0f, 0.0f, 0.0f, 0.5019608f);
        _debugButtonIcon.color = new Color(0.2848765f, 1.0f, 0.0f, 1.0f);
    }

    public void ToggleDebugPanel()
    {
        if (!_isDebugPanelOn)
        {
            DebugPanelSlideIn().Play();
            _isDebugPanelOn = true;
        }
        else
        {
            DebugPanelSlideOut().Play();
            _isDebugPanelOn = false;
        }
    }

    private Sequence DebugPanelSlideIn()
    {
        Sequence slideIn = DOTween.Sequence();
        slideIn
            .Append(_debugSlidingPanel.DOLocalMoveX(881.0f, 0.75f))
            .Pause();

        Sequence buttonFadeOut = DOTween.Sequence();
        buttonFadeOut
            .Append(_debugToggleButton.image.DOFade(0.0f, 0.75f))
            .Join(_debugButtonIcon.DOFade(0.0f, 0.75f))
            .Pause();

        Sequence completePanelSlideInSequence = DOTween.Sequence();
        completePanelSlideInSequence
            .Append(slideIn)
            .Join(buttonFadeOut)
            .OnComplete(()=>
            {
                _debugToggleButton.gameObject.SetActive(false);
                _clickOutButton.gameObject.SetActive(true);
            })
            .Pause();

        return completePanelSlideInSequence;
    }

    private Sequence DebugPanelSlideOut()
    {
        Sequence slideOut = DOTween.Sequence();
        slideOut
            .Append(_debugSlidingPanel.DOLocalMoveX(1039.5f, 0.75f))
            .Pause();

        Sequence buttonFadeIn = DOTween.Sequence();
        buttonFadeIn
            .Append(_debugToggleButton.image.DOFade(0.5019608f, 0.75f))
            .Join(_debugButtonIcon.DOFade(1.0f, 0.75f))
            .Pause();

        Sequence completePanelSlideOutSequence = DOTween.Sequence();
        completePanelSlideOutSequence
            .AppendCallback(() =>
            {
                _clickOutButton.gameObject.SetActive(false);
                _debugToggleButton.gameObject.SetActive(true);
            })
            .Append(slideOut)
            .Join(buttonFadeIn)
            .Pause();

        return completePanelSlideOutSequence;
    }

    public void ToggleMockup()
    {
        if (_isMockUpOn)
        {
            _mockUp.gameObject.SetActive(false);
            _isMockUpOn = false;
            _mockUpButtonImage.color = new Color(0.75f, 0.3f, 0.3f, 1.0f);
        }
        else
        {
            _mockUp.gameObject.SetActive(true);
            _isMockUpOn = true;
            _mockUpButtonImage.color = new Color(0.5f, 0.775f, 0.3f, 1.0f);
        }
    }

    public void ToggleUIElements()
    {
        if (_isUIOn)
        {
            foreach (GameObject element in _uiElements)
            {
                element.SetActive(false);
            }

            _isUIOn = false;
            _uiButtonImage.color = new Color(0.75f, 0.3f, 0.3f, 1.0f);
        }
        else
        {
            foreach (GameObject element in _uiElements)
            {
                element.SetActive(true);
            }

            _isUIOn = true;
            _uiButtonImage.color = new Color(0.5f, 0.775f, 0.3f, 1.0f);
        }
    }

    public void ToggleBackgroundMusic()
    {
        if (_isBGMPlaying)
        {
            _BGM.Pause();
            _isBGMPlaying = false;
            _BGMButtonImage.color = new Color(0.75f, 0.3f, 0.3f, 1.0f);
        }
        else
        {
            _BGM.Play();
            _isBGMPlaying = true;
            _BGMButtonImage.color = new Color(0.5f, 0.775f, 0.3f, 1.0f);
        }
    }

    public void ToggleAnimatedBG()
    {
        if (!_isAnimatedBGOn)
        {
            _animatedBG.gameObject.SetActive(true);
            _isAnimatedBGOn = true;
            _animBGButtonImage.color = new Color(0.5f, 0.775f, 0.3f, 1.0f);
        }
        else
        {
            _animatedBG.gameObject.SetActive(false);
            _isAnimatedBGOn = false;
            _animBGButtonImage.color = new Color(0.75f, 0.3f, 0.3f, 1.0f);
        }
    }

    #endregion

    #region Splash

    private void InitializeSplashSequence()
    {
        _splashBackgroundImage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        _splashFadeImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _splashString.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _projectDetailsString.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        _splashPanel.SetActive(true);

        Debug.Log("NOTICE | Initilization has been conducted.");
    }

    private void PlaySplashSequence()
    {
        SplashSequence().Play();
    }

    private Sequence SplashSequence()
    {
        Sequence fadeIn = DOTween.Sequence();
        fadeIn
            .AppendInterval(2.0f)
            .Append(_splashFadeImage.DOFade(1.0f, 2.0f))
            .AppendInterval(1.5f)
            .Append(_splashFadeImage.DOFade(0.0f, 2.0f))
            .Join(_splashString.DOFade(1.0f, 2.0f))
            .AppendInterval(3.0f)
            .Pause();

        Sequence fadeOut = DOTween.Sequence();
        fadeOut
            .Append(_splashFadeImage.DOFade(1.0f, 2.0f))
            .AppendInterval(1.5f)
            .Append(_splashString.DOFade(0.0f, 0.0f))
            .Append(_splashFadeImage.DOFade(0.0f, 2.0f))
            .Join(_splashBackgroundImage.DOFade(0.0f, 2.0f))
            .Pause();

        Sequence projectDetailsFadeIn = DOTween.Sequence();
        projectDetailsFadeIn
            .Append(_projectDetailsString.DOFade(1.0f, 2.0f))
            .Pause();

        _completeSplashSequence = DOTween.Sequence();
        _completeSplashSequence
            .Append(fadeIn)
            .Append(fadeOut)
            .Append(projectDetailsFadeIn)
            .Pause();

        return _completeSplashSequence;
    }

    #endregion
}
