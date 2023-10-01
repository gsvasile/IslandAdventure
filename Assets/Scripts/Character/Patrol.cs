using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.AI;

namespace RPG.Character
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private GameObject splineGameObject;
        [SerializeField] private float walkDurationMinimum = 1f;
        [SerializeField] private float walkDurationMaximum = 5f;
        [SerializeField] private float walkDuration = 4f;
        [SerializeField] private float pauseDurationMinimum = 2f;
        [SerializeField] private float pauseDurationMaximum = 6f;
        [SerializeField] private float pauseDuration = 3f;

        private SplineContainer splineComponent;
        private NavMeshAgent agentComponent;

        private float splinePosition;
        private float splineLength = 0f;
        private float lengthWalked = 0f;
        private float walkTime = 0f;
        private float pauseTime = 0f;
        private bool isWalking = true;

        private void Awake()
        {
            if (splineGameObject == null)
            {
                Debug.LogWarning($"{name} does not have a spline.");
            }

            splineComponent = splineGameObject.GetComponent<SplineContainer>();
            splineLength = splineComponent.CalculateLength();
            agentComponent = GetComponent<NavMeshAgent>();

            ResetWalkAndPauseDurations();
        }

        public void ResetWalkAndPauseDurations()
        {
            pauseDuration = UnityEngine.Random.Range(pauseDurationMinimum, pauseDurationMaximum);
            walkDuration = UnityEngine.Random.Range(walkDurationMinimum, walkDurationMaximum);
        }

        public Vector3 GetNextPosition()
        {
            return splineComponent.EvaluatePosition(splinePosition);
        }

        public void CalculateNextPosition()
        {
            walkTime += Time.deltaTime;

            if (walkTime > walkDuration)
            {
                isWalking = false;
            }

            if (!isWalking)
            {
                pauseTime += Time.deltaTime;

                if (pauseTime < pauseDuration)
                {
                    return;
                }

                ResetTimers();
                ResetWalkAndPauseDurations();
            }

            lengthWalked += Time.deltaTime * agentComponent.speed;

            if (lengthWalked > splineLength)
            {
                lengthWalked = 0f;
            }

            splinePosition = Mathf.Clamp01(lengthWalked / splineLength);
        }

        public void ResetTimers()
        {
            pauseTime = 0f;
            walkTime = 0f;
            isWalking = true;
        }

        public Vector3 GetFartherOutPosition()
        {
            float tempSplinePosition = splinePosition + 0.02f;

            if (tempSplinePosition >= 1f)
            {
                tempSplinePosition -= 1f;
            }

            return splineComponent.EvaluatePosition(tempSplinePosition);
        }
    }
}
