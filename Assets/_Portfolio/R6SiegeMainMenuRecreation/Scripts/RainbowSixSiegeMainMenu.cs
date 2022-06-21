using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RainbowSixSiegeMainMenu : MonoBehaviour
{
    [Header("Splash")]
    [SerializeField] private GameObject _splashPanel;
    [SerializeField] private Image _splashBackgroundImage;
    [SerializeField] private Image _splashFadeImage;
    [SerializeField] private TextMeshProUGUI _splashString;
    [SerializeField] private TextMeshProUGUI _projectDetailsString;

    private Sequence _completeSplashSequence;

    [Header("Playlist")]
    [SerializeField] private GameObject _playlistButton;
    [SerializeField] private Image _playlistButtonHighlight;
    [SerializeField] private Image _playlistButtonFill;
    [SerializeField] private Image _playlistButtonIcon;

    private Sequence _completePlaylistHighlightSequence;
    private Sequence _completePlaylistUnhighlightSequence;

    // Start is called before the first frame update
    private void Start()
    {
        //InitializeSplashSequence();
        //PlaySplashSequence();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

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
        _playlistButtonIcon.transform.localScale = Vector3.one;
        _playlistButtonHighlight.gameObject.SetActive(false);
    }

    public void PlayPlaylistButtonHighlightSequence()
    {
        PlaylistButtonHighlightSequence().Play();
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
            .AppendCallback(null)
            .Append(null)
            .Join(null)
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
            .Join(lightSweep) // TO-DO
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
            .OnComplete(()=> _playlistButtonHighlight.gameObject.SetActive(false))
            .Pause();

        return _completePlaylistUnhighlightSequence;
    }

    #endregion
}
