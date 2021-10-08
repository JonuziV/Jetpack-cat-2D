using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Tabtale.Services
{
    /// <summary>
    /// Provides the public interface of the advert service package.
    /// </summary>
    public class AdService
        : MonoBehaviour
    {
        public static AdService Instance { get { return FindService(); } }
        public event EventHandler AdVisible;
        public event EventHandler AdClosed;
        public bool AdShowing { get; private set; }

        public void Show()
        {
            if (AdShowing)
                return;

            AdShowing = true;
            StartCoroutine(ShowAdvert());

            if (AdVisible != null)
                AdVisible(this, EventArgs.Empty);
        }

        private IEnumerator ShowAdvert()
        {
            _adCanvas.SetActive(true);
            yield return new WaitWhile(() => _adCanvas.activeSelf);

            AdShowing = false;
            if (AdClosed != null)
                AdClosed(this, EventArgs.Empty);
        }

        private static AdService FindService()
        {
            if (_service != null)
                return _service;

            Debug.Log("AdService::Initializing");
            if (EventSystem.current == null)
            {
                Debug.LogWarning("EventSystem is missing; loading default");
                Instantiate(Resources.Load<EventSystem>("EventSystem"));
            }

            var service = Instantiate(Resources.Load<GameObject>("AdService"));
            service.name = "AdService";
            _service = service.GetComponent<AdService>();
            return _service;
        }

        private static AdService _service;
        [SerializeField] private GameObject _adCanvas;
    }
}