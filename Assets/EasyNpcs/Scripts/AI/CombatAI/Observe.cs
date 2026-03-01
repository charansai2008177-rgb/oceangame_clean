using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPackage
{
    [RequireComponent(typeof(AimRig))]
    public class Observe : MonoBehaviour
    {
        public bool animRigging = true;
        AimRig aimRig;

        private void Awake()
        {
            stats = GetComponent<AI_Stats>();
            npcAI = GetComponent<NpcAI>();
            combatAI = GetComponent<CombatAI>();
            aimRig = GetComponent<AimRig>();
        }

        AI_Stats stats;
        NpcAI npcAI;
        CombatAI combatAI;
        
        public static List<Observe> enemiesOnPlayer = new List<Observe>();

        Transform _currentTarget;
        public Transform currentTarget
        {
            get { return _currentTarget; }
            set 
            {
                if (_currentTarget != value)
                {
                    _currentTarget = value;
                    ChangeAI();
                    AddListener();
                }
            }
        }

        void ChangeAI()
        {
            if (currentTarget != null)
            {
                npcAI.enabled = false;
                combatAI.enabled = true;

                if (animRigging)
                    aimRig.RigToTarget(currentTarget);

                combatAI.ChangeState(NpcState.Chase);
            }
            else
            {
                combatAI.ChangeState(NpcState.Default);

                if (animRigging)
                    aimRig.RemoveRig();
            }
        }

        void AddListener()
        {
            if (_currentTarget != null)
            {
                _currentTarget.GetComponent<Stats>().onDeath.AddListener(() => TargetToNull());
            }
        }

        private void FixedUpdate()
        {
            if (!stats.isDead)
                UpdateTarget();
        }

        float updateTargetTime = 0.1f;
        float currentTime = 0;

        void UpdateTarget()
        {
            if (currentTime > updateTargetTime)
            {
                if (currentTarget == null)
                {
                    AssignNewTarget();
                }

                currentTime = 0;
            }

            currentTime += Time.deltaTime;
        }

        void AssignNewTarget()
        {
            Transform target = TargetEnemy();
            if (target != null)
            {
                currentTarget = target;
            }
        }

        public Transform TargetEnemy()
        {
            List<Collider> possibleTargets = FindEnemies();
            Collider nearestTarget = NearestTarget(possibleTargets);

            if (nearestTarget != null)
                return nearestTarget.transform;
            else
                return null;
        }

        List<Collider> FindEnemies()
        {
            List<Collider> enemies = new List<Collider>();
            foreach (Collider target in PossibleTargets())
            {
                if (CheckTag(target))
                {
                    enemies.Add(target);
                }
            }

            return enemies;
        }

        public List<Collider> PossibleTargets(bool fov = true)
        {
            Vector3 hipPos = transform.position + new Vector3(0, 1);

            Collider[] cols = new Collider[10];
            Physics.OverlapSphereNonAlloc(hipPos, stats.visionRange, cols, LayerMask.GetMask("Npc")
                | LayerMask.GetMask("Player"));
            
            return CheckInSight(cols, fov);
        }

        List<Collider> CheckInSight(Collider[] cols, bool fov)
        {
            List<Collider> targetsInSight = new List<Collider>();
            foreach (Collider col in cols)
            {
                if (col != null && col.transform != transform)
                {
                    if (fov == true)
                    {
                        if (CheckAngle(col) < stats.visionAngle)
                            targetsInSight.Add(col);
                    }
                    else
                    {
                        if (col.GetComponent<NpcBase>() != null)
                            targetsInSight.Add(col);
                    }
                }
            }

            return targetsInSight;
        }

        bool CheckTag(Collider col)
        {
            for (int i = 0; i < stats.enemies.Count; i++)
            {
                if (col.gameObject.CompareTag(stats.enemies[i]))
                {
                    return true;
                }
            }

            return false;
        }

        float CheckAngle(Collider col)
        {
            Vector3 targetDir = col.transform.position - transform.position;
            return Vector3.Angle(targetDir, transform.forward);
        }

        Collider NearestTarget(List<Collider> possibleTargets)
        {
            Collider nearestTarget = null;
            if (possibleTargets.Count > 0)
            {
                nearestTarget = possibleTargets[0];
                for (int i = 1; i < possibleTargets.Count; i++)
                {
                    if (Vector3.Distance(possibleTargets[i].transform.position, transform.position)
                        < Vector3.Distance(nearestTarget.transform.position, transform.position))
                        nearestTarget = possibleTargets[i];
                }
            }

            return nearestTarget;
        }

        public bool CheckTarget_Distance_AndRaycast()
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, 1), 
                currentTarget.position - transform.position, out hit, 1);
            if (hit.transform == currentTarget)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<NpcBase> FindNpcs_ForInteraction(bool fov = true)
        {
            List<NpcBase> result = new List<NpcBase>();
            List<Collider> colliders = PossibleTargets(fov);
            foreach (Collider collider in colliders)
            {
                AIChangeBuffer buffer = collider.GetComponentInParent<AIChangeBuffer>();

                if (buffer != null)
                {
                    NpcBase npc = collider.GetComponentInParent<AIChangeBuffer>().enabledAI;
                    result.Add(npc);
                }
            }

            return result;
        }

        public void TargetToNull()
        {
            currentTarget = null;
        }
    }
}