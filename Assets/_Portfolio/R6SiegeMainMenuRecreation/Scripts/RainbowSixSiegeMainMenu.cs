using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class RainbowSixSiegeMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _splashPanel;
    [SerializeField] private Image _splashBackgroundImage;
    [SerializeField] private Image _splashFadeImage;
    [SerializeField] private TextMeshProUGUI _splashString;
    [SerializeField] private TextMeshProUGUI _projectDetailsString;

    private Sequence _completeSplashSequence;

    // Start is called before the first frame update
    private void Start()
    {
        InitializeRainbowMiniProject();
        PlaySplashSequence();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void InitializeRainbowMiniProject()
    {
        _splashBackgroundImage.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        _splashFadeImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _splashString.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _projectDetailsString.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

        _splashPanel.SetActive(true);

        Debug.Log("NOTICE | Initilization has been conducted.");
    }

    private void PlaySplashSequence()
    {
        SplashSequence().Play();
    }

    private Sequence SplashSequence()
    {
        Sequence fadeIn = DOTween.Sequence();
        fadeIn
            .AppendInterval(2.0f)
            .Append(_splashFadeImage.DOFade(1.0f, 2.0f))
            .AppendInterval(1.5f)
            .Append(_splashFadeImage.DOFade(0.0f, 2.0f))
            .Join(_splashString.DOFade(1.0f, 2.0f))
            .AppendInterval(3.0f)
            .Pause();

        Sequence fadeOut = DOTween.Sequence();
        fadeOut
            .Append(_splashFadeImage.DOFade(1.0f, 2.0f))
            .AppendInterval(1.5f)
            .Append(_splashString.DOFade(0.0f, 0.0f))
            .Append(_splashFadeImage.DOFade(0.0f, 2.0f))
            .Join(_splashBackgroundImage.DOFade(0.0f, 2.0f))
            .Pause();

        Sequence projectDetailsFadeIn = DOTween.Sequence();
        projectDetailsFadeIn
            .Append(_projectDetailsString.DOFade(1.0f, 2.0f))
            .Pause();

        _completeSplashSequence = DOTween.Sequence();
        _completeSplashSequence
            .Append(fadeIn)
            .Append(fadeOut)
            .Append(projectDetailsFadeIn)
            .Pause();

        return _completeSplashSequence;
    }
}
