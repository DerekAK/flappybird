using UnityEngine;

public class Parallax : MonoBehaviour
{
   private MeshRenderer meshRenderer;

   public float animationSpeed = 5;

   private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
   }

   private void Update(){
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
   }
}
