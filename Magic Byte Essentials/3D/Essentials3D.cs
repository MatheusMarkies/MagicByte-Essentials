using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics
{
    public class Essentials3D
    {

        public struct Character
        {
            public float bodyWeight;
            public GameObject head;
            public GameObject abdomen;
            public GameObject upperArmRight;
            public GameObject uppeArmLeft;
            public GameObject lowerArmRight;
            public GameObject lowerArmLeft;
            public GameObject handRight;
            public GameObject handLeft;
            public GameObject upperLegRight;
            public GameObject uppeLegLeft;
            public GameObject lowerLegRight;
            public GameObject lowerLegLeft;
            public GameObject footRight;
            public GameObject footLeft;
        }
        public struct Weight
        {
            public float headWeight;//8% Weight
            public float abdomenWeight;//50% Weight
            public float upperArmWeight;//2.7% Weight
            public float lowerArmWeight;//1.6% Weight
            public float handWeight;//0.7% Weight
            public float upperLegWeight;//10.1% Weight
            public float lowerLegWeight;//4.4% Weight
            public float footWeight;//1.5% Weight
        }
            public struct Ground
        {
            public float distance;
            public Vector3 normal;
        }

        int get3DMovimentKey()
        {
            int Key = 0;
            if (Input.GetKey(KeyCode.W))
                Key += 3;
            if (Input.GetKey(KeyCode.S))
                Key -= 3;
            if (Input.GetKey(KeyCode.D))
                Key += 1;
            if (Input.GetKey(KeyCode.A))
                Key -= 1;
            return Key;
        }

        Ground getGround(Vector3 sourcePosition)
        {
            Ground ground;
            RaycastHit hit;
            Physics.Raycast(sourcePosition, Vector3.down, out hit);
            ground.distance = hit.distance;
            ground.normal = hit.normal;
            return ground;
        }

        public void simpleMoviment(float Speed, Transform Object, int key)
        {

            switch (key)
            {
                case 1:
                    Object.Translate(Vector3.right * Speed * Time.deltaTime);
                    break;
                case -1:
                    Object.Translate(Vector3.left * Speed * Time.deltaTime);
                    break;
                case 3:
                    Object.Translate(Vector3.forward * Speed * Time.deltaTime);
                    break;
                case -3:
                    Object.Translate(Vector3.back * Speed * Time.deltaTime);
                    break;
                case 4:
                    Object.Translate(Vector3.right * Speed * Time.deltaTime);
                    Object.Translate(Vector3.forward * Speed * Time.deltaTime);
                    break;
                case 2:
                    Object.Translate(Vector3.left * Speed * Time.deltaTime);
                    Object.Translate(Vector3.forward * Speed * Time.deltaTime);
                    break;
                case -2:
                    Object.Translate(Vector3.right * Speed * Time.deltaTime);
                    Object.Translate(Vector3.back * Speed * Time.deltaTime);
                    break;
                case -4:
                    Object.Translate(Vector3.left * Speed * Time.deltaTime);
                    Object.Translate(Vector3.back * Speed * Time.deltaTime);
                    break;
            }
        }

    }
}