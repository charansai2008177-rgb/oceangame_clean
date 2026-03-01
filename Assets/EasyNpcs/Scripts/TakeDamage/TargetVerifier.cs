using UnityEngine;

namespace AIPackage
{
    public static class TargetVerifier
    {
        public static Transform FindMainParent_OfTarget(Transform attacker, Transform targetPart)
        {
            if (NotBlocked(targetPart.GetComponentInParent<Animator>()) &&
                Vector3.Distance(attacker.transform.position, targetPart.transform.position) <= attacker.GetComponent<Stats>().attackDistance)
            {
                return targetPart.GetComponentInParent<Animator>().transform;
            }

            return null;
        }

        static bool NotBlocked(Animator anim)
        {
            if (anim.GetBool("Block"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}