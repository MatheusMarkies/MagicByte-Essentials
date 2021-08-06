using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics
{
    public class SpringManager : MonoBehaviour
    {
        public SpringModule[] Modules;
        public float Dynamic;

        public bool useGravity;
        public Vector3 Gravity = new Vector3(0, -0.9f, 0);

        // Start is called before the first frame update
        void Start()
        {
            Modules = GetComponentsInChildren<SpringModule>();
            foreach (SpringModule sb in Modules)
            {
                sb.setDynamic(Dynamic);
                sb.setColliders(GetComponentsInChildren<SpringCollider>());
            }
        }

        private void LateUpdate()
        {
            if (Dynamic != 0.0f)
                for (int i = 0; i < Modules.Length; i++)
                {
                    Modules[i].VerletIntegration();
                    if (useGravity)
                    {
                        Modules[i].setUseGravity(useGravity);
                        Modules[i].setGravity(Gravity);
                    }
                }
        }
        public SpringModule[] getModules()
        {
            return this.Modules;
        }
    }
}