using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace EssentialMechanics
{
    public static class Essentials2D
    {
        static Vector3 vectorZero = Vector3.zero;
        public static Transform translateMovement(Transform Position, float Speed, bool ArrowToo)
        {
            Transform transform = Position;
            Transform transformMove = Position;

            int Key = 0;
            if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow) && ArrowToo))
                Key += 3;
            if (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow) && ArrowToo))
                Key -= 3;
            if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow) && ArrowToo))
                Key += 1;
            if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow) && ArrowToo))
                Key -= 1;

            switch (Key)
            {
                case 1:
                    transformMove.Translate(Vector2.right * Speed * Time.deltaTime);
                    transform = flipCharacterRight(transform);
                    break;
                case -1:
                    transformMove.Translate(Vector2.left * Speed * Time.deltaTime);
                    transform = flipCharacterLeft(transform);
                    break;
                case 3:
                    transformMove.Translate(Vector2.up * Speed * Time.deltaTime);
                    break;
                case -3:
                    transformMove.Translate(Vector2.down * Speed * Time.deltaTime);
                    break;
                case 4:
                    transformMove.Translate(Vector2.right * Speed * Time.deltaTime);
                    transformMove.Translate(Vector2.up * Speed * Time.deltaTime);
                    transform = flipCharacterRight(transform);
                    break;
                case 2:
                    transformMove.Translate(Vector2.left * Speed * Time.deltaTime);
                    transformMove.Translate(Vector2.up * Speed * Time.deltaTime);
                    transform = flipCharacterLeft(transform);
                    break;
                case -2:
                    transformMove.Translate(Vector2.right * Speed * Time.deltaTime);
                    transformMove.Translate(Vector2.down * Speed * Time.deltaTime);
                    transform = flipCharacterRight(transform);
                    break;
                case -4:
                    transformMove.Translate(Vector2.left * Speed * Time.deltaTime);
                    transformMove.Translate(Vector2.down * Speed * Time.deltaTime);
                    transform = flipCharacterLeft(transform);
                    break;

                default:

                    break;
            }

            transform.position = Vector2.Lerp(transform.position, transformMove.position,Speed * Time.deltaTime);
            
            return transform;
        }
        public static bool getIsGrounded(Vector2 checkPosition, float radius, LayerMask groundLayer) { return Physics2D.OverlapCircle(checkPosition, radius, groundLayer); }
        public static void rigidbodyMovement(Rigidbody2D rigidbody,Transform transform, float smoothVelocity, float Speed, bool ArrowToo) 
        {

            Vector2 targetSpeed = new Vector2();

            int Key = 0;
            if (Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow) && ArrowToo))
                Key += 3;
            if (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow) && ArrowToo))
                Key -= 3;
            if (Input.GetKey(KeyCode.D) || (Input.GetKey(KeyCode.RightArrow) && ArrowToo))
                Key += 1;
            if (Input.GetKey(KeyCode.A) || (Input.GetKey(KeyCode.LeftArrow) && ArrowToo))
                Key -= 1;

            switch (Key)
            {
                case 1:
                    targetSpeed = Vector2.right * Speed * Time.deltaTime;
                    break;
                case -1:
                    targetSpeed = (Vector2.left * Speed * Time.deltaTime);
                    //transform = flipCharacter(transform);
                    break;
                case 3:
                    targetSpeed = (Vector2.up * Speed * Time.deltaTime);
                    break;
                case -3:
                    targetSpeed = (Vector2.down * Speed * Time.deltaTime);
                    break;
                case 4:
                    targetSpeed = (Vector2.right * Speed * Time.deltaTime);
                    targetSpeed = (Vector2.up * Speed * Time.deltaTime);
                    break;
                case 2:
                    targetSpeed = (Vector2.left * Speed * Time.deltaTime);
                    targetSpeed = (Vector2.up * Speed * Time.deltaTime);
                    //transform = flipCharacter(transform);
                    break;
                case -2:
                    targetSpeed = (Vector2.right * Speed * Time.deltaTime);
                    targetSpeed = (Vector2.down * Speed * Time.deltaTime);
                    break;
                case -4:
                    targetSpeed = (Vector2.left * Speed * Time.deltaTime);
                    targetSpeed = (Vector2.down * Speed * Time.deltaTime);
                    //transform = flipCharacter(transform);
                    break;

                default:

                    break;

            }
            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetSpeed, ref vectorZero, smoothVelocity);
        }
        public static Transform flipCharacterLeft(Transform transform)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Min(theScale.x, -theScale.x);
            transform.localScale = theScale;
            return transform;
        }
        public static Transform flipCharacterRight(Transform transform)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = Mathf.Max(theScale.x,-theScale.x);
            transform.localScale = theScale;
            return transform;
        }
        public static Transform flipCharacter(Transform transform)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            return transform;
        }
        public static Transform mouseMovement(Transform Position, float Speed,Camera camera)
        {
            Vector3 mouseClickPosition = Vector3.zero;
            Transform transform = Position;
            if (Input.GetButton("Fire1"))
            {
                Vector3 mousePos = Input.mousePosition;
                    mouseClickPosition = camera.ScreenToWorldPoint(mousePos);
                if (mouseClickPosition.x < Position.position.x)
                    Position = flipCharacterLeft(Position);
                else
                    Position = flipCharacterRight(Position);
                transform.position = Vector2.MoveTowards(Position.position, mouseClickPosition, Speed * Time.deltaTime);
            }

            return transform;
        }
        public static int mouseClickSite(Transform Position, Camera camera)
        {
            Vector3 mouseClickPosition = Vector3.zero;
            int site = 0;
            float tg = 0;
            if (Input.GetButton("Fire1"))
            {
                Vector3 mousePos = Input.mousePosition;
                mouseClickPosition = camera.ScreenToWorldPoint(mousePos);

                tg = (Mathf.Atan2(mouseClickPosition.y - Position.position.y, mouseClickPosition.x - Position.position.x) * Mathf.Rad2Deg);
                //Debug.LogWarning(tg);
                if (tg > 45 && tg < 135)
                {
                    site = 4;
                    //Debug.LogWarning(tg + " Cima");
                }
                if (tg > 135 && tg < 180)
                {
                    site = -2;
                    //Debug.LogWarning(tg + " Esquerda");
                }
                if (tg > -180 && tg < -135)
                {
                    site = -2;
                    //Debug.LogWarning(tg + " Esquerda");
                }
                if (tg > -135 && tg < -45)
                {
                    site = -4;
                    //Debug.LogWarning(tg + " Baixo");
                }
                if (tg > -45 && tg < 0)
                {
                    site = 2;
                    //Debug.LogWarning(tg + " Direita");
                }
                if (tg > 0 && tg < 45)
                {
                    site = 2;
                    //Debug.LogWarning(tg + " Direita");
                }

            }

            return site;
        }
        public static List<Vector2> getTilesPositions(this Tilemap tilemap)
        {
            List<Vector2> tiles = new List<Vector2>();

            for (int y = tilemap.origin.y; y < (tilemap.origin.y + tilemap.size.y); y++)
            {
                for (int x = tilemap.origin.x; x < (tilemap.origin.x + tilemap.size.x); x++)
                {
                    TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                    if (tile != null)
                    {
                        tiles.Add(new Vector2(x,y));
                    }
                }
            }
            return tiles;
        }

    }

}//Namespace