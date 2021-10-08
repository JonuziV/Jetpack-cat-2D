using System;
using UnityEngine;

namespace Tabtale.Services.Tests
{
    /// <summary>
    /// Simple test to see how the ad service works.
    /// </summary>
    public class AdTest
        : MonoBehaviour
    {
        public void Start()
        {
            Debug.Log("AdTest::Showing Ad");
            AdService.Instance.AdVisible += HandleAdShown;
            AdService.Instance.AdClosed += HandleAdClosed;
            AdService.Instance.Show();
        }

        private void HandleAdShown(object sender, EventArgs args)
        {
            Debug.Log("AdTest::Handling Ad Visible");
        }

        private void HandleAdClosed(object sender, EventArgs args)
        {
            Debug.Log("AdTest::Handling Ad Closed");
        }
    }
}
