using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pixelo
{
    public class Pawn : MonoBehaviour
    {
        [SerializeField] private DefaultBlockSlot[] m_defaultEngine = default;
        [SerializeField] private Transform m_bodyRoot = default;
        [SerializeField] private GameObject m_explosion = default;
        
        public float multiplier = 100f;

        private List<Collider2D> colliders;

        public BlockInstance root;
        
        private BlockInstance AttachBlock(DefaultBlockSlot slot, Transform parent, BlockInstance owner = null)
        {
            BlockInstance instance = Instantiate(slot.definition.prefab, parent).GetComponent<BlockInstance>();
            instance.parent = owner;
            instance.children = new List<BlockInstance>();
            instance.transform.localPosition = new Vector3(0.16f * slot.x, 0.16f * slot.y, 0f);
            instance.gameObject.layer = 7;
            
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

        private void Start()
        {
            colliders = new List<Collider2D>();
            
            foreach (DefaultBlockSlot defaultBlockSlot in m_defaultEngine)
            {
                AttachBlock(defaultBlockSlot, m_bodyRoot);
            }
        }

        public Collider2D GetClosestModule(Vector3 position)
        {
            var min = float.MaxValue;
            Collider2D minCollider = null;

            foreach (Collider2D collider2D1 in colliders)
            {
                var evaluate = Vector3.SqrMagnitude(collider2D1.transform.position - position);
                if (evaluate < min)
                {
                    min = evaluate;
                    minCollider = collider2D1;
                }
            }

            return minCollider;
        }

        public void DetachModule(BlockInstance instance)
        {
            instance.parent.children.Remove(instance);
            var cols = instance.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D col2d in cols)
            {
                colliders.Remove(col2d);
            }
            
            Destroy(instance.gameObject);
        }

        public void GameOver()
        {
            Instantiate(m_explosion, transform.position, Quaternion.identity);
            
#if UNITY_EDITOR
            // Time.timeScale = 0f;
#else            
            SceneManager.LoadScene(0);
#endif
        }
    }
}
