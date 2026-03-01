using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AIPackage
{
    public class NpcObjectInteract : MonoBehaviour
    {
        Transform obj;
        Transform start;

        bool move;

        public float height = 0;
        public float rotateSec = 0;

        NavMeshAgent agent;
        Rotate rotate;

        Animator animator;
        string anim;

        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            rotate = GetComponent<Rotate>();
            animator = GetComponent<Animator>();
        }

        public void Set(Transform pos, Transform start, float height, 
            float rotateSec, string anim)
        {
            this.obj = pos;
            this.start = start;
            this.height = height;
            this.rotateSec = rotateSec;
            this.anim = anim;

            enabled = true;

            agent.SetDestination(start.position);
            move = false;
            StartCoroutine(ObjectInteracting());
        }

        private void Update()
        {
            if (move == true)
            {
                transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    new Vector3(obj.position.x, obj.position.y + height, obj.position.z), Time.deltaTime * 1 / 2);
            }
        }

        IEnumerator ObjectInteracting()
        {
            yield return new WaitUntil(() => Vector3.Distance(transform.position, start.position) < 0.5f);
            agent.enabled = false;
            rotate.RotateTo(obj.position + obj.forward);
            animator.SetBool(anim, true);

            move = true;

            yield return new WaitForSeconds(rotateSec);
            rotate.enabled = false;
        }

        private void OnDisable()
        {
            StopAllCoroutines();

            agent.enabled = true;
            animator.SetBool(anim, false);
            rotate.enabled = false;
        }
    }
}