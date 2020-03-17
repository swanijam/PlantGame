using UnityEditor;
using UnityEngine;

public static class EditorHelper
{
    public static SerializedProperty FindPropertyRelativeFix(this SerializedProperty sp, string name, ref SerializedObject objectToApplyChanges)
    {
        SerializedProperty result;
        if (typeof(ScriptableObject).IsAssignableFrom(GetFieldType(sp)))
        {
            if (sp.objectReferenceValue == null) return null;
            if (objectToApplyChanges == null)
                objectToApplyChanges = new SerializedObject(sp.objectReferenceValue);
            result = objectToApplyChanges.FindProperty(name);
        }
        else
        {
            objectToApplyChanges = null;
            result = sp.FindPropertyRelative(name);
        }
        return result;
    }


    public static System.Type GetFieldType(SerializedProperty property)
    {
        if (property.serializedObject.targetObject == null) return null;
        System.Type parentType = property.serializedObject.targetObject.GetType();
        System.Reflection.FieldInfo fi = parentType.GetField(property.propertyPath);
        string path = property.propertyPath;
        if (fi == null)
            return null;

        return fi.FieldType;
    }
}