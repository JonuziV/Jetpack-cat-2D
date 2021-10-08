/* 

    These files were assembled by Veton to be used for developing prototypes
    In case you're reading this... well... Hello! :D 
 
 */
 
using System.Collections.Generic;
using UnityEngine;

namespace Prototyping.MonoBehaviours {

    /*
        Setup for CameraFollow, follows the transform with zoom
     */
    public class CameraFollowSetup : MonoBehaviour {

        [SerializeField] private CameraFollow cameraFollow = null;
        [SerializeField] private Transform followTransform = null;
        [SerializeField] private float zoom = 50f;

        private void Start() {
            if (followTransform == null) {
                Debug.LogError("followTransform is null! Intended?");
                cameraFollow.Setup(() => Vector3.zero, () => zoom, true, true);
            } else {
                cameraFollow.Setup(() => followTransform.position, () => zoom, true, true);
            }
        }
    }

}