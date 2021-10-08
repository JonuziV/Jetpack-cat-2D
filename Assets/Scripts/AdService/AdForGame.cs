using System;
using UnityEngine;

namespace Tabtale.Services.Tests{
    /// <summary>
    /// Simple test to see how the ad service works.
    /// </summary>
    public class AdForGame: MonoBehaviour{
        private void Start(){
            Debug.Log("AdForGame: Showing");
            AdService.Instance.AdVisible += HandleAdShown;
            AdService.Instance.AdClosed += HandleAdClosed;
            //Show();
        }

        private void HandleAdShown(object sender, EventArgs args){
            Debug.Log("AdForGame: Waiting for Respone");
        }

        private void HandleAdClosed(object sender, EventArgs args){
            Debug.Log("AdForGame: Closing");
        }

        public void Show(){
            AdService.Instance.Show();
        }
    }
}