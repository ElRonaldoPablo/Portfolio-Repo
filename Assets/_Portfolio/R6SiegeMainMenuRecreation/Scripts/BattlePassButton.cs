using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattlePassButton : MonoBehaviour
{
    [Header("Battle Pass")]
    [SerializeField] private GameObject _button;
    [SerializeField] private Image _highlight;
    [SerializeField] private RectTransform _lightSweep;

    private Sequence _completeBattlePassHighlightSequence;
    private Sequence _completeBattlePassUnhighlightSequence;

    #region Challenges Button

    private void InitializeBattlePassButton()
    {
        _highlight.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _highlight.transform.localScale = new Vector3(1.06f, 1.125f, 2.0f);

        _highlight.gameObject.SetActive(false);
        _lightSweep.transform.localPosition = new Vector3(-376.0f, 10.0f, 0.0f);
    }

    public void PlayBattlePassButtonHighlightSequence()
    {
        BattlePassButtonHighlightSequence().Play();
    }

    public void PlayBattlePassButtonUnhighlightSequence()
    {
        ChallengesButtonUnhighlightSequence().Play();
    }

    private Sequence BattlePassButtonHighlightSequence()
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

        _completeBattlePassHighlightSequence = DOTween.Sequence();
        _completeBattlePassHighlightSequence
            .AppendCallback(InitializeBattlePassButton)
            .Append(highlight)
            .Join(lightSweep)
            .Pause();

        return _completeBattlePassHighlightSequence;
    }

    private Sequence ChallengesButtonUnhighlightSequence()
    {
        Sequence unhighlight = DOTween.Sequence();
        unhighlight
            .Append(_highlight.rectTransform.DOScale(new Vector3(1.06f, 1.125f, 2.0f), 0.2f))
            .Join(_highlight.DOFade(0.0f, 0.2f))
            .Pause();

        _completeBattlePassUnhighlightSequence = DOTween.Sequence();
        _completeBattlePassUnhighlightSequence
            .Append(unhighlight)
            .OnComplete(() =>
            {
                _highlight.gameObject.SetActive(false);
                DOTween.KillAll(true);
            })
            .Pause();

        return _completeBattlePassUnhighlightSequence;
    }

    #endregion
}
