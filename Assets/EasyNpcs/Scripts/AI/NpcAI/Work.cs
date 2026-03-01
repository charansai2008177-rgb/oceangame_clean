using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AIPackage
{
    public class Work : MonoBehaviour
    {
        public Behaviour GetScript()
        {
            return this;
        }

        public virtual void WorkDisable()
        {
            enabled = false;
        }
    }
}
