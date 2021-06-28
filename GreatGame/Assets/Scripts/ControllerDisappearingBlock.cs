using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MMP.Mechanics
{
    public class ControllerDisappearingBlock : MonoBehaviour
    {
        public bool initialVisibilityOfBlue = true;
        public bool initialVisibilityOfRed = false;

        private PlayerController playerScript;

        [SerializeField] GameObject TilemapBlue;
        [SerializeField] GameObject TilemapRed;

        void Start()
        {
            if (initialVisibilityOfBlue != TilemapBlue.activeSelf)
                TilemapBlue.SetActive(!TilemapBlue.activeSelf);

            if (initialVisibilityOfRed != TilemapRed.activeSelf)
                TilemapRed.SetActive(!TilemapRed.activeSelf);

            playerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        }

        void Update()
        {
            if (
                Input.GetButtonDown("Jump") &&
                  (
                    (playerScript.jumpState == PlayerController.JumpState.Grounded) ||
                    (playerScript.jumpState == PlayerController.JumpState.PrepareToJump)
                  )
               )

            {
                TilemapBlue.SetActive(!TilemapBlue.activeSelf);
                TilemapRed.SetActive(!TilemapRed.activeSelf);
            }
        }
    }
}