using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public UIAnimator uiAnimator;
    public RectTransform mainMenuPanel;
    public RectTransform[] panels;

    private RectTransform currentPanel;
    private RectTransform previousPanel;

    [Header("Start Screen")]
    public GameObject StartScreen;
    public GameObject TicTacToeLogo;
    [SerializeField] private RectTransform PlayBtn;
    [SerializeField] private RectTransform AccountBtn;
    [SerializeField] private RectTransform LeaderboardBtn; 
    [SerializeField] private RectTransform SettingsBtn, StoreBtn;

    [Header("Account")]
    public RectTransform AccountPanel;

    [Header("Store")]
    public GameObject StorePanel;

    [Header("Settings")]
    public RectTransform SettingsPanel;

    [Header("Game Selection Screen")]
    public GameObject GameSelectionScreen;
    [SerializeField] private RectTransform TopBtns;
    [SerializeField] private GameObject TitleScreen;
    [SerializeField] private RectTransform PVPBtn;
    [SerializeField] private RectTransform PVCBtn;
    [SerializeField] private RectTransform PVPOBtn;

    [Header("LeaderBoard Screen")]
    int Close = 0;
    public GameObject LeaderBoardPannel;
    [SerializeField] private RectTransform LeaderBoardTopBtns;
    [SerializeField] private GameObject ChildLeaderBoardPannel;

    private void Start()
    {
        // Hide all panels except the main menu panel
        //foreach (var panel in panels)
        //{
        //    uiAnimator.AnimatePanelOut(panel);
        //}
        ShowStartPannel();
       // ShowPanel(mainMenuPanel); // Show the main menu panel at start
    }

    public void ShowStartPannel()
    {
        SoundManager.Instacne.PlayPopEffect();
        TicTacToeLogo.transform.DOScale(1, .3f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
        {
            SoundManager.Instacne.PlayTransitionEffect();
            PlayBtn.transform.DOScale(1, .15f).SetEase(Ease.OutBack).SetUpdate(false);
            AccountBtn.transform.DOScale(1, .15f).SetEase(Ease.OutBack).SetUpdate(false);
            LeaderboardBtn.transform.DOScale(1, .15f).SetEase(Ease.OutBack).SetUpdate(false);
            PlayBtn.DOAnchorPosX(0, .3f).SetEase(Ease.InBounce).SetUpdate(false).OnComplete(() =>
            {
                SoundManager.Instacne.PlayTransitionEffect();

                AccountBtn.DOAnchorPosX(0, .3f).SetEase(Ease.InBounce).SetUpdate(false).OnComplete(() =>
                {
                    SoundManager.Instacne.PlayTransitionEffect();

                    LeaderboardBtn.DOAnchorPosX(0, .3f).SetEase(Ease.InBounce).SetUpdate(false).OnComplete(() =>
                    {
                        SoundManager.Instacne.PlayPopEffect();

                        StoreBtn.transform.DOScale(0.7487897f, .2f).SetEase(Ease.InBounce).SetUpdate(false).OnComplete(() =>
                        {
                            SoundManager.Instacne.PlayPopEffect();

                            SettingsBtn.transform.DOScale(0.7487897f, .2f).SetEase(Ease.InBounce).SetUpdate(false);
                        });
                    });
                });
            });
        });
    }

    public void ShowPanel(RectTransform panel)
    {
        if (currentPanel != null && currentPanel != panel)
        {
            previousPanel = currentPanel;
            uiAnimator.AnimatePanelOut(currentPanel);
        }

        uiAnimator.AnimatePanelIn(panel);
        currentPanel = panel;
    }

    public void MainMenu()
    {
        //  ShowPanel(mainMenuPanel);
        CloseGameSelectionPannel();
    }

    public void PlaySelect()
    {
        //ShowPanel(panels[0]);

        CloseStartPannel();

    }

    public void CloseStartPannel()
    {
        SoundManager.Instacne.PlayPopEffect();
        SettingsBtn.transform.DOScale(0f, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
        {
            SoundManager.Instacne.PlayPopEffect();

            StoreBtn.transform.DOScale(0f, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
            {
                SoundManager.Instacne.PlayTransitionEffect();
                LeaderboardBtn.DOAnchorPosX(1167f, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
                {
                    SoundManager.Instacne.PlayTransitionEffect();
                    AccountBtn.DOAnchorPosX(1167f, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
                    {
                        SoundManager.Instacne.PlayTransitionEffect();
                        PlayBtn.DOAnchorPosX(1167f, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
                        {
                            TicTacToeLogo.transform.DOScale(0, .15f).SetEase(Ease.OutBack).SetUpdate(false);
                            PlayBtn.transform.DOScale(0, .15f).SetEase(Ease.OutBack).SetUpdate(false);
                            AccountBtn.transform.DOScale(0, .15f).SetEase(Ease.OutBack).SetUpdate(false);
                            LeaderboardBtn.transform.DOScale(0, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
                            ShowGameSelectionScreen());
                        });
                    });
                });
            });
        });
    }

    public void ShowGameSelectionScreen()
    {
        GameSelectionScreen.SetActive(true);
        SoundManager.Instacne.PlayTransitionEffect();

        TopBtns.DOAnchorPosY(-151f, .3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            SoundManager.Instacne.PlayPopEffect();

            TitleScreen.transform.DOScale(1, .2f).SetEase(Ease.InBack).OnComplete(() =>
            {
                SoundManager.Instacne.PlayTransitionEffect();

                PVPBtn.transform.DOScale(1, .15f).SetEase(Ease.OutBack).SetUpdate(false);
                PVCBtn.transform.DOScale(1, .15f).SetEase(Ease.OutBack).SetUpdate(false);
                PVPOBtn.transform.DOScale(1, .15f).SetEase(Ease.OutBack).SetUpdate(false);

                PVPBtn.DOAnchorPosX(0, .3f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    SoundManager.Instacne.PlayTransitionEffect();

                    PVCBtn.DOAnchorPosX(0, .3f).SetEase(Ease.InBack).OnComplete(() =>
                    {
                        SoundManager.Instacne.PlayTransitionEffect();

                        PVPOBtn.DOAnchorPosX(0, .3f).SetEase(Ease.InBack).SetUpdate(true);
                    });
                });
            });
        });
    }

    public void CloseGameSelectionPannel()
    {
        SoundManager.Instacne.PlayTransitionEffect();

        PVPOBtn.DOAnchorPosX(1168f, .15f).SetEase(Ease.InBack).OnComplete(() =>
        {
            SoundManager.Instacne.PlayTransitionEffect();

            PVCBtn.DOAnchorPosX(1168f, .15f).SetEase(Ease.InBack).OnComplete(() =>
            {
                SoundManager.Instacne.PlayTransitionEffect();

                PVPBtn.DOAnchorPosX(1168f, .15f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    SoundManager.Instacne.PlayPopEffect();

                    TitleScreen.transform.DOScale(0, .15f).SetEase(Ease.InBack).OnComplete(() =>
                    {
                        SoundManager.Instacne.PlayTransitionEffect();
                        PVPBtn.transform.DOScale(0, .15f).SetEase(Ease.OutBack).SetUpdate(false);
                        PVCBtn.transform.DOScale(0, .15f).SetEase(Ease.OutBack).SetUpdate(false);
                        PVPOBtn.transform.DOScale(0, .15f).SetEase(Ease.OutBack).SetUpdate(false);

                        TopBtns.DOAnchorPosY(277f, .15f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(()=>
                        ShowStartPannel());
                    });
                });
            });
        });
    }

    public void ShowLeaderBoardPannel()
    {
        LeaderBoardPannel.SetActive(true);
        SoundManager.Instacne.PlayTransitionEffect();
        LeaderBoardTopBtns.DOAnchorPosY(-154f, .3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            SoundManager.Instacne.PlayPopEffect();

            ChildLeaderBoardPannel.transform.DOScale(1, .2f).SetEase(Ease.InBack);
        });
    }

    public void PlayerVsPlayer()
    {
        ShowPanel(panels[1]);
    }

    public void PlayerVsCOM()
    {
        ShowPanel(panels[2]);
    }

    public void PlayerVsOnline()
    {
        ShowPanel(panels[3]);
    }

    public void LeaderboardFromGameSelection()
    {
        Close = 1;
        SoundManager.Instacne.PlayTransitionEffect();

        PVPOBtn.DOAnchorPosX(1168f, .15f).SetEase(Ease.InBack).OnComplete(() =>
        {
            SoundManager.Instacne.PlayTransitionEffect();

            PVCBtn.DOAnchorPosX(1168f, .15f).SetEase(Ease.InBack).OnComplete(() =>
            {
                SoundManager.Instacne.PlayTransitionEffect();

                PVPBtn.DOAnchorPosX(1168f, .15f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    SoundManager.Instacne.PlayPopEffect();

                    TitleScreen.transform.DOScale(0, .15f).SetEase(Ease.InBack).OnComplete(() =>
                    {
                        SoundManager.Instacne.PlayTransitionEffect();

                        TopBtns.DOAnchorPosY(277f, .15f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() =>
                        ShowLeaderBoardPannel()
                        );
                    });
                });
            });
        });
    }

    public void LeaderboardFromStartPannel()
    {
        Close = 0;
        SoundManager.Instacne.PlayPopEffect();
        SettingsBtn.transform.DOScale(0f, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
        {
            SoundManager.Instacne.PlayPopEffect();

            StoreBtn.transform.DOScale(0f, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
            {
                SoundManager.Instacne.PlayTransitionEffect();
                LeaderboardBtn.DOAnchorPosX(1167f, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
                {
                    SoundManager.Instacne.PlayTransitionEffect();
                    AccountBtn.DOAnchorPosX(1167f, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
                    {
                        SoundManager.Instacne.PlayTransitionEffect();
                        PlayBtn.DOAnchorPosX(1167f, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
                         TicTacToeLogo.transform.DOScale(0, .15f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
                         ShowLeaderBoardPannel())
                         );
                    });
                });
            });
        });
    }

    public void CloseLeaderBoard()
    {
        SoundManager.Instacne.PlayPopEffect();
        ChildLeaderBoardPannel.transform.DOScale(0, .15f).SetEase(Ease.OutBack).OnComplete(()=>
        {
            SoundManager.Instacne.PlayTransitionEffect();

            LeaderBoardTopBtns.DOAnchorPosY(235f, .15f).SetEase(Ease.OutBack).OnComplete(()=>
            {
                if (Close == 1)
                {
                    ShowGameSelectionScreen();
                }
                else
                {
                    ShowStartPannel();
                }
            });
        }); 
    }

    public void OpenAccount()
    {
        SoundManager.Instacne.PlayPopEffect();
        AccountPanel.gameObject.SetActive(true);
        AccountPanel.transform.DOScale(1, .3f).SetEase(Ease.OutBack).SetUpdate(false);
    }

    public void CloseAccount()
    {
        SoundManager.Instacne.PlayPopEffect();
        AccountPanel.transform.DOScale(0, .3f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
        AccountPanel.gameObject.SetActive(false));
    }

    public void OpenStore()
    {
        SoundManager.Instacne.PlayPopEffect();
        StorePanel.SetActive(true);
        StorePanel.transform.DOScale(1, .3f).SetEase(Ease.OutBack).SetUpdate(false);
    }

    public void CloseStore()
    {
        SoundManager.Instacne.PlayPopEffect();
        StorePanel.transform.DOScale(0, .3f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(() =>
        StorePanel.SetActive(false));
    }

    public void OpenSettings()
    {
        SettingsPanel.gameObject.SetActive(true);
        SoundManager.Instacne.PlayPopEffect();
        SettingsPanel.transform.DOScale(1, .3f).SetEase(Ease.OutBack).SetUpdate(false);
    }

    public void CloseSettings()
    {
        SettingsPanel.transform.DOScale(0, .3f).SetEase(Ease.OutBack).SetUpdate(false).OnComplete(()=>
        {
            SoundManager.Instacne.PlayPopEffect();
            SettingsPanel.gameObject.SetActive(false);
        });
    }

    public void Shop()
    {
        ShowPanel(panels[5]);
    }

    public void Account()
    {
        ShowPanel(panels[6]);
    }

    public void Settings()
    {
        ShowPanel(panels[7]);
    }

    public void GoBack()
    {
        if (previousPanel != null)
        {
            ShowPanel(previousPanel);
        }
    }
}
