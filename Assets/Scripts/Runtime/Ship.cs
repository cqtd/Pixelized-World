using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Pixelo
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private DefaultBlockSlot[] m_defaultEngine = default;
        [SerializeField] private Transform m_bodyRoot = default;
        [SerializeField] private GameObject m_explosion = default;
        
        public float multiplier = 100f;
        private List<Collider2D> colliders;
        public Module root;

        public bool isAlive;
        public bool isInvincible;

        public Action<EnemyDefinition> onAttackEnemy;

        private void Start()
        {
            isAlive = true;
            colliders = new List<Collider2D>();
            
            foreach (DefaultBlockSlot defaultBlockSlot in m_defaultEngine)
            {
                AttachBlock(defaultBlockSlot, m_bodyRoot);
            }
        }

        private Module lastAttached;

        public void Attach(BlockDefinition def)
        {
            // var socket = new DefaultBlockSlot()
            // {
            //     x = lastAttached.x > 0 ? lastAttached.x + 1 : lastAttached.x - 1,
            //     y = lastAttached.y > 0 ? lastAttached.y + 1 : lastAttached.y - 1,
            // };
            //
            // AttachBlock(socket, lastAttached.transform, lastAttached);
        }

        private Module AttachBlock(DefaultBlockSlot slot, Transform parent, Module owner = null)
        {
            Module instance = Instantiate(slot.definition.prefab, parent).GetComponent<Module>();
            instance.parent = owner;
            instance.children = new List<Module>();
            instance.transform.localPosition = new Vector3(Constant.PLAYER_SIZE * slot.x, Constant.PLAYER_SIZE * slot.y, 0f);
            instance.gameObject.layer = 7;
            instance.x = slot.x;
            instance.y = slot.y;

            lastAttached = instance;
            
            if (owner == null)
            {
                root = instance;
            }

            colliders.Add(instance.GetComponent<Collider2D>());

            for (int i = 0; i < slot.children.Length; i++)
            {
                DefaultBlockSlot child = slot.children[i];
                instance.children.Add(AttachBlock(child, instance.transform, instance));
            }

            return instance;
        }
        
        public void DetachModule(Module instance)
        {
            instance.children.Remove(instance);
            var cols = instance.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col2d in cols)
            {
                colliders.Remove(col2d);
            }

            // Instantiate(m_explosion, instance.transform.position, Quaternion.identity);
            Destroy(instance.gameObject);
        }

        public void SetInvincible(bool invincible)
        {
            isInvincible = invincible;
            
            // foreach (Collider2D collider2D1 in colliders)
            // {
            //     collider2D1.enabled = !invincible;
            // }
        }

        public void GameOver()
        {
            DetachModule(root);
            
            Instantiate(m_explosion, transform.position, Quaternion.identity);
            isAlive = false;
            
            Game.instance.onGameOver?.Invoke();

#if UNITY_EDITOR
            // Time.timeScale = 0f;
#else
            // UnityEngine.SceneManagement.SceneManager.LoadScene(0);
#endif
        }
    }
}
