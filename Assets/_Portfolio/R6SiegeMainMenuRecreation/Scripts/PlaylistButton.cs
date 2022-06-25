using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlaylistButton : MonoBehaviour
{
    [Header("Playlist")]
    [SerializeField] private GameObject _playlistButton;
    [SerializeField] private Image _playlistButtonHighlight;
    [SerializeField] private Image _playlistButtonFill;
    [SerializeField] private Image _playlistButtonIcon;
    [SerializeField] private RectTransform _playlistButtonLightSweep;

    private Sequence _completePlaylistHighlightSequence;
    private Sequence _completePlaylistUnhighlightSequence;

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
            .AppendCallback(() => _playlistButtonHighlight.gameObject.SetActive(true))
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
            .OnComplete(() =>
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
