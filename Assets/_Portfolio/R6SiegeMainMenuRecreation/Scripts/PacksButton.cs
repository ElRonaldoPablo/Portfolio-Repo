using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PacksButton : MonoBehaviour
{
    [Header("Packs")]
    [SerializeField] private GameObject _packsButton;
    [SerializeField] private Image _packsButtonHighlight;
    [SerializeField] private RectTransform _packsButtonLightSweep;
    [SerializeField] private RectTransform _packsButtonIcon;

    private Sequence _completePacksHighlightSequence;
    private Sequence _completePacksUnhighlightSequence;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    #region Packs Button

    private void InitializePacksButton()
    {
        _packsButtonHighlight.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _packsButtonHighlight.transform.localScale = new Vector3(1.08f, 1.2f, 2.0f);

        _packsButtonHighlight.gameObject.SetActive(false);
        _packsButtonLightSweep.transform.localPosition = new Vector3(-168.0f, 10.0f, 0.0f);
    }

    public void PlayPacksButtonHighlightSequence()
    {
        PacksButtonHighlightSequence().Play();
    }

    public void PlayPacksButtonUnhighlightSequence()
    {
        PacksButtonUnhighlightSequence().Play();
    }

    private Sequence PacksButtonHighlightSequence()
    {
        Sequence highlight = DOTween.Sequence();
        highlight
            .AppendCallback(() => _packsButtonHighlight.gameObject.SetActive(true))
            .Append(_packsButtonHighlight.rectTransform.DOScale(1.0f, 0.2f))
            .Join(_packsButtonHighlight.DOFade(1.0f, 0.2f))
            .Pause();

        Sequence lightSweep = DOTween.Sequence();
        lightSweep
            .AppendInterval(0.15f)
            .Append(_packsButtonLightSweep.DOLocalMoveX(177.0f, 0.5f))
            .Pause();

        Sequence icon = DOTween.Sequence();
        icon
            .Append(_packsButtonIcon.DOLocalRotate(new Vector3(0.0f, -90.0f, 0.0f), 0.25f))
            .Append(_packsButtonIcon.DOLocalRotate(Vector3.zero, 0.25f))
            .Append(_packsButtonIcon.DOLocalRotate(new Vector3(0.0f, -90.0f, 0.0f), 0.25f))
            .Append(_packsButtonIcon.DOLocalRotate(Vector3.zero, 0.25f))
            .Pause();

        _completePacksHighlightSequence = DOTween.Sequence();
        _completePacksHighlightSequence
            .AppendCallback(InitializePacksButton)
            .Append(highlight)
            .Join(lightSweep)
            .Join(icon)
            .Pause();

        return _completePacksHighlightSequence;
    }

    private Sequence PacksButtonUnhighlightSequence()
    {
        Sequence unhighlight = DOTween.Sequence();
        unhighlight
            .Append(_packsButtonHighlight.rectTransform.DOScale(new Vector3(1.08f, 1.2f, 2.0f), 0.2f))
            .Join(_packsButtonHighlight.DOFade(0.0f, 0.2f))
            .Pause();

        _completePacksUnhighlightSequence = DOTween.Sequence();
        _completePacksUnhighlightSequence
            .Append(unhighlight)
            .OnComplete(() =>
            {
                _packsButtonHighlight.gameObject.SetActive(false);
                DOTween.Kill(true);
            })
            .Pause();

        return _completePacksUnhighlightSequence;
    }

    #endregion
}
