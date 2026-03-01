using System.Collections;
using UnityEngine.AI;
using UnityEngine;

namespace AIPackage
{
    public class Rotate : MonoBehaviour
    {
        NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void RotateTo(Transform target)
        {
            enabled = true;
            StopAllCoroutines();
            StartCoroutine(RotateToObject(target));
        }

        IEnumerator RotateToObject(Transform target)
        {
            Quaternion lookRotation;
            do
            {
                Vector3 direction = new Vector3(target.position.x - transform.position.x, 0f, target.position.z - transform.position.z);
                lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime / (
                    Quaternion.Angle(transform.rotation, lookRotation) / agent.angularSpeed));

                yield return new WaitForFixedUpdate();
            } while (true);
        }

        public void RotateTo(Vector3 target)
        {
            enabled = true;
            StopAllCoroutines();
            StartCoroutine(RotateToObject(target));
        }

        IEnumerator RotateToObject(Vector3 target)
        {
            Quaternion lookRotation;
            do
            {
                Vector3 direction = new Vector3(target.x - transform.position.x, 0f, target.z - transform.position.z);
                lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime / (
                    Quaternion.Angle(transform.rotation, lookRotation) / agent.angularSpeed));
                yield return new WaitForFixedUpdate();

            } while (true);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}