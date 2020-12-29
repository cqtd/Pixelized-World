using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pixelo
{
    public class InputHandler : MonoBehaviour
    {
        public float keyboardMovementSpeed = 3f;
        public float dash = 10f;
        
        float _dashed = 0f;
        public float dashMax = 10f;

        private bool canDash;
        private bool isDashing;
        private Rigidbody2D _rigidbody;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            canDash = true;
        }

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 input = Vector3.ClampMagnitude(new Vector3(horizontal, vertical, 0), 1.0f);
            Vector3 velocity = input * keyboardMovementSpeed;

            if (isDashing)
            {
                velocity *= dash;
                _dashed += velocity.magnitude * Time.deltaTime;

                if (_dashed > dashMax)
                {
                    isDashing = false;
                    _dashed = 0;
                    
                    Game.instance.ship.SetInvincible(false);
                }
            }
            
            _rigidbody.velocity = velocity;

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                Dash();
            }
        }

        private void Dash()
        {
            if (!canDash) return;

            StartCoroutine(DashCoroutine());
        }


        private IEnumerator DashCoroutine()
        {
            canDash = false;
            isDashing = true;
            
            Game.instance.ship.SetInvincible(true);

            // cooldown
            yield return new WaitForSeconds(2.0f);
            canDash = true;
        }
    }
}
