using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics
{
    public class SpringModule : MonoBehaviour
    {

        Transform Referential;
        public Transform Child;

        Vector3 OldPosition;
        Vector3 CurrentPosi;

        public SpringCollider[] colliders;
        float radius = 0.05f;

        public float Drag = 0.4f;
        float SpringLength_;
        float Dynamic = 1f;
        public float rigidity = 0.01f;

        Quaternion localRotation;
        public Vector3 Axis = new Vector3(-1.0f, 0.0f, 0.0f);

        public Vector3 Forces = new Vector3(0.0f, -0.0001f, 0.0f);

        bool useGravity;
        Vector3 Gravity = new Vector3(0, -9.8f, 0);//(0,-9.8f,0)

        // Start is called before the first frame update
        void Start()
        {
            Referential = transform;

            OldPosition = Child.position;
            CurrentPosi = Child.position;

            SpringLength_ = Vector3.Distance(Referential.position, Child.position);
            localRotation = transform.localRotation;
        }

        // Update is called once per frame
        void Update()
        {
        VerletIntegration();
        }

        public void VerletIntegration()
        {

            Transform noChangesReferential = Referential;
            Referential.localRotation = Quaternion.identity * localRotation;

            float sqrDt = Time.deltaTime * Time.deltaTime;//Delta Time to Square

            Vector3 VerletForces = Referential.rotation * (Axis * rigidity) / sqrDt;//Deformation Force Power [W] * Referential Rotation Vector [->]
            VerletForces += (CurrentPosi - OldPosition) * Drag / sqrDt;//Air Drag Force Power [W]
            VerletForces += Forces / sqrDt;//Tangent Forces Power [W]

            if (useGravity)
                VerletForces += Gravity / sqrDt;//Gravity Impulse[N] (CurrentPosi - OldPosition) * Gravity / sqrDt;//Gravity Force Power [W]

            Vector3 temp = CurrentPosi;

            CurrentPosi = (CurrentPosi - OldPosition) + CurrentPosi + (VerletForces * sqrDt);//Verlet Integration * Impulse Force[N]
            CurrentPosi = ((CurrentPosi - Referential.position).normalized * SpringLength_) + Referential.position;//Normalize Child Position to Referencial Vector Diretion [->]

            //Collision Calculation
            for (int i = 0; i < colliders.Length; i++)
                if (Vector3.Distance(CurrentPosi, colliders[i].transform.position) <= (radius + colliders[i].getColliderRadius()))
                {
                    Vector3 normal = (CurrentPosi - colliders[i].transform.position).normalized;
                    CurrentPosi = colliders[i].transform.position + (normal * (radius + colliders[i].getColliderRadius()));
                    CurrentPosi = ((CurrentPosi - Referential.position).normalized * SpringLength_) + Referential.position;
                }

            OldPosition = CurrentPosi;

            Vector3 aimVector = Referential.TransformDirection(Axis);//Fix Object Referential to World Position
            Quaternion aimRotation = Quaternion.FromToRotation(aimVector, CurrentPosi - Referential.position);
            Quaternion secondaryRotation = aimRotation * Referential.rotation;
            Referential.rotation = Quaternion.Lerp(noChangesReferential.rotation, secondaryRotation, Dynamic);

        }

        public void setForces(Vector3 forces)
        {
            this.Forces = forces;
        }
        public void setDynamic(float dynamic)
        {
            this.Dynamic = dynamic;
        }
        public void setUseGravity(bool useGravity)
        {
            this.useGravity = useGravity;
        }
        public void setGravity(Vector3 gravity)
        {
            this.Gravity = gravity;
        }
        public void setColliders(SpringCollider[] colliders)
        {
            this.colliders = colliders;
        }

    }
}