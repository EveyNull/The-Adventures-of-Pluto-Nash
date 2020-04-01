using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower2 : MonoBehaviour
    {
        
        private GameObject player;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        private float speed = 3;
        public float maxSpeed = 3;
        public float distanceTravelled;
        public bool moving = true;

        private void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        private void Update()
        {
            if (pathCreator == null) return;
            
            if (moving && speed != maxSpeed)
            {
                speed += Time.deltaTime * maxSpeed;
                if (speed > maxSpeed) speed = maxSpeed;
            }
            else if (!moving && speed != 0)
            {
                speed -= Time.deltaTime * maxSpeed;
                if (speed < 0) speed = 0;
            }
            distanceTravelled += speed * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);

            //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            player = other.gameObject;
            other.transform.SetParent(this.transform);
            Debug.Log("parented");
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            player = null;
            other.transform.SetParent(null);
            Debug.Log("unparented");
        }
    }
}