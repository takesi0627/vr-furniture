using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hekira
{
    public class NetworkStateReporter : MonoBehaviour
    {
        const string WARNING_WINDOW = "Warning";
        GameObject m_WarningWindow;
        
        // Use this for initialization
        void Start()
        {
            // Set this object don't destroy
            DontDestroyOnLoad (this);

            // Get warning window reference from child
            m_WarningWindow = transform.Find(WARNING_WINDOW).gameObject;
            if (m_WarningWindow == null)
            {
                Debug.LogError("Cannot find warning window game object");
            }
            else
            {
                m_WarningWindow.SetActive(false);
            }


            // start check networkstate
            StartCoroutine(CheckNetworkState());
        }

        /// <summary>
        /// Repeatedly checks the state of the network every one second.
        /// </summary>
        /// <returns>The network state.</returns>
        IEnumerator CheckNetworkState () {
            while (true)
            {
                if (Application.internetReachability == NetworkReachability.NotReachable) 
                {
                    m_WarningWindow.SetActive(true);
                }

                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}