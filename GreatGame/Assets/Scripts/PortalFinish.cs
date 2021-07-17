using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MMP.Mechanics
{
    public class PortalFinish : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
}
