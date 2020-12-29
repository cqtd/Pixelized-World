using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pixelo
{
    public class InputHandler : MonoBehaviour
    {
        public float keyboardMovementSpeed = 3f;
        
        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 input = Vector3.ClampMagnitude(new Vector3(horizontal, vertical, 0), 1.0f);
            Vector3 velocity = input * keyboardMovementSpeed;

            GetComponent<Rigidbody2D>().velocity = velocity;

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
