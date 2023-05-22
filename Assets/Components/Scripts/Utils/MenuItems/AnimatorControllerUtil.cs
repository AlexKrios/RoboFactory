using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Components.Scripts.Utils.MenuItems
{
    public class AnimatorControllerUtil : MonoBehaviour
    {
        [MenuItem("GameObject/Create Button Animator Controller")]
        public static void CreateMaterial()
        {
            var animationController = new UnityEditor.Animations.AnimatorController();
            AssetDatabase.CreateAsset(animationController, "Assets/Animation/AnimatorController.controller");

            var normalClip = new AnimationClip { name = "Normal" };
            var pressedClip = new AnimationClip { name = "Pressed" };
            var disabledClip = new AnimationClip { name = "Disabled" };
            var lockedClip = new AnimationClip { name = "Locked" };
            AssetDatabase.AddObjectToAsset(normalClip, animationController);
            AssetDatabase.AddObjectToAsset(pressedClip, animationController);
            AssetDatabase.AddObjectToAsset(disabledClip, animationController);
            AssetDatabase.AddObjectToAsset(lockedClip, animationController);
            
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(normalClip));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(pressedClip));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(disabledClip));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(lockedClip));
        }
        
        [MenuItem("GameObject/Create Upgrade Button Animator Controller")]
        public static void CreateUpgradeButtonAnimator()
        {
            var animationController = new UnityEditor.Animations.AnimatorController();
            AssetDatabase.CreateAsset(animationController, "Assets/Animation/AnimatorController.controller");

            var normalClip = new AnimationClip { name = "Normal" };
            var pressedClip = new AnimationClip { name = "Pressed" };
            var disabledClip = new AnimationClip { name = "Disabled" };
            var lockedClip = new AnimationClip { name = "Locked" };
            AssetDatabase.AddObjectToAsset(normalClip, animationController);
            AssetDatabase.AddObjectToAsset(pressedClip, animationController);
            AssetDatabase.AddObjectToAsset(disabledClip, animationController);
            AssetDatabase.AddObjectToAsset(lockedClip, animationController);
            
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(normalClip));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(pressedClip));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(disabledClip));
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(lockedClip));
        }
    }
}
#endif