using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class NewsButton : MonoBehaviour
{
    [Header("News")]
    [SerializeField] private GameObject _button;
    [SerializeField] private Image _highlight;

    [SerializeField] private RectTransform _circleHighlight;
    [SerializeField] private Image _radialBar;

    [SerializeField] private RectTransform _contentsParent;
    [SerializeField] private Image _activeContent;
    [SerializeField] private Image _nextContent;
    [SerializeField][Range(0.0f, 12.0f)] private float _cycleDuration = 3.0f;
    [SerializeField] private float _timer;
    private bool _isReadyToCycle;
    private bool _isPaused = false;

    [SerializeField] private RectTransform _tabsParent;
    [SerializeField] private RectTransform _activeTab;
    [SerializeField] private RectTransform _nextTab;

    private Sequence _completeNewsHighlightSequence;
    private Sequence _completeNewsUnhighlightSequence;
    private Sequence _completeNewsIdleCyclingSequence;

    void Start()
    {
        _activeTab = _tabsParent.GetChild(2).GetComponent<RectTransform>();
        _nextTab = _tabsParent.GetChild(1).GetComponent<RectTransform>();
        _circleHighlight.localPosition = _activeTab.localPosition;

        _activeContent = _contentsParent.GetChild(2).GetComponent<Image>();
        _nextContent = _contentsParent.GetChild(1).GetComponent<Image>();

        _timer = 0.0f;
        _isReadyToCycle = false;
    }

    void Update()
    {
        if (!_isPaused)
        {
            _timer += Time.deltaTime;
        }

        if (_timer >= _cycleDuration)
        {
            _isReadyToCycle = true;
        }

        if (_isReadyToCycle)
        {
            PlayNewsContentCycling();
            _isReadyToCycle = false;
        }

        RadialBarPercentage();
    }

    #region News Button

    private void RadialBarPercentage()
    {
        var _circle = _timer / _cycleDuration;
        _radialBar.fillAmount = _circle;
    }

    public void PlayNewsContentCycling()
    {
        IdleNewsContentCycling().Play();
    }

    private void PauseNewsContentCycling()
    {
        _isPaused = true;
    }

    private void UnpauseNewsContentCycling()
    {
        _isPaused = false;
    }

    private Sequence IdleNewsContentCycling()
    {
        _activeTab = _tabsParent.GetChild(2).GetComponent<RectTransform>();
        _nextTab = _tabsParent.GetChild(1).GetComponent<RectTransform>();
        _circleHighlight.localPosition = _nextTab.localPosition;

        _activeContent = _contentsParent.GetChild(2).GetComponent<Image>();
        _nextContent = _contentsParent.GetChild(1).GetComponent<Image>();

        Sequence updateContent = DOTween.Sequence();
        updateContent
            .Append(_activeContent.DOFade(0.0f, 0.3f))
            .Join(_nextContent.rectTransform.DOLocalMoveX(0.0f, 0.3f))
            .Join(_nextContent.DOFade(1.0f, 0.3f))
            .Pause();

        _completeNewsIdleCyclingSequence = DOTween.Sequence();
        _completeNewsIdleCyclingSequence
            .AppendCallback(()=> _timer = 0.0f)
            .Append(updateContent)
            .OnComplete(() =>
            {
                _activeContent.rectTransform.localPosition = new Vector3(536.0f, 0.0f, 0.0f);
                _activeContent.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
                _activeContent.rectTransform.SetAsFirstSibling();
                _activeTab.SetAsFirstSibling();
            })
            .Pause();

        return _completeNewsIdleCyclingSequence;
    }

    private void InitializeNewsButton()
    {
        _highlight.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _highlight.transform.localScale = new Vector3(1.05f, 1.125f, 2.0f);
        _highlight.gameObject.SetActive(false);
    }

    public void PlayNewsButtonHighlightSequence()
    {
        NewsButtonHighlightSequence().Play();
        PauseNewsContentCycling();
    }

    public void PlayNewsButtonUnhighlightSequence()
    {
        NewsButtonUnhighlightSequence().Play();
        UnpauseNewsContentCycling();
    }

    private Sequence NewsButtonHighlightSequence()
    {
        Sequence highlight = DOTween.Sequence();
        highlight
            .AppendCallback(() => _highlight.gameObject.SetActive(true))
            .Append(_highlight.rectTransform.DOScale(1.0f, 0.2f))
            .Join(_highlight.DOFade(1.0f, 0.2f))
            .Pause();

        _completeNewsHighlightSequence = DOTween.Sequence();
        _completeNewsHighlightSequence
            .AppendCallback(InitializeNewsButton)
            .Append(highlight)
            .Pause();

        return _completeNewsHighlightSequence;
    }

    private Sequence NewsButtonUnhighlightSequence()
    {
        Sequence unhighlight = DOTween.Sequence();
        unhighlight
            .Append(_highlight.rectTransform.DOScale(new Vector3(1.05f, 1.125f, 2.0f), 0.2f))
            .Join(_highlight.DOFade(0.0f, 0.2f))
            .Pause();

        _completeNewsUnhighlightSequence = DOTween.Sequence();
        _completeNewsUnhighlightSequence
            .Append(unhighlight)
            .OnComplete(() =>
            {
                _highlight.gameObject.SetActive(false);
                DOTween.Kill(true);
            })
            .Pause();

        return _completeNewsUnhighlightSequence;
    }

    #endregion
}
