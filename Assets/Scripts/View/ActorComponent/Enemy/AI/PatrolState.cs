using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class PatrolState : IAIState
    {
        private IAIContext _context;
        private bool _isDwell;
        private float _dwellElapsedTime;

        public PatrolState(IAIContext context, bool isPatrolImmediately = true)
        {
            _context = context;
            _context.ChangeRunState(false);
            if (isPatrolImmediately)
            {
                _context.ChangeDirection(_context.PatrolPath[_context.PatrolTargetIndex] - _context.Transform.position);
            }
            else
            {
                _isDwell = true;
                _context.ChangeDirection(Vector3.zero);
            }
        }

        public void Update()
        {
            if (Vector3.Distance(_context.Transform.position, _context.ChaseTarget.position) <= _context.ChaseDistance)
            {
                _context.State = new FightState(_context);
                return;
            }
            if (_isDwell)
            {
                _dwellElapsedTime += Time.deltaTime;
                if (_dwellElapsedTime >= _context.DwellTime)
                {
                    _isDwell = false;
                    _dwellElapsedTime = 0;
                    _context.ChangeDirection(_context.PatrolPath[_context.PatrolTargetIndex] - _context.Transform.position);
                }
                return;
            }
            Vector3 currentPosition = _context.Transform.position;
            Vector3 destination = _context.PatrolPath[_context.PatrolTargetIndex];
            if (Vector3.Distance(currentPosition, destination) <= _context.PatrolTargetTolerance)
            {
                _context.PatrolTargetIndex = (int)Mathf.Repeat(_context.PatrolTargetIndex + 1, _context.PatrolPath.Length);
                _isDwell = true;
                _context.ChangeDirection(Vector3.zero);
            }
            else if (Vector3.Angle(_context.Transform.forward, destination - currentPosition) >= 10)
            {
                _context.ChangeDirection(destination - currentPosition);
            }
        }
    }
}