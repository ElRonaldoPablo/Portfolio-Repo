using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UbisoftConnectButton : MonoBehaviour
{
    [Header("UbiConnect")]
    [SerializeField] private GameObject _button;
    [SerializeField] private Image _highlight;
    [SerializeField] private CanvasGroup _background;
    [SerializeField] private RectTransform _ubiString;
    [SerializeField] private RectTransform _connectString;
    [SerializeField] private RectTransform _lightSweep;
    [SerializeField] private Image _arrowBG;

    private Sequence _completeUbiConnectHighlightSequence;
    private Sequence _completeUbiConnectUnhighlightSequence;

    #region Ubisoft Connect Button

    private void InitializeUbisoftConnectButton()
    {
        _arrowBG.color = Color.black;
        _highlight.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _highlight.transform.localScale = new Vector3(1.08f, 1.2f, 2.0f);
        _ubiString.localPosition = new Vector3(0.0f, 14.5f, 0.0f);
        _connectString.localPosition = new Vector3(0.0f, -9.5f, 0.0f);

        _highlight.gameObject.SetActive(false);
        _lightSweep.transform.localPosition = new Vector3(-168.0f, 10.0f, 0.0f);
    }

    public void PlayUbiConnectButtonHighlightSequence()
    {
        UbiConnectButtonHighlightSequence().Play();
        //QueueButtonFillFading();
    }

    public void PlayUbiConnectButtonUnhighlightSequence()
    {
        UbiConnectButtonUnhighlightSequence().Play();
    }

    private Sequence UbiConnectButtonHighlightSequence()
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
            .Append(_lightSweep.DOLocalMoveX(242.0f, 0.5f))
            .Pause();

        Sequence highlightedBG = DOTween.Sequence();
        highlightedBG
            .Append(_background.DOFade(1.0f, 0.5f))
            .Pause();

        Sequence arrowBackground = DOTween.Sequence();
        arrowBackground
            .Append(_arrowBG.DOColor(Color.white, 0.0f))
            .Pause();

        Sequence ubisoftConnect = DOTween.Sequence();
        ubisoftConnect
            .Append(_ubiString.DOLocalMoveX(-9.0f, 0.2f))
            .Join(_connectString.DOLocalMoveX(25.0f, 0.2f))
            .Pause();

        Sequence ubisoftConnectRevert = DOTween.Sequence();
        ubisoftConnectRevert
            .Append(_ubiString.DOLocalMoveX(0.0f, 0.2f))
            .Join(_connectString.DOLocalMoveX(0.0f, 0.2f))
            .Pause();

        _completeUbiConnectHighlightSequence = DOTween.Sequence();
        _completeUbiConnectHighlightSequence
            .AppendCallback(InitializeUbisoftConnectButton)
            .Append(highlight)
            .Join(lightSweep)
            .Join(ubisoftConnect)
            .Join(highlightedBG)
            .Join(arrowBackground)
            .AppendInterval(3.0f)
            .Append(ubisoftConnectRevert)
            .Pause();

        return _completeUbiConnectHighlightSequence;
    }

    private Sequence UbiConnectButtonUnhighlightSequence()
    {
        Sequence unhighlight = DOTween.Sequence();
        unhighlight
            .Append(_highlight.rectTransform.DOScale(new Vector3(1.06f, 1.26f, 2.0f), 0.2f))
            .Join(_highlight.DOFade(0.0f, 0.2f))
            .Pause();

        Sequence highlightedBG = DOTween.Sequence();
        highlightedBG
            .Append(_background.DOFade(0.0f, 0.5f))
            .Pause();

        Sequence arrowBackground = DOTween.Sequence();
        arrowBackground
            .Append(_arrowBG.DOColor(Color.black, 0.0f))
            .Pause();

        Sequence ubisoftConnectRevert = DOTween.Sequence();
        ubisoftConnectRevert
            .Append(_ubiString.DOLocalMoveX(0.0f, 0.2f))
            .Join(_connectString.DOLocalMoveX(0.0f, 0.2f))
            .Pause();

        _completeUbiConnectUnhighlightSequence = DOTween.Sequence();
        _completeUbiConnectUnhighlightSequence
            .Append(unhighlight)
            .Join(highlightedBG)
            .Join(arrowBackground)
            .Join(ubisoftConnectRevert)
            .OnComplete(() =>
            {
                _highlight.gameObject.SetActive(false);
                DOTween.KillAll(true);
            })
            .Pause();

        return _completeUbiConnectUnhighlightSequence;
    }

    //private void RotatingCircles()
    //{
    //    _queueButtonFill.DOFade(0.25f, 2.0f).SetDelay(0.2f).SetLoops(-1, LoopType.Yoyo);
    //}

    #endregion

}
