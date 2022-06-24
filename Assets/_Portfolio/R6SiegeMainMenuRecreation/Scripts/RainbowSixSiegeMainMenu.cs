using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RainbowSixSiegeMainMenu : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private Image _mockUp;
    [SerializeField] private Image _mockUpButtonImage;
    private bool _isMockUpOn = true;

    [Space]

    [SerializeField] private GameObject[] _uiElements;
    [SerializeField] private Image _uiButtonImage;
    private bool _isUIOn = true;

    [Space]

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

    [Header("BGM")]

    [Header("Playlist")]
    [SerializeField] private GameObject _playlistButton;
    [SerializeField] private Image _playlistButtonHighlight;
    [SerializeField] private Image _playlistButtonFill;
    [SerializeField] private Image _playlistButtonIcon;
    [SerializeField] private RectTransform _playlistButtonLightSweep;

    private Sequence _completePlaylistHighlightSequence;
    private Sequence _completePlaylistUnhighlightSequence;

    // Start is called before the first frame update
    private void Start()
    {
        //InitializeSplashSequence();
        //PlaySplashSequence();

        _BGM.Play();

        _isBGMPlaying = true;
        _isMockUpOn = true;
        _isUIOn = true;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    #region Debug Toggles

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

    #region Playlist Button

    private void InitializePlaylistButton()
    {
        _playlistButtonHighlight.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _playlistButtonHighlight.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        _playlistButtonFill.transform.localScale = Vector3.one;
        _playlistButtonFill.color = new Color(0.1882f, 0.6f, 0.8078432f, 1.0f);
        _playlistButtonIcon.transform.localScale = Vector3.one;
        _playlistButtonHighlight.gameObject.SetActive(false);
        _playlistButtonLightSweep.transform.localPosition = new Vector3(-100.0f, 10.0f, 0.0f);
    }

    public void PlayPlaylistButtonHighlightSequence()
    {
        PlaylistButtonHighlightSequence().Play();
        PlaylistButtonFillFading();
    }

    public void PlayPlaylistButtonUnhighlightSequence()
    {
        PlaylistButtonUnhighlightSequence().Play();
    }

    private Sequence PlaylistButtonHighlightSequence()
    {
        Sequence highlight = DOTween.Sequence();
        highlight
            .AppendCallback(()=> _playlistButtonHighlight.gameObject.SetActive(true))
            .Append(_playlistButtonHighlight.rectTransform.DOScale(1.0f, 0.2f))
            .Join(_playlistButtonHighlight.DOFade(1.0f, 0.2f))
            .Pause();

        Sequence lightSweep = DOTween.Sequence();
        lightSweep
            .AppendInterval(0.15f)
            .Append(_playlistButtonLightSweep.DOLocalMoveX(110.0f, 0.5f))
            .Pause();

        Sequence fill = DOTween.Sequence();
        fill
            .Append(_playlistButtonFill.rectTransform.DOScaleX(13.45f, 0.2f))
            .Pause();

        Sequence icon = DOTween.Sequence();
        icon
            .Append(_playlistButtonIcon.rectTransform.DOScale(1.15f, 0.05f))
            .Append(_playlistButtonIcon.rectTransform.DOScale(1.0f, 0.05f))
            .Pause();

        _completePlaylistHighlightSequence = DOTween.Sequence();
        _completePlaylistHighlightSequence
            .AppendCallback(InitializePlaylistButton)
            .Append(highlight)
            .Join(lightSweep)
            .Join(fill)
            .Join(icon)
            .Pause();

        return _completePlaylistHighlightSequence;
    }

    private Sequence PlaylistButtonUnhighlightSequence()
    {
        Sequence unhighlight = DOTween.Sequence();
        unhighlight
            .Append(_playlistButtonHighlight.rectTransform.DOScale(1.15f, 0.2f))
            .Join(_playlistButtonHighlight.DOFade(0.0f, 0.2f))
            .Pause();

        Sequence fill = DOTween.Sequence();
        fill
            .Append(_playlistButtonFill.rectTransform.DOScaleX(1.0f, 0.2f))
            .Pause();

        _completePlaylistUnhighlightSequence = DOTween.Sequence();
        _completePlaylistUnhighlightSequence
            .Append(unhighlight)
            .Join(fill)
            .OnComplete(()=>
            {
                _playlistButtonHighlight.gameObject.SetActive(false);
                DOTween.KillAll(true);
                _playlistButtonFill.color = new Color(0.1882f, 0.6f, 0.8078432f, 1.0f);
            })
            .Pause();

        return _completePlaylistUnhighlightSequence;
    }

    private void PlaylistButtonFillFading()
    {
        _playlistButtonFill.DOFade(0.25f, 2.0f).SetDelay(0.2f).SetLoops(-1, LoopType.Yoyo);
    }

    #endregion
}
