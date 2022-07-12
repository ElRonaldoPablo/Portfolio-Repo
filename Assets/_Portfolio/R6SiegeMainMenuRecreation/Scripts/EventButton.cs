using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EventButton : MonoBehaviour
{
    [Header("Event")]
    [SerializeField] private GameObject _button;
    [SerializeField] private Image _highlight;
    [SerializeField] private RectTransform _lightSweep;

    private Sequence _completeEventHighlightSequence;
    private Sequence _completeEventUnhighlightSequence;

    #region Event Button

    private void InitializeEventButton()
    {
        _highlight.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _highlight.transform.localScale = new Vector3(1.06f, 1.125f, 2.0f);

        _highlight.gameObject.SetActive(false);
        _lightSweep.transform.localPosition = new Vector3(-376.0f, 10.0f, 0.0f);
    }

    public void PlayEventButtonHighlightSequence()
    {
        EventButtonHighlightSequence().Play();
    }

    public void PlayEventButtonUnhighlightSequence()
    {
        EventButtonUnhighlightSequence().Play();
    }

    private Sequence EventButtonHighlightSequence()
    {
        Sequence highlight = DOTween.Sequence();
        highlight
            .AppendCallback(() => _highlight.gameObject.SetActive(true))
            .Append(_highlight.rectTransform.DOScale(1.0f, 0.2f))
            .Join(_highlight.DOFade(1.0f, 0.2f))
            .Pause();

        Sequence lightSweep = DOTween.Sequence();
        lightSweep
            .AppendInterval(0.15f)
            .Append(_lightSweep.DOLocalMoveX(391.0f, 0.5f))
            .Pause();

        _completeEventHighlightSequence = DOTween.Sequence();
        _completeEventHighlightSequence
            .AppendCallback(InitializeEventButton)
            .Append(highlight)
            .Join(lightSweep)
            .Pause();

        return _completeEventHighlightSequence;
    }

    private Sequence EventButtonUnhighlightSequence()
    {
        Sequence unhighlight = DOTween.Sequence();
        unhighlight
            .Append(_highlight.rectTransform.DOScale(new Vector3(1.06f, 1.125f, 2.0f), 0.2f))
            .Join(_highlight.DOFade(0.0f, 0.2f))
            .Pause();

        _completeEventUnhighlightSequence = DOTween.Sequence();
        _completeEventUnhighlightSequence
            .Append(unhighlight)
            .OnComplete(() =>
            {
                _highlight.gameObject.SetActive(false);
                DOTween.KillAll(true);
            })
            .Pause();

        return _completeEventUnhighlightSequence;
    }

    #endregion
}
