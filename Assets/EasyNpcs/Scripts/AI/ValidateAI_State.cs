using UnityEngine;

namespace AIPackage
{
    public static class ValidateAI_State
    {
        public static bool IsNpc(Transform npc)
        {
            if (npc.GetComponentInParent<AI_Stats>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CanNpcInteract(Transform npcPart, bool ignoreState)
        {
            Stats stats = npcPart.GetComponentInParent<Stats>();
            if (!stats.isDead)
            {
                if (!ignoreState)
                {
                    return Does_CurrentState_Allow(stats.transform);
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public static bool Does_CurrentState_Allow(Transform npc)
        {
            NpcBase npcBase = npc.GetComponent<AIChangeBuffer>().enabledAI;
            if (npcBase.currentState != NpcState.Talking && npcBase.currentState != NpcState.Scared
                && npcBase.currentState != NpcState.AtHome)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
