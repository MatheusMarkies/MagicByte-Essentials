using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics
{
    public class SpringCollider : MonoBehaviour
    {
        public float ColliderRadius;

        public void setColliderRadius(float radius)
        {
            this.ColliderRadius = radius;
        }
        public float getColliderRadius()
        {
            return this.ColliderRadius;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, ColliderRadius);
        }
    }
}