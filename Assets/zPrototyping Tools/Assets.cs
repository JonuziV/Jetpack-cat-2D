/* 
    These files were assembled by Veton to be used for developing prototypes
    In case you're reading this... well... Hello! :D 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototyping {

    /*
     * Global Asset references
     * Edit Asset references in the prefab Utilities/Resources/PrototypingAssets
     * */
    public class Assets : MonoBehaviour {

        // Internal instance reference
        private static Assets _i; 

        // Instance reference
        public static Assets i {
            get {
                if (_i == null) _i = Instantiate(Resources.Load<Assets>("PrototypingAssets")); 
                return _i; 
            }
        }


        // All references
        
        public Sprite s_White;
        public Sprite s_Circle;
        public Material m_White;

    }

}
