using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MatchmakeButton : MonoBehaviour
{
    [Header("Queue")]
    [SerializeField] private GameObject _queueButton;
    [SerializeField] private Image _queueButtonHighlight;
    [SerializeField] private Image _queueButtonFill;
    [SerializeField] private RectTransform _queueButtonString;
    [SerializeField] private RectTransform _queueButtonLightSweep;

    private Sequence _completeQueueHighlightSequence;
    private Sequence _completeQueueUnhighlightSequence;

    #region Queue Button

    private void InitializeQueueButton()
    {
        _queueButtonHighlight.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _queueButtonHighlight.transform.localScale = new Vector3(1.06f, 1.26f, 2.0f);
        _queueButtonFill.color = new Color(0.1882f, 0.6f, 0.8078432f, 1.0f);
        _queueButtonString.transform.localScale = Vector3.one;

        _queueButtonHighlight.gameObject.SetActive(false);
        _queueButtonLightSweep.transform.localPosition = new Vector3(-238.0f, 10.0f, 0.0f);
    }

    public void PlayQueueButtonHighlightSequence()
    {
        QueueButtonHighlightSequence().Play();
        QueueButtonFillFading();
    }

    public void PlayQueueButtonUnhighlightSequence()
    {
        QueueButtonUnhighlightSequence().Play();
    }

    private Sequence QueueButtonHighlightSequence()
    {
        Sequence highlight = DOTween.Sequence();
        highlight
            .AppendCallback(() => _queueButtonHighlight.gameObject.SetActive(true))
            .Append(_queueButtonHighlight.rectTransform.DOScale(1.0f, 0.2f))
            .Join(_queueButtonHighlight.DOFade(1.0f, 0.2f))
            .Pause();

        Sequence lightSweep = DOTween.Sequence();
        lightSweep
            .AppendInterval(0.15f)
            .Append(_queueButtonLightSweep.DOLocalMoveX(242.0f, 0.5f))
            .Pause();

        Sequence gameModeString = DOTween.Sequence();
        gameModeString
            .Append(_queueButtonString.DOScale(1.15f, 0.05f))
            .Append(_queueButtonString.DOScale(1.0f, 0.05f))
            .Pause();

        _completeQueueHighlightSequence = DOTween.Sequence();
        _completeQueueHighlightSequence
            .AppendCallback(InitializeQueueButton)
            .Append(highlight)
            .Join(lightSweep)
            .Join(gameModeString)
            .Pause();

        return _completeQueueHighlightSequence;
    }

    private Sequence QueueButtonUnhighlightSequence()
    {
        Sequence unhighlight = DOTween.Sequence();
        unhighlight
            .Append(_queueButtonHighlight.rectTransform.DOScale(new Vector3(1.06f, 1.26f, 2.0f), 0.2f))
            .Join(_queueButtonHighlight.DOFade(0.0f, 0.2f))
            .Pause();

        _completeQueueUnhighlightSequence = DOTween.Sequence();
        _completeQueueUnhighlightSequence
            .Append(unhighlight)
            .OnComplete(() =>
            {
                _queueButtonHighlight.gameObject.SetActive(false);
                DOTween.KillAll(true);
                _queueButtonFill.color = new Color(0.1882f, 0.6f, 0.8078432f, 1.0f);
            })
            .Pause();

        return _completeQueueUnhighlightSequence;
    }

    private void QueueButtonFillFading()
    {
        _queueButtonFill.DOFade(0.25f, 2.0f).SetDelay(0.2f).SetLoops(-1, LoopType.Yoyo);
    }

    #endregion
}
