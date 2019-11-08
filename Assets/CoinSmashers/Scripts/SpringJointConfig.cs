using UnityEngine;

namespace Assets.CoinSmashers.Scripts
{
    public class SpringJointConfig : MonoBehaviour
    {
        [SerializeField]
        [UnityEngine.Serialization.FormerlySerializedAs("sprintJointList")]
        private SpringJoint[] springJointList = null;
        [SerializeField]
        private BoxCollider boxCollider = null;
        [SerializeField]
        private float spring = 1.0f;
        [SerializeField]
        private float damper = 1.0f;

        [SerializeField]
        private float maxDistance = 2.0f;
        [SerializeField]
        private float minDistance = 1.0f;

        void OnValidate()
        {
            var multiplier = new[] { new[] { 1, 1 }, new[] { 1, -1 }, new[] { -1, 1 }, new[] { -1, -1 } };

            for (int i = 0; i < springJointList.Length; i++)
            {
                springJointList[i].anchor = new Vector3(boxCollider.size.x / 2 * multiplier[i][0], -boxCollider.size.y / 2, boxCollider.size.z / 2 * multiplier[i][1]);
                springJointList[i].spring = spring;
                springJointList[i].damper = damper;
                springJointList[i].maxDistance = maxDistance;
                springJointList[i].minDistance = minDistance;
            }
        }
    }
}
