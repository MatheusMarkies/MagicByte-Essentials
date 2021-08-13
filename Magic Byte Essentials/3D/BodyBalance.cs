using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics
{
    public class BodyBalance
    {
        public Essentials3D.Weight createWeightMap(Essentials3D.Character character)
        {
            Essentials3D.Weight body = new Essentials3D.Weight();
            body.headWeight = character.bodyWeight * 0.08f;//8% Weight
            body.abdomenWeight = character.bodyWeight * 0.5f;//50% Weight
            body.upperArmWeight = character.bodyWeight * 0.027f;//2.7% Weight
            body.lowerArmWeight = character.bodyWeight * 0.016f;//1.6% Weight
            body.handWeight = character.bodyWeight * 0.07f;//0.7% Weight
            body.upperLegWeight = character.bodyWeight * 0.101f;//10.1% Weight
            body.lowerLegWeight = character.bodyWeight * 0.044f;//4.4% Weight
            body.footWeight = character.bodyWeight * 0.015f;//1.5% Weight

            return body;
    }
        public Vector3 getPositionOfCenterOfMass(Essentials3D.Character character)
        {
            float X = 0;
            float Y = 0;
            float Z = 0;

            Essentials3D.Weight body = createWeightMap(character);

            X = ((character.head.transform.position).x * body.headWeight +
                 (character.abdomen.transform.position).x * body.abdomenWeight +
                 (character.upperArmRight.transform.position).x * body.upperArmWeight +
                 (character.uppeArmLeft.transform.position).x * body.upperArmWeight +
                 (character.lowerArmRight.transform.position).x * body.lowerArmWeight +
                 (character.lowerArmLeft.transform.position).x * body.lowerArmWeight +
                 (character.handRight.transform.position).x * body.handWeight +
                 (character.handLeft.transform.position).x * body.handWeight +
                 (character.upperLegRight.transform.position).x * body.upperLegWeight +
                 (character.uppeLegLeft.transform.position).x * body.upperLegWeight +
                 (character.lowerLegRight.transform.position).x * body.lowerLegWeight +
                 (character.lowerLegLeft.transform.position).x * body.lowerLegWeight +
                 (character.footRight.transform.position).x * body.footWeight +
                 (character.footLeft.transform.position).x * body.footWeight) /
                (body.headWeight +
                 body.abdomenWeight +
                 body.upperArmWeight +
                 body.upperArmWeight +
                 body.lowerArmWeight +
                 body.lowerArmWeight +
                 body.handWeight +
                 body.handWeight +
                 body.upperLegWeight +
                 body.upperLegWeight +
                 body.lowerLegWeight +
                 body.lowerLegWeight +
                 body.footWeight +
                 body.footWeight);

            Y = ((character.head.transform.position).y * body.headWeight +
                 (character.abdomen.transform.position).y * body.abdomenWeight +
                 (character.upperArmRight.transform.position).y * body.upperArmWeight +
                 (character.uppeArmLeft.transform.position).y * body.upperArmWeight +
                 (character.lowerArmRight.transform.position).y * body.lowerArmWeight +
                 (character.lowerArmLeft.transform.position).y * body.lowerArmWeight +
                 (character.handRight.transform.position).y * body.handWeight +
                 (character.handLeft.transform.position).y * body.handWeight +
                 (character.upperLegRight.transform.position).y * body.upperLegWeight +
                 (character.uppeLegLeft.transform.position).y * body.upperLegWeight +
                 (character.lowerLegRight.transform.position).y * body.lowerLegWeight +
                 (character.lowerLegLeft.transform.position).y * body.lowerLegWeight +
                 (character.footRight.transform.position).y * body.footWeight +
                 (character.footLeft.transform.position).y * body.footWeight) /
                (body.headWeight +
                 body.abdomenWeight +
                 body.upperArmWeight +
                 body.upperArmWeight +
                 body.lowerArmWeight +
                body.lowerArmWeight +
                 body.handWeight +
                 body.handWeight +
                 body.upperLegWeight +
                 body.upperLegWeight +
                 body.lowerLegWeight +
                 body.lowerLegWeight +
                 body.footWeight +
                 body.footWeight);

            Z = ((character.head.transform.position).z * body.headWeight +
                  (character.abdomen.transform.position).z * body.abdomenWeight +
                  (character.upperArmRight.transform.position).z * body.upperArmWeight +
                  (character.uppeArmLeft.transform.position).z * body.upperArmWeight +
                  (character.lowerArmRight.transform.position).z * body.lowerArmWeight +
                  (character.lowerArmLeft.transform.position).z * body.lowerArmWeight +
                  (character.handRight.transform.position).z * body.handWeight +
                  (character.handLeft.transform.position).z * body.handWeight +
                  (character.upperLegRight.transform.position).z * body.upperLegWeight +
                  (character.uppeLegLeft.transform.position).z * body.upperLegWeight +
                  (character.lowerLegRight.transform.position).z * body.lowerLegWeight +
                  (character.lowerLegLeft.transform.position).z * body.lowerLegWeight +
                  (character.footRight.transform.position).z * body.footWeight +
                  (character.footLeft.transform.position).z * body.footWeight) /
                 (body.headWeight +
                  body.abdomenWeight +
                  body.upperArmWeight +
                  body.upperArmWeight +
                  body.lowerArmWeight +
                  body.lowerArmWeight +
                  body.handWeight +
                  body.handWeight +
                  body.upperLegWeight +
                  body.upperLegWeight +
                  body.lowerLegWeight +
                  body.lowerLegWeight +
                  body.footWeight +
                  body.footWeight);

            return new Vector3(X, Y, Z);
        }
        public Vector3 getPositionOfCenterOfGravity(Essentials3D.Character character, float Gravity)
        {
            float X = 0;
            float Y = 0;
            float Z = 0;

            Essentials3D.Weight body = createWeightMap(character);

            X = ((character.head.transform.position).x * body.headWeight * Gravity +
                 (character.abdomen.transform.position).x * body.abdomenWeight * Gravity +
                 (character.upperArmRight.transform.position).x * body.upperArmWeight * Gravity +
                 (character.uppeArmLeft.transform.position).x * body.upperArmWeight * Gravity +
                 (character.lowerArmRight.transform.position).x * body.lowerArmWeight * Gravity +
                 (character.lowerArmLeft.transform.position).x * body.lowerArmWeight * Gravity +
                 (character.handRight.transform.position).x * body.handWeight * Gravity +
                 (character.handLeft.transform.position).x * body.handWeight * Gravity +
                 (character.upperLegRight.transform.position).x * body.upperLegWeight * Gravity +
                 (character.uppeLegLeft.transform.position).x * body.upperLegWeight * Gravity +
                 (character.lowerLegRight.transform.position).x * body.lowerLegWeight * Gravity +
                 (character.lowerLegLeft.transform.position).x * body.lowerLegWeight * Gravity +
                 (character.footRight.transform.position).x * body.footWeight * Gravity +
                 (character.footLeft.transform.position).x * body.footWeight * Gravity) /
                (body.headWeight * Gravity +
                 body.abdomenWeight * Gravity +
                 body.upperArmWeight * Gravity +
                 body.upperArmWeight * Gravity +
                 body.lowerArmWeight * Gravity +
                 body.lowerArmWeight * Gravity +
                 body.handWeight * Gravity +
                 body.handWeight * Gravity +
                 body.upperLegWeight * Gravity +
                 body.upperLegWeight * Gravity +
                 body.lowerLegWeight * Gravity +
                 body.lowerLegWeight * Gravity +
                 body.footWeight * Gravity +
                 body.footWeight * Gravity);

            Y = ((character.head.transform.position).y * body.headWeight * Gravity +
         (character.abdomen.transform.position).y * body.abdomenWeight * Gravity +
         (character.upperArmRight.transform.position).y * body.upperArmWeight * Gravity +
         (character.uppeArmLeft.transform.position).y * body.upperArmWeight * Gravity +
         (character.lowerArmRight.transform.position).y * body.lowerArmWeight * Gravity +
         (character.lowerArmLeft.transform.position).y * body.lowerArmWeight * Gravity +
         (character.handRight.transform.position).y * body.handWeight * Gravity +
         (character.handLeft.transform.position).y * body.handWeight * Gravity +
         (character.upperLegRight.transform.position).y * body.upperLegWeight * Gravity +
         (character.uppeLegLeft.transform.position).y * body.upperLegWeight * Gravity +
         (character.lowerLegRight.transform.position).y * body.lowerLegWeight * Gravity +
         (character.lowerLegLeft.transform.position).y * body.lowerLegWeight * Gravity +
         (character.footRight.transform.position).y * body.footWeight * Gravity +
         (character.footLeft.transform.position).y * body.footWeight * Gravity) /
                (body.headWeight * Gravity +
                 body.abdomenWeight * Gravity +
                 body.upperArmWeight * Gravity +
                 body.upperArmWeight * Gravity +
                 body.lowerArmWeight * Gravity +
                 body.lowerArmWeight * Gravity +
                 body.handWeight * Gravity +
                 body.handWeight * Gravity +
                 body.upperLegWeight * Gravity +
                 body.upperLegWeight * Gravity +
                 body.lowerLegWeight * Gravity +
                 body.lowerLegWeight * Gravity +
                 body.footWeight * Gravity +
                 body.footWeight * Gravity);

            Z = ((character.head.transform.position).z * body.headWeight * Gravity +
         (character.abdomen.transform.position).z * body.abdomenWeight * Gravity +
         (character.upperArmRight.transform.position).z * body.upperArmWeight * Gravity +
         (character.uppeArmLeft.transform.position).z * body.upperArmWeight * Gravity +
         (character.lowerArmRight.transform.position).z * body.lowerArmWeight * Gravity +
         (character.lowerArmLeft.transform.position).z * body.lowerArmWeight * Gravity +
         (character.handRight.transform.position).z * body.handWeight * Gravity +
         (character.handLeft.transform.position).z * body.handWeight * Gravity +
         (character.upperLegRight.transform.position).z * body.upperLegWeight * Gravity +
         (character.uppeLegLeft.transform.position).z * body.upperLegWeight * Gravity +
         (character.lowerLegRight.transform.position).z * body.lowerLegWeight * Gravity +
         (character.lowerLegLeft.transform.position).z * body.lowerLegWeight * Gravity +
         (character.footRight.transform.position).z * body.footWeight * Gravity +
         (character.footLeft.transform.position).z * body.footWeight * Gravity) /
                (body.headWeight * Gravity +
                 body.abdomenWeight * Gravity +
                 body.upperArmWeight * Gravity +
                 body.upperArmWeight * Gravity +
                 body.lowerArmWeight * Gravity +
                body.lowerArmWeight * Gravity +
                 body.handWeight * Gravity +
                 body.handWeight * Gravity +
                 body.upperLegWeight * Gravity +
                 body.upperLegWeight * Gravity +
                 body.lowerLegWeight * Gravity +
                 body.lowerLegWeight * Gravity +
                 body.footWeight * Gravity +
                 body.footWeight * Gravity);

            return new Vector3(X, Y, Z);
        }
    }
}