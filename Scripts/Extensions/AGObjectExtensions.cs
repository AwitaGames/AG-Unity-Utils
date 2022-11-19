using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AGObjectExtensions{

    /// <summary>
    /// Runs an action after a delay 
    /// </summary>
    /// <param name="action">The action to run after the delay</param>
    /// <param name="delay">The delay in seconds</param>
    /// <param name="useUnscaledTime">Should we use unscaled time?</param>
    public static Coroutine InvokeEvent(this MonoBehaviour obj, float delay, System.Action action, bool useUnscaledTime = false) {
        return obj.StartCoroutine(_InvokeEvent(action, delay, useUnscaledTime));
    }

    private static IEnumerator _InvokeEvent(System.Action action, float delay, bool useUnscaledTime = false) {
        float s = 0f;
        while (s < delay) {
            s += useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        action();
    }

    /// <summary>
    /// Destroys all childs recursively. 
    /// </summary>
    public static void DestroyAllChilds(this Transform trans) {
        Transform[] childs = trans.GetComponentsInChildren<Transform>(true);
        foreach (var t in childs) {
            if (t != trans)
                GameObject.Destroy(t.gameObject);
        }
    }

    /// <summary>
    /// Finds the first child recursively that has the type T with name name.
    /// </summary>
    /// <param name="name">The name of the object we're looking for in childs.</param>
    public static T FindChildT<T>(this GameObject gameObject, string name) {
        T[] childs = gameObject.GetComponentsInChildren<T>(true);
        foreach (var child in childs) {
            if ((child as Component).gameObject.name == name) {
                return child;
            }
        }
        return default(T);
    }

    /// <summary>
    /// Looks at the object smoothly. Used inside a loop, update or IEnumerator.
    /// </summary>
    /// <param name="target">The target position to look at.</param>
    /// <param name="ignoreY">If we should ignore Y and only rotate on X and Z angles.</param>
    /// <param name="speed">The speed of the rotation</param>
    /// <param name="offsetRotation">The offset rotation to add after calculating the final rotation.</param>
    public static void LookAtSmooth(this Transform trans, Vector3 target, bool ignoreY = true, float speed = 5f, Vector3 offsetRotation = new Vector3()) {

        if (ignoreY)
            target.y = trans.position.y;

        if ((target - trans.position) != Vector3.zero) {

            Quaternion targetRotation = Quaternion.LookRotation(target - trans.position);

            Vector3 eulerRotation = targetRotation.eulerAngles;
            eulerRotation += offsetRotation;

            targetRotation = Quaternion.Euler(eulerRotation);

            trans.rotation = Quaternion.Slerp(trans.rotation, targetRotation, Time.deltaTime * speed);
        }

    }

    /// <summary>
    /// Check if an object is looking at target with a precision. That means "how much" the forward of the origin object is facing the target object.
    /// </summary>
    /// <param name="target">The target object to check if is being looked at.</param>
    /// <param name="lookAtPrecision">The precision of the check.</param>
    /// <returns></returns>
    public static bool IsObjectLookingAtTarget(this Transform origin, Transform target, float lookAtPrecision = 0.75f) {

        Vector3 dirFromAtoB = (target.position - origin.position).normalized;
        float dotProd = Vector3.Dot(dirFromAtoB, origin.transform.forward);

        if (dotProd > lookAtPrecision) {
            return true;
        } else {
            return false;
        }

    }

}
