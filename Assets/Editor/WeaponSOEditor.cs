using UnityEditor;

[CustomEditor(typeof(WeaponSO))]
public class WeaponSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();
        
        var weaponSO = (WeaponSO)target;
        DrawPropertiesExcluding(serializedObject, nameof(weaponSO.projectile), nameof(weaponSO.projectileSpeed));

        if (weaponSO.fireMode == WeaponSO.FireMode.Projectile)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(weaponSO.projectileSpeed)));
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(weaponSO.projectile)));
        }

        serializedObject.ApplyModifiedProperties();
    }
    
}
