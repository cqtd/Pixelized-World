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

        private bool pause = false;
        
        private void Update()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                if (pause)
                {
                    Time.timeScale = 1.0f;
                }
                else
                {
                    Time.timeScale = 0.0f;
                }

                pause = !pause;
            }
#endif

            if (!Game.instance.ship.isAlive)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }
            
            ProcessInput();
        }

        /// <summary>
        /// 키보드 인풋 매니징
        /// </summary>
        private void ProcessInput()
        {
            if (Game.instance.IsPaused)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }
            
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            Vector3 input = Vector3.ClampMagnitude(new Vector3(horizontal, vertical, 0), 1.0f);
            Vector3 velocity = input * keyboardMovementSpeed;

            if (isDashing)
            {
                velocity *= dash;
                _dashed += velocity.magnitude * Game.deltaTime;

                if (_dashed > dashMax)
                {
                    isDashing = false;
                    _dashed = 0;
                    
                    Game.instance.ship.SetInvincible(false);
                }
            }
            
            _rigidbody.velocity = velocity;
            
            
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                Dash();
            }
        }

        /// <summary>
        ///  대쉬 실행
        /// </summary>
        private void Dash()
        {
            if (!canDash) return;

            StartCoroutine(DashCoroutine());
        }

        /// <summary>
        /// 대쉬 코루틴
        /// </summary>
        /// <returns></returns>
        private IEnumerator DashCoroutine()
        {
            Game.instance.onDashCooldown?.Invoke();

            canDash = false;
            isDashing = true;
            
            Game.instance.ship.SetInvincible(true);

            // cooldown
            yield return new WaitForSeconds(2.0f);
            canDash = true;
        }
    }
}
