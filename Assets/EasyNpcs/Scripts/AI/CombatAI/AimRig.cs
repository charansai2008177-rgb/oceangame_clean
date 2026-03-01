using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace AIPackage
{
    public class AimRig : MonoBehaviour
    {
        MultiAimConstraint rig;
        RigBuilder rigBuilder;

        // Start is called before the first frame update
        void Start()
        {
            rig = GetComponentInChildren<MultiAimConstraint>();
            rigBuilder = GetComponent<RigBuilder>();
        }

        // Update is called once per frame
        void Update()
        {
            if (buildRig == true)
            {
                rigBuilder.Build();
                buildRig = false;
            }
        }

        bool buildRig = false;

        public void RigToTarget(Transform target)
        {
            var data = rig.data.sourceObjects;
            if (data.Count > 0)
            {
                data[0] = new WeightedTransform(target.Find("Camera/Human Male (v4.1.1)/BoneRoot/Base HumanPelvis/Base HumanSpine1/Base HumanSpine2/" +
                    "Base HumanSpine3/Base HumanRibcage/Base HumanNeck/Base HumanHead"), 1);
            }
            else
            {
                data.Add(new WeightedTransform(target.Find("Camera/Human Male (v4.1.1)/BoneRoot/Base HumanPelvis/Base HumanSpine1/Base HumanSpine2/" +
                    "Base HumanSpine3/Base HumanRibcage/Base HumanNeck/Base HumanHead"), 1));
            }

            rig.data.sourceObjects = data;
            buildRig = true;
        }

        public void RemoveRig()
        {
            var data = rig.data.sourceObjects;
            data.Clear();
            rig.data.sourceObjects = data;

            buildRig = true;
        }
    }
}

