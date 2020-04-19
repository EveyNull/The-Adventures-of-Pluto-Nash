using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        
        private GameObject player;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        private float currentSpeed;
        public float distanceTravelled;
        public bool moving = true;

        private void Start()
        {
            currentSpeed = speed;
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        private void FixedUpdate()
        {



            if (pathCreator == null) return;

            if (moving && currentSpeed != speed)
            {
                currentSpeed += Time.deltaTime * speed;
                if (currentSpeed > speed) currentSpeed = speed;
            }
            else if (!moving && currentSpeed != 0)
            {
                currentSpeed -= Time.deltaTime * speed;
                if (currentSpeed < 0) currentSpeed = 0;
            }
            distanceTravelled += currentSpeed * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);

            //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction).z);
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