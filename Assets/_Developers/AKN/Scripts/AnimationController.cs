using UnityEngine;

namespace Poop.Player
{
    public class AnimationController : MonoBehaviour
    {
        private const float AnimationBlendSpeed = 0.1f;
        private Animator animator;
        private PlayerControllerBase player;

        private int verticalHash;

        private void Start()
        {
            animator = GetComponent<Animator>();
            player = GetComponentInParent<PlayerControllerBase>();

            verticalHash = Animator.StringToHash("Vertical");
        }

        private void Update()
        {
            animator.SetFloat(verticalHash, player.GetMoveAmount(), AnimationBlendSpeed, Time.deltaTime);
        }
    }
}