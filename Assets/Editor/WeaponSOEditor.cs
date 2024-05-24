using UnityEditor;

[CustomEditor(typeof(WeaponSO))]
public class WeaponSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();
        
        var weaponSO = (WeaponSO)target;

        DrawPropertiesExcluding(serializedObject, nameof(weaponSO.projectile), nameof(weaponSO.projectileSpeed),
            nameof(weaponSO.rayDistance));

        switch (weaponSO.fireMode)
        {
            case WeaponSO.FireMode.Projectile:
                
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(weaponSO.projectileSpeed)));
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(weaponSO.projectile)));
                break;
            
            case WeaponSO.FireMode.Raycast:
                
                EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(weaponSO.rayDistance)));
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
    
}
