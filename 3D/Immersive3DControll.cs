using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace EssentialMechanics
{
    /*
    Basic immersiveness for characters. 
    With movement controls, camera, humidity in shaders, calculation of steps, control of center of mass, balance based and physics.
    */
    [ExecuteInEditMode]
    public class Immersive3DControll
    {

        #region STRUCTS
        public struct Character
        {
            public float headWeight;//8% Weight
            public float abdomenWeight;//50% Weight
            public float upperArmWeight;//2.7% Weight
            public float lowerArmWeight;//1.6% Weight
            public float handWeight;//0.7% Weight
            public float upperLegWeight;//10.1% Weight
            public float lowerLegWeight;//4.4% Weight
            public float footWeight;//1.5% Weight

            public float charactergroundDistance;

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

        public struct Ground
        {
            public float distance;
            public Vector3 normal;
        }


        public enum ClampAngle
        {
     [InspectorName("15 Degrees")]
            _15_Degrees = 15
    , [InspectorName("30 Degrees")]
            _30_Degrees = 30
    , [InspectorName("45 Degrees")]
            _45_Degrees = 45
    , [InspectorName("60 Degrees")]
            _60_Degrees = 60
    , [InspectorName("90 Degrees")]
            _90_Degrees = 90
        }
        #endregion

        int Warning = 0;//Erros em funcionalidades do codigo

        Character CharacterConfiguration(bool autoCharactergroundDistance,float charactergroundDistance, float characterBodyWeight
        , GameObject head
        , GameObject abdomen
        , GameObject upperArmRight
        , GameObject upperArmLeft
        , GameObject lowerArmRight
        , GameObject lowerArmLeft
        , GameObject handRight
        , GameObject handLeft
        , GameObject upperLegRight
        , GameObject uppeLegLeft
        , GameObject lowerLegRight
        , GameObject lowerLegLeft
        , GameObject footRight
        , GameObject footLeft
            ){
            Character character = new Character();
            if (!autoCharactergroundDistance)
                character.charactergroundDistance = charactergroundDistance;
            else
            {
                character.charactergroundDistance = head.transform.position.y + head.transform.localScale.y;
                charactergroundDistance = head.transform.position.y + head.transform.localScale.y;
            }
            character.headWeight = characterBodyWeight * 0.08f;
            character.abdomenWeight = characterBodyWeight * 0.5f;
            character.upperArmWeight = characterBodyWeight * 0.027f;
            character.lowerArmWeight = characterBodyWeight * 0.016f;
            character.handWeight = characterBodyWeight * 0.007f;
            character.upperLegWeight = characterBodyWeight * 0.101f;
            character.lowerLegWeight = characterBodyWeight * 0.044f;
            character.footWeight = characterBodyWeight * 0.015f;
            try
            {
                character.head = head;
                character.abdomen = abdomen;
                character.upperArmRight = upperArmRight;
                character.uppeArmLeft = upperArmLeft;
                character.lowerArmRight = lowerArmRight;
                character.lowerArmLeft = lowerArmLeft;
                character.handRight = handRight;
                character.handLeft = handLeft;
                character.upperLegRight = upperLegRight;
                character.uppeLegLeft = uppeLegLeft;
                character.lowerLegRight = lowerLegRight;
                character.lowerLegLeft = lowerLegLeft;
                character.footRight = footRight;
                character.footLeft = footLeft;

                //centerOfMass = getPositionOfCenterOfMass(character);
                //centerOfGravity = getPositionOfCenterOfGravity(character);
            }
            catch (System.Exception e) { Debug.LogWarning("[ImmersiveControll] It was not possible to define values ​​for the center of mass and the center of gravity!"); }
            return character;
        }

        GameObject[] getWaters()
        {
            try
            {
                return GameObject.FindGameObjectsWithTag("Water");
            }
            catch (System.Exception e) { Debug.LogWarning("[ImmersiveControll] Tag Water is not defined"); return null; }
        }

        //void Update()
        //{
        //    if (autoEyeControl)
        //        autoEyesControl(this.character);
        //    step = nextStepWithTightropeDetect(this.character.footRight, this.character.footLeft, false, 1);
        //}
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

        public Vector3 simpleCameraOrbit(GameObject gameObject, Transform camera, Transform lockAtObject, float mouseX, Vector3 offset, float cameraDistance, float cameraGroundDistance, float movementDamping, bool viewOffset)
        {
            Vector3 CameraOffset = new Vector3();
            camera.LookAt(new Vector3(lockAtObject.position.x + offset.x, lockAtObject.position.y + offset.y, lockAtObject.position.z + offset.z));
            if (viewOffset)
                CameraOffset = new Vector3(lockAtObject.position.x + offset.x, lockAtObject.position.y + offset.y, lockAtObject.position.z + offset.z);

            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    Quaternion rot = Quaternion.Euler(0, mouseX, 0);
            //    Vector3 moveTowards = Vector3.MoveTowards(gameObject.transform.position, rot * add(Player.transform.position, targetOffSet), movementDamping * Time.deltaTime);
            //    transform.position = new Vector3(moveTowards.x, cameraYposition, moveTowards.z);

            //}
            Vector3 moveTowards = new Vector3();
            if (Vector3.Distance(camera.position, lockAtObject.position) > cameraDistance)
            {
                //float step = Mathf.Lerp(0, movementDamping * Time.deltaTime,1- Mathf.Clamp01(cameraDistance / Vector3.Distance(gameObject.transform.position, Player.transform.position)));
                moveTowards = Vector3.MoveTowards(gameObject.transform.position, new Vector3(lockAtObject.position.x + offset.x, lockAtObject.position.y + offset.y, lockAtObject.position.z + offset.z), movementDamping * Time.deltaTime * (1 - Mathf.Clamp01(cameraDistance / Vector3.Distance(gameObject.transform.position, lockAtObject.position))));
                moveTowards = new Vector3(moveTowards.x, cameraGroundDistance, moveTowards.z);
            }
            else
            {
                moveTowards = camera.position;
            }

            return moveTowards;
        }
        public void simpleMoviment(float Speed, Transform Object, int key,GameObject gameObject,Character character,float groundDistance)
        {

            switch (key)
            {
                case 1:
                    Object.Translate(Vector3.right * Speed * Time.deltaTime);
                    //if ((currentGroundDistance <= groundDistance))
                    //gameObject.GetComponent<Animator>().SetInteger("Walk", 1);
                    break;
                case -1:
                    Object.Translate(Vector3.left * Speed * Time.deltaTime);
                    //if ((currentGroundDistance <= groundDistance))
                    //gameObject.GetComponent<Animator>().SetInteger("Walk", 1);
                    break;
                case 3:
                    Object.Translate(Vector3.forward * Speed * Time.deltaTime);
                    //if ((currentGroundDistance <= groundDistance))
                    //gameObject.GetComponent<Animator>().SetInteger("Walk", 1);
                    break;
                case -3:
                    Object.Translate(Vector3.back * Speed * Time.deltaTime);
                    //if (!(currentGroundDistance <= groundDistance))
                    //gameObject.GetComponent<Animator>().SetInteger("Walk", 1);
                    break;
                case 4:
                    Object.Translate(Vector3.right * Speed * Time.deltaTime);
                    Object.Translate(Vector3.forward * Speed * Time.deltaTime);
                    //if((currentGroundDistance <= groundDistance))
                    //gameObject.GetComponent<Animator>().SetInteger("Walk", 1);
                    break;
                case 2:
                    Object.Translate(Vector3.left * Speed * Time.deltaTime);
                    Object.Translate(Vector3.forward * Speed * Time.deltaTime);
                    //if ((currentGroundDistance <= groundDistance))
                    //gameObject.GetComponent<Animator>().SetInteger("Walk", 1);
                    break;
                case -2:
                    Object.Translate(Vector3.right * Speed * Time.deltaTime);
                    Object.Translate(Vector3.back * Speed * Time.deltaTime);
                    //if ((currentGroundDistance <= groundDistance))
                    //gameObject.GetComponent<Animator>().SetInteger("Walk", 1);
                    break;
                case -4:
                    Object.Translate(Vector3.left * Speed * Time.deltaTime);
                    Object.Translate(Vector3.back * Speed * Time.deltaTime);
                    //if ((currentGroundDistance <= groundDistance))
                    //gameObject.GetComponent<Animator>().SetInteger("Walk", 1);
                    break;

                default:
                    gameObject.GetComponent<Animator>().SetInteger("Walk", 0);
                    break;

            }

            GameObject nearFoot;
            if (character.footLeft.transform.position.y < character.footRight.transform.position.y)
                nearFoot = character.footLeft;
            else
                nearFoot = character.footRight;
            if (getGround(nearFoot.transform.position).distance <= groundDistance)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //rb.AddForce(3 * Vector3.up, ForceMode.Impulse);
                }
            }
            //gameObject.GetComponent<Animator>().SetInteger("Walk", 0);
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
        public void autoEyesControl(GameObject gameObject,GameObject[] Eyes, ClampAngle EyeClampAngle)
        {
            Vector3 eyeControl = new Vector3();
            RaycastHit hit;
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, 10f))
                eyeControl = hit.transform.position;
            else
                try
                {
                    eyeControl = gameObject.transform.position + 5 * gameObject.transform.forward;
                }
                catch (System.Exception e) { if (Warning == 0) { Debug.LogWarning("[ImmersiveControll] Character configuration values ​​are missing!"); Warning++; } }
            foreach (GameObject i in Eyes)
            {
                i.transform.LookAt(eyeControl);
                i.transform.Rotate(Mathf.Clamp(i.transform.rotation.x, -(int)EyeClampAngle, (int)EyeClampAngle),
                    Mathf.Clamp(i.transform.rotation.y, -(int)EyeClampAngle, (int)EyeClampAngle),
                    Mathf.Clamp(i.transform.rotation.z, -(int)EyeClampAngle, (int)EyeClampAngle));
            }
        }
        public bool footIsGrounded(GameObject foot,float groundDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(foot.transform.position, -Vector3.up, out hit, groundDistance))
                return true;
            else
                return false;
        }
        public Vector3 nextStepWithTightropeDetect(GameObject gameObject,GameObject foot, GameObject secondFoot, bool secondFootIsGrounded, float stepDistance, float groundDistance)
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
        public Vector3 nextStep(GameObject gameObject, GameObject foot, GameObject secondFoot, bool secondFootIsGrounded, float stepDistance,float groundDistance)
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
        public Vector3 getPositionOfCenterOfMass(Character character)
        {
            float X = 0;
            float Y = 0;
            float Z = 0;

            X = ((character.head.transform.position).x * character.headWeight +
                 (character.abdomen.transform.position).x * character.abdomenWeight +
                 (character.upperArmRight.transform.position).x * character.upperArmWeight +
                 (character.uppeArmLeft.transform.position).x * character.upperArmWeight +
                 (character.lowerArmRight.transform.position).x * character.lowerArmWeight +
                 (character.lowerArmLeft.transform.position).x * character.lowerArmWeight +
                 (character.handRight.transform.position).x * character.handWeight +
                 (character.handLeft.transform.position).x * character.handWeight +
                 (character.upperLegRight.transform.position).x * character.upperLegWeight +
                 (character.uppeLegLeft.transform.position).x * character.upperLegWeight +
                 (character.lowerLegRight.transform.position).x * character.lowerLegWeight +
                 (character.lowerLegLeft.transform.position).x * character.lowerLegWeight +
                 (character.footRight.transform.position).x * character.footWeight +
                 (character.footLeft.transform.position).x * character.footWeight) /
                (character.headWeight +
                 character.abdomenWeight +
                 character.upperArmWeight +
                 character.upperArmWeight +
                 character.lowerArmWeight +
                character.lowerArmWeight +
                 character.handWeight +
                 character.handWeight +
                 character.upperLegWeight +
                 character.upperLegWeight +
                 character.lowerLegWeight +
                 character.lowerLegWeight +
                 character.footWeight +
                 character.footWeight);

            Y = ((character.head.transform.position).y * character.headWeight +
                 (character.abdomen.transform.position).y * character.abdomenWeight +
                 (character.upperArmRight.transform.position).y * character.upperArmWeight +
                 (character.uppeArmLeft.transform.position).y * character.upperArmWeight +
                 (character.lowerArmRight.transform.position).y * character.lowerArmWeight +
                 (character.lowerArmLeft.transform.position).y * character.lowerArmWeight +
                 (character.handRight.transform.position).y * character.handWeight +
                 (character.handLeft.transform.position).y * character.handWeight +
                 (character.upperLegRight.transform.position).y * character.upperLegWeight +
                 (character.uppeLegLeft.transform.position).y * character.upperLegWeight +
                 (character.lowerLegRight.transform.position).y * character.lowerLegWeight +
                 (character.lowerLegLeft.transform.position).y * character.lowerLegWeight +
                 (character.footRight.transform.position).y * character.footWeight +
                 (character.footLeft.transform.position).y * character.footWeight) /
                (character.headWeight +
                 character.abdomenWeight +
                 character.upperArmWeight +
                 character.upperArmWeight +
                 character.lowerArmWeight +
                character.lowerArmWeight +
                 character.handWeight +
                 character.handWeight +
                 character.upperLegWeight +
                 character.upperLegWeight +
                 character.lowerLegWeight +
                 character.lowerLegWeight +
                 character.footWeight +
                 character.footWeight);

            Z = ((character.head.transform.position).z * character.headWeight +
                  (character.abdomen.transform.position).z * character.abdomenWeight +
                  (character.upperArmRight.transform.position).z * character.upperArmWeight +
                  (character.uppeArmLeft.transform.position).z * character.upperArmWeight +
                  (character.lowerArmRight.transform.position).z * character.lowerArmWeight +
                  (character.lowerArmLeft.transform.position).z * character.lowerArmWeight +
                  (character.handRight.transform.position).z * character.handWeight +
                  (character.handLeft.transform.position).z * character.handWeight +
                  (character.upperLegRight.transform.position).z * character.upperLegWeight +
                  (character.uppeLegLeft.transform.position).z * character.upperLegWeight +
                  (character.lowerLegRight.transform.position).z * character.lowerLegWeight +
                  (character.lowerLegLeft.transform.position).z * character.lowerLegWeight +
                  (character.footRight.transform.position).z * character.footWeight +
                  (character.footLeft.transform.position).z * character.footWeight) /
                 (character.headWeight +
                  character.abdomenWeight +
                  character.upperArmWeight +
                  character.upperArmWeight +
                  character.lowerArmWeight +
                 character.lowerArmWeight +
                  character.handWeight +
                  character.handWeight +
                  character.upperLegWeight +
                  character.upperLegWeight +
                  character.lowerLegWeight +
                  character.lowerLegWeight +
                  character.footWeight +
                  character.footWeight);

            return new Vector3(X, Y, Z);
        }
        public Vector3 getPositionOfCenterOfGravity(Character character, float Gravity)
        {
            float X = 0;
            float Y = 0;
            float Z = 0;

            X = ((character.head.transform.position).x * character.headWeight * Gravity +
                 (character.abdomen.transform.position).x * character.abdomenWeight * Gravity +
                 (character.upperArmRight.transform.position).x * character.upperArmWeight * Gravity +
                 (character.uppeArmLeft.transform.position).x * character.upperArmWeight * Gravity +
                 (character.lowerArmRight.transform.position).x * character.lowerArmWeight * Gravity +
                 (character.lowerArmLeft.transform.position).x * character.lowerArmWeight * Gravity +
                 (character.handRight.transform.position).x * character.handWeight * Gravity +
                 (character.handLeft.transform.position).x * character.handWeight * Gravity +
                 (character.upperLegRight.transform.position).x * character.upperLegWeight * Gravity +
                 (character.uppeLegLeft.transform.position).x * character.upperLegWeight * Gravity +
                 (character.lowerLegRight.transform.position).x * character.lowerLegWeight * Gravity +
                 (character.lowerLegLeft.transform.position).x * character.lowerLegWeight * Gravity +
                 (character.footRight.transform.position).x * character.footWeight * Gravity +
                 (character.footLeft.transform.position).x * character.footWeight * Gravity) /
                (character.headWeight * Gravity +
                 character.abdomenWeight * Gravity +
                 character.upperArmWeight * Gravity +
                 character.upperArmWeight * Gravity +
                 character.lowerArmWeight * Gravity +
                character.lowerArmWeight * Gravity +
                 character.handWeight * Gravity +
                 character.handWeight * Gravity +
                 character.upperLegWeight * Gravity +
                 character.upperLegWeight * Gravity +
                 character.lowerLegWeight * Gravity +
                 character.lowerLegWeight * Gravity +
                 character.footWeight * Gravity +
                 character.footWeight * Gravity);

            Y = ((character.head.transform.position).y * character.headWeight * Gravity +
         (character.abdomen.transform.position).y * character.abdomenWeight * Gravity +
         (character.upperArmRight.transform.position).y * character.upperArmWeight * Gravity +
         (character.uppeArmLeft.transform.position).y * character.upperArmWeight * Gravity +
         (character.lowerArmRight.transform.position).y * character.lowerArmWeight * Gravity +
         (character.lowerArmLeft.transform.position).y * character.lowerArmWeight * Gravity +
         (character.handRight.transform.position).y * character.handWeight * Gravity +
         (character.handLeft.transform.position).y * character.handWeight * Gravity +
         (character.upperLegRight.transform.position).y * character.upperLegWeight * Gravity +
         (character.uppeLegLeft.transform.position).y * character.upperLegWeight * Gravity +
         (character.lowerLegRight.transform.position).y * character.lowerLegWeight * Gravity +
         (character.lowerLegLeft.transform.position).y * character.lowerLegWeight * Gravity +
         (character.footRight.transform.position).y * character.footWeight * Gravity +
         (character.footLeft.transform.position).y * character.footWeight * Gravity) /
                (character.headWeight * Gravity +
                 character.abdomenWeight * Gravity +
                 character.upperArmWeight * Gravity +
                 character.upperArmWeight * Gravity +
                 character.lowerArmWeight * Gravity +
                character.lowerArmWeight * Gravity +
                 character.handWeight * Gravity +
                 character.handWeight * Gravity +
                 character.upperLegWeight * Gravity +
                 character.upperLegWeight * Gravity +
                 character.lowerLegWeight * Gravity +
                 character.lowerLegWeight * Gravity +
                 character.footWeight * Gravity +
                 character.footWeight * Gravity);

            Z = ((character.head.transform.position).z * character.headWeight * Gravity +
         (character.abdomen.transform.position).z * character.abdomenWeight * Gravity +
         (character.upperArmRight.transform.position).z * character.upperArmWeight * Gravity +
         (character.uppeArmLeft.transform.position).z * character.upperArmWeight * Gravity +
         (character.lowerArmRight.transform.position).z * character.lowerArmWeight * Gravity +
         (character.lowerArmLeft.transform.position).z * character.lowerArmWeight * Gravity +
         (character.handRight.transform.position).z * character.handWeight * Gravity +
         (character.handLeft.transform.position).z * character.handWeight * Gravity +
         (character.upperLegRight.transform.position).z * character.upperLegWeight * Gravity +
         (character.uppeLegLeft.transform.position).z * character.upperLegWeight * Gravity +
         (character.lowerLegRight.transform.position).z * character.lowerLegWeight * Gravity +
         (character.lowerLegLeft.transform.position).z * character.lowerLegWeight * Gravity +
         (character.footRight.transform.position).z * character.footWeight * Gravity +
         (character.footLeft.transform.position).z * character.footWeight * Gravity) /
                (character.headWeight * Gravity +
                 character.abdomenWeight * Gravity +
                 character.upperArmWeight * Gravity +
                 character.upperArmWeight * Gravity +
                 character.lowerArmWeight * Gravity +
                character.lowerArmWeight * Gravity +
                 character.handWeight * Gravity +
                 character.handWeight * Gravity +
                 character.upperLegWeight * Gravity +
                 character.upperLegWeight * Gravity +
                 character.lowerLegWeight * Gravity +
                 character.lowerLegWeight * Gravity +
                 character.footWeight * Gravity +
                 character.footWeight * Gravity);

            return new Vector3(X, Y, Z);
        }
        public Vector2 getWetPercent(Character character, GameObject Water, float interactiveDistance)
        {
            Vector2 wetBoundsPercent = new Vector2();
            if (Water.TryGetComponent<MeshFilter>(out MeshFilter waterMeshFilter))
            {
                Vector3[] waterVertex = waterMeshFilter.mesh.vertices;
                List<Vector3> interactiveVertex = new List<Vector3>();
                foreach (Vector3 vert in waterVertex)
                    if (interactiveDistance <= Vector2.Distance(new Vector2(vert.x, vert.z), new Vector2(character.abdomen.transform.position.x, character.abdomen.transform.position.z)))
                        interactiveVertex.Add(vert);

                Vector3 minXVertex = interactiveVertex[0];
                Vector3 maxXVertex = interactiveVertex[0];
                Vector3 maxYVertex = interactiveVertex[0];

                for (int i = 0; i < interactiveVertex.Count; i++)
                {
                    if (interactiveVertex[i].y > maxYVertex.y)
                        maxYVertex = interactiveVertex[i];
                    if (interactiveVertex[i].x > maxXVertex.x)
                        maxXVertex = interactiveVertex[i];
                    if (interactiveVertex[i].x < minXVertex.x)
                        minXVertex = interactiveVertex[i];
                }
                wetBoundsPercent.x = Mathf.Min(Mathf.Abs(maxXVertex.x - minXVertex.x) / interactiveDistance, 1);
                wetBoundsPercent.y = Mathf.Min(Mathf.Abs((maxYVertex).y - Logic.Vector3D.arithmeticAverage(character.footLeft.transform.position, character.footRight.transform.position).y) / character.charactergroundDistance, 1);
            }
            else
            {
                wetBoundsPercent.x = 1;
                wetBoundsPercent.y = 1;
            }
            return wetBoundsPercent;
        }

    }
}