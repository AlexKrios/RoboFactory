using System.Collections;
using UnityEngine;
using Utils;

namespace Modules.Battle.Units
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Scripts/Battle/Unit Animation", 1)]
    public class UnitAnimation : MonoBehaviour
    {
        private const int Speed = 3;
        
        private const string AttackKey = "Attack";
        private const string TossGrenadeKey = "TossGrenade";
        private const string HitKey = "Hit";
        private const string DeadKey = "Dead";
        
        private static readonly int Attack = Animator.StringToHash(AttackKey);
        private static readonly int Hit = Animator.StringToHash(HitKey);
        private static readonly int Dead = Animator.StringToHash(DeadKey);
        private static readonly int TossGrenade = Animator.StringToHash(TossGrenadeKey);
        
        private static readonly int WalkX = Animator.StringToHash("WalkX");
        private static readonly int WalkZ = Animator.StringToHash("WalkZ");
        
        private Rigidbody _unitRigidbody;
        private Animator _unitAnimator;
        
        private Vector3 _moveDirection;

        private void Awake()
        {
            _unitRigidbody = GetComponent<Rigidbody>();
            _unitAnimator = GetComponent<Animator>();
            
            _unitRigidbody.constraints = (RigidbodyConstraints) 80;
        }

        public IEnumerator PlayAttack()
        {
            _unitAnimator.SetTrigger(Attack);
            yield return new WaitForSeconds(AnimatorUtil.FindAnimation(_unitAnimator, AttackKey).length);
        }
        
        public IEnumerator PlayTossGrenade()
        {
            _unitAnimator.SetTrigger(TossGrenade);
            yield return new WaitForSeconds(AnimatorUtil.FindAnimation(_unitAnimator, TossGrenadeKey).length);
        }
        
        public IEnumerator PlayHit()
        {
            _unitAnimator.SetTrigger(Hit);
            yield return new WaitForSeconds(AnimatorUtil.FindAnimation(_unitAnimator, HitKey).length);
        }
        
        public IEnumerator PlayDead()
        {
            _unitAnimator.SetTrigger(Dead);
            yield return new WaitForSeconds(AnimatorUtil.FindAnimation(_unitAnimator, DeadKey).length);
        }

        public void Update()
        {
            var axisX = Input.GetAxis("Horizontal");
            var axisZ = Input.GetAxis("Vertical");
            
            _moveDirection = new Vector3(axisX , 0, axisZ).normalized;
            _unitAnimator.SetFloat(WalkX, axisX);
            _unitAnimator.SetFloat(WalkZ, axisZ);
        }
        
        private void FixedUpdate()
        {
            _unitRigidbody.velocity = _moveDirection * Speed;
        }
    }
}