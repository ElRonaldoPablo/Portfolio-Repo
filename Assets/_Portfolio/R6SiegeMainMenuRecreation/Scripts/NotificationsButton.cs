using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NotificationsButton : MonoBehaviour
{
    [Header("Notifications")]
    [SerializeField] private GameObject _button;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _highlight;
    [SerializeField] private RectTransform _lightSweep;

    private Sequence _completeNotificationsHighlightSequence;
    private Sequence _completeNotificationsUnhighlightSequence;

    #region Animations

    private void InitializeNotificationsButton()
    {
        _icon.color = new Color(0.2264151f, 0.2264151f, 0.2264151f, 1.0f);
        _highlight.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _highlight.transform.localScale = new Vector3(1.04f, 1.5f, 2.0f);

        _highlight.gameObject.SetActive(false);
        _lightSweep.transform.localPosition = new Vector3(-295.0f, 10.0f, 0.0f);
    }

    public void PlayNotificationsButtonHighlightSequence()
    {
        NotificationsButtonHighlightSequence().Play();
    }

    public void PlayNotificationsButtonUnhighlightSequence()
    {
        NotificationsButtonUnhighlightSequence().Play();
    }

    private Sequence NotificationsButtonHighlightSequence()
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
            .Append(_lightSweep.DOLocalMoveX(303.0f, 0.5f))
            .Pause();

        Sequence icon = DOTween.Sequence();
        icon
            .AppendCallback(()=> _icon.color = Color.white)
            .Append(_icon.rectTransform.DOScale(1.25f, 0.15f))
            .Append(_icon.rectTransform.DOScale(1.0f, 0.15f))
            .Pause();

        _completeNotificationsHighlightSequence = DOTween.Sequence();
        _completeNotificationsHighlightSequence
            .AppendCallback(InitializeNotificationsButton)
            .Append(highlight)
            .Join(lightSweep)
            .Join(icon)
            .Pause();

        return _completeNotificationsHighlightSequence;
    }

    private Sequence NotificationsButtonUnhighlightSequence()
    {
        Sequence unhighlight = DOTween.Sequence();
        unhighlight
            .Append(_highlight.rectTransform.DOScale(new Vector3(1.04f, 1.5f, 2.0f), 0.2f))
            .Join(_highlight.DOFade(0.0f, 0.2f))
            .Pause();

        _completeNotificationsUnhighlightSequence = DOTween.Sequence();
        _completeNotificationsUnhighlightSequence
            .Append(unhighlight)
            .OnComplete(() =>
            {
                _highlight.gameObject.SetActive(false);
                DOTween.Kill(true);
                _icon.color = new Color(0.2264151f, 0.2264151f, 0.2264151f, 1.0f);
            })
            .Pause();

        return _completeNotificationsUnhighlightSequence;
    }

    #endregion
}
