using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ChallengesButton : MonoBehaviour
{
    [Header("Challenges")]
    [SerializeField] private GameObject _button;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _highlight;
    [SerializeField] private RectTransform _lightSweep;

    private Sequence _completeChallengesHighlightSequence;
    private Sequence _completeChallengesUnhighlightSequence;

    #region Challenges Button

    private void InitializeChallengesConnectButton()
    {
        _icon.rectTransform.localScale = Vector3.one;
        _highlight.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _highlight.transform.localScale = new Vector3(1.08f, 1.2f, 2.0f);

        _highlight.gameObject.SetActive(false);
        _lightSweep.transform.localPosition = new Vector3(-168.0f, 10.0f, 0.0f);
    }

    public void PlayChallengesButtonHighlightSequence()
    {
        ChallengesButtonHighlightSequence().Play();
        LoopedIconScaling();
    }

    public void PlayChallengesButtonUnhighlightSequence()
    {
        ChallengesButtonUnhighlightSequence().Play();
    }

    private Sequence ChallengesButtonHighlightSequence()
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
            .Append(_lightSweep.DOLocalMoveX(178.0f, 0.5f))
            .Pause();

        _completeChallengesHighlightSequence = DOTween.Sequence();
        _completeChallengesHighlightSequence
            .AppendCallback(InitializeChallengesConnectButton)
            .Append(highlight)
            .Join(lightSweep)
            .Pause();

        return _completeChallengesHighlightSequence;
    }

    private Sequence ChallengesButtonUnhighlightSequence()
    {
        Sequence unhighlight = DOTween.Sequence();
        unhighlight
            .Append(_highlight.rectTransform.DOScale(new Vector3(1.06f, 1.26f, 2.0f), 0.2f))
            .Join(_highlight.DOFade(0.0f, 0.2f))
            .Pause();

        _completeChallengesUnhighlightSequence = DOTween.Sequence();
        _completeChallengesUnhighlightSequence
            .Append(unhighlight)
            .OnComplete(() =>
            {
                _highlight.gameObject.SetActive(false);
                _icon.rectTransform.localScale = Vector3.one;
                DOTween.KillAll(true);
            })
            .Pause();

        return _completeChallengesUnhighlightSequence;
    }

    private void LoopedIconScaling()
    {
        _icon.rectTransform.DOScale(1.2f, 1.5f).SetLoops(-1, LoopType.Yoyo);
    }

    #endregion
}