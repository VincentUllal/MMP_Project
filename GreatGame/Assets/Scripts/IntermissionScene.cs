using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MMP.Mechanics
{
    public class IntermissionScene : MonoBehaviour
    {
        private bool IntermissionOn = false;

        [SerializeField] GameObject Background, Buttons, DeathScene, PauseScene, Player, UI_Layer;

        public void Abort()
        {
            SceneManager.LoadScene("MenuScene");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(IntermissionOn)
                {
                    CloseIntermission();
                }
                else
                {
                    IntermissionSelect(1);
                }
            }
        }

        public void Continue()
        {
            if(DeathScene.activeSelf)
                RevivePlayer();
            CloseIntermission();
        }

        public void IntermissionSelect(int Nr)
        {
            IntermissionOn = true;
            Player.GetComponent<PlayerController>().controlEnabled = false;
            UI_Layer.SetActive(false);
            Background.SetActive(true);
            Buttons.SetActive(true);
            switch (Nr)
            {
                case 0:
                    DeathScene.SetActive(true);
                    break;
                case 1:
                    PauseScene.SetActive(true);
                    break;
                default:
                    Debug.Log("WARNING: Wrong number send to Scene(int)");
                    CloseIntermission();
                    break;
            }
        }
        void CloseIntermission()
        {
            Player.GetComponent<PlayerController>().controlEnabled = true;
            DeathScene.SetActive(false);
            PauseScene.SetActive(false);
            Background.SetActive(false);
            Buttons.SetActive(false);
            UI_Layer.SetActive(true);
            IntermissionOn = false;
        }

        public void RevivePlayer() // allows external access
        {
            Vector3 newPosition = Player.GetComponent<PlayerController>().respawnPoint;
            Player.transform.SetPositionAndRotation(newPosition, new Quaternion());
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2();

            Player.GetComponent<PlayerHealth>().SetHealth(100);
        }
    }
}
