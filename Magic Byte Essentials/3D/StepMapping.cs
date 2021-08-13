using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics
{
    public class StepMapping
    {
        public bool footIsGrounded(GameObject foot, float minGroundDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(foot.transform.position, -Vector3.up, out hit, minGroundDistance))
                return true;
            else
                return false;
        }
        class TightRope
        {
            public Vector3 nextStep(GameObject gameObject, GameObject foot, GameObject secondFoot, bool secondFootIsGrounded, float stepDistance, float groundDistance)
            {
                Vector3 step = new Vector3();
                int tightropeDetect = 10;

                RaycastHit hit;
                if (Physics.Raycast(new Vector3(foot.transform.position.x, gameObject.transform.position.y, foot.transform.position.z) + stepDistance * gameObject.transform.forward, -Vector3.up, out hit, groundDistance))
                {
                    step = foot.transform.position + stepDistance * foot.transform.forward;
                }
                else
                {
                    if (!secondFootIsGrounded)
                    {
                        Vector3 StartTightrope = new Vector3();
                        Vector3 EndTightrope = new Vector3();
                        int size = 0;
                        for (int i = -tightropeDetect; i < tightropeDetect; i++)
                        {
                            RaycastHit _hit;
                            if (Physics.Raycast(new Vector3(foot.transform.position.x, gameObject.transform.position.y, foot.transform.position.z) + stepDistance * gameObject.transform.forward + i * gameObject.transform.right, -Vector3.up, out _hit, groundDistance))
                            {
                                if (size == 0)
                                    StartTightrope = _hit.transform.position;
                                size++;
                                EndTightrope = _hit.transform.position;
                            }
                            else
                            {

                            }
                            step = Logic.Vector3D.arithmeticAverage(StartTightrope, EndTightrope);
                        }
                    }
                    else
                    {
                        step = secondFoot.transform.position + (stepDistance / 2) * secondFoot.transform.forward;
                    }
                }

                return step;
            }
        }
        public Vector3 nextStep(GameObject gameObject, GameObject foot, GameObject secondFoot, bool secondFootIsGrounded, float stepDistance, float groundDistance)
        {
            Vector3 step = new Vector3();

            RaycastHit hit;
            if (Physics.Raycast(foot.transform.position + stepDistance * gameObject.transform.forward, -Vector3.up, out hit, groundDistance))
            {
                step = foot.transform.position + stepDistance * foot.transform.forward;
            }
            else
                if (secondFootIsGrounded)
                step = secondFoot.transform.position + (stepDistance / 2) * secondFoot.transform.forward;

            return step;
        }
    }
}