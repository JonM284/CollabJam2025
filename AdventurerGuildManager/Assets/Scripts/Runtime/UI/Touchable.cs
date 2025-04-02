namespace Runtime.Misc
{
    //Not my code, made using: https://stackoverflow.com/questions/36888780/how-to-make-an-invisible-transparent-button-work = Apparently everyone uses this
    using UnityEngine;
    using UnityEngine.UI;
#if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(Touchable))]
    public class Touchable_Editor : Editor
    { public override void OnInspectorGUI(){} }
#endif
    public class Touchable:Text
    { protected override void Awake() { base.Awake();} }
}