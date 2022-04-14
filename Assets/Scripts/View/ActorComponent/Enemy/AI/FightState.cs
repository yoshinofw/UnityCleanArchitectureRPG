using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class FightState : IAIState
    {
        private IAIContext _context;
        private float _currentCD;
        private bool _canAttack;

        public FightState(IAIContext context)
        {
            _context = context;
            _context.ChangeRunState(true);
            _currentCD = _context.AttackCD;
            _canAttack = true;
        }

        public void Update()
        {
            UpdateAttackCD();
            Vector3 currentPosition = _context.Transform.position;
            Vector3 targetPosition = _context.ChaseTarget.position;
            float distanceToTarget = Vector3.Distance(currentPosition, targetPosition);
            if (distanceToTarget > _context.ChaseDistance)
            {
                _context.State = new PatrolState(_context, false);
                return;
            }
            if (distanceToTarget > _context.AttackDistance || Vector3.Angle(_context.Transform.forward, targetPosition - currentPosition) > _context.HalfAttackAngle)
            {
                _context.ChangeDirection(targetPosition - currentPosition);
                return;
            }
            if (_canAttack)
            {
                _context.PerformAction("Attack");
                _canAttack = false;
            }
            else
            {
                _context.ChangeDirection(Vector3.zero);
            }
        }

        private void UpdateAttackCD()
        {
            if (!_canAttack)
            {
                _currentCD -= Time.deltaTime;
                if (_currentCD <= 0)
                {
                    _canAttack = true;
                    _currentCD = _context.AttackCD;
                }
            }
        }
    }
}