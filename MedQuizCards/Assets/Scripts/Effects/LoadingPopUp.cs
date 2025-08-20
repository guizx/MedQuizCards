
using Nato.Singleton;
using UnityEngine;

namespace MedQuizCards
{
    public class LoadingPopUp : Singleton<LoadingPopUp>
    {
        [SerializeField] private GameObject loadingPanel;

        public void Show()
        {
            loadingPanel.SetActive(true);
        }

        public void Hide()
        {
            loadingPanel.SetActive(false);
        }
    }
}
