using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BoostersButton : MonoBehaviour
{
    [Header("Boosters")]
    [SerializeField] private GameObject _button;
    [SerializeField] private Image _highlight;
    [SerializeField] private Image _diamondPlating;
    [SerializeField] private RectTransform _lightSweep;
    [SerializeField] private RectTransform _icon;
    [SerializeField] private RectTransform _quantity;
    [SerializeField] private RectTransform _iconBackground;
    [SerializeField] private RectTransform _quantityBackground;

    private Sequence _completeBoostersHighlightSequence;
    private Sequence _completeBoostersUnhighlightSequence;

    private string _iconScalingLoopAnimKill = "IconScalingLoopAnimKill";

    #region Boosters Button

    private void InitializeBoostersButton()
    {
        _highlight.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _highlight.transform.localScale = new Vector3(1.08f, 1.2f, 2.0f);

        _highlight.gameObject.SetActive(false);
        _lightSweep.transform.localPosition = new Vector3(-168.0f, 10.0f, 0.0f);

        _iconBackground.transform.localPosition = new Vector3(-72.0f, 0.0f, 0.0f);
        _quantityBackground.transform.localPosition = new Vector3(108.0f, 0.0f, 0.0f);
        _quantityBackground.sizeDelta = new Vector2(148.0f, 54.0f);
        _quantity.transform.localScale = Vector3.one;
        _diamondPlating.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _icon.localScale = Vector3.one;
    }

    public void PlayBoostersButtonHighlightSequence()
    {
        BoostersButtonHighlightSequence().Play();
        BoostersIconScaling();
    }

    public void PlayBoostersButtonUnhighlightSequence()
    {
        PacksButtonUnhighlightSequence().Play();
    }

    private Sequence BoostersButtonHighlightSequence()
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
            .Append(_lightSweep.DOLocalMoveX(177.0f, 0.5f))
            .Pause();

        Sequence iconBG = DOTween.Sequence();
        iconBG
            .Append(_iconBackground.DOLocalMoveX(-62.0f, 0.5f))
            .Pause();

        Sequence qtyBG = DOTween.Sequence();
        qtyBG
            .Append(_quantityBackground.DOLocalMoveX(98.0f, 0.5f))
            .Join(_quantityBackground.DOSizeDelta(new Vector2(175.0f, 54.0f), 0.5f))
            .Pause();

        Sequence qty = DOTween.Sequence();
        qty
            .Append(_quantity.DOScale(1.15f, 0.05f))
            .Append(_quantity.DOScale(1.0f, 0.05f))
            .Pause();

        Sequence plating = DOTween.Sequence();
        plating
            .Append(_diamondPlating.DOFade(0.25f, 0.5f))
            .Pause();

        _completeBoostersHighlightSequence = DOTween.Sequence();
        _completeBoostersHighlightSequence
            .AppendCallback(InitializeBoostersButton)
            .Append(highlight)
            .Join(lightSweep)
            .Join(iconBG)
            .Join(qtyBG)
            .Join(qty)
            .Join(plating)
            .Pause();

        return _completeBoostersHighlightSequence;
    }

    private Sequence PacksButtonUnhighlightSequence()
    {
        Sequence unhighlight = DOTween.Sequence();
        unhighlight
            .Append(_highlight.rectTransform.DOScale(new Vector3(1.08f, 1.2f, 2.0f), 0.2f))
            .Join(_highlight.DOFade(0.0f, 0.2f))
            .Pause();

        Sequence iconBG = DOTween.Sequence();
        iconBG
            .Append(_iconBackground.DOLocalMoveX(-72.0f, 0.5f))
            .Pause();

        Sequence qtyBG = DOTween.Sequence();
        qtyBG
            .Append(_quantityBackground.DOLocalMoveX(108.0f, 0.5f))
            .Join(_quantityBackground.DOSizeDelta(new Vector2(148.0f, 54.0f), 0.5f))
            .Pause();

        Sequence plating = DOTween.Sequence();
        plating
            .Append(_diamondPlating.DOFade(0.0f, 0.5f))
            .Pause();

        _completeBoostersUnhighlightSequence = DOTween.Sequence();
        _completeBoostersUnhighlightSequence
            .Append(unhighlight)
            .Join(iconBG)
            .Join(qtyBG)
            .Join(plating)
            .OnComplete(() =>
            {
                _highlight.gameObject.SetActive(false);
                DOTween.Kill(_iconScalingLoopAnimKill, true);
            })
            .Pause();

        return _completeBoostersUnhighlightSequence;
    }

    private void BoostersIconScaling()
    {
        _icon.DOScale(1.15f, 1.0f).SetDelay(0.2f).SetLoops(-1, LoopType.Yoyo).SetId(_iconScalingLoopAnimKill);
    }

    #endregion
}
