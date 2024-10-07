using UnityEngine;
using UnityEngine.Serialization;

public enum Highway
{
   Enter,
   Exit,
   None
}

public class Stage : MonoBehaviour
{
   public Highway highwayType = Highway.None;
   public string sceneName;
   private SpriteRenderer _spriteRenderer;

   [SerializeField] private Stage enterStage;
   [SerializeField] private Stage exitStage;

   public Stage GetEnterStage()
   {
      return enterStage;
   }

   public Stage GetExitStage()
   {
      return exitStage;
   }

   #region Alpha
   private void Awake()
   {
      _spriteRenderer = GetComponent<SpriteRenderer>();
   }
   
   public void SetAlpha(float newAlpha)
   {
      Color currentColor = _spriteRenderer.color;
      _spriteRenderer.color = new Color(currentColor.r , currentColor.g , currentColor.b , newAlpha);
   }

   public float GetAlpha() => _spriteRenderer.color.a;
   

   #endregion
 
}
