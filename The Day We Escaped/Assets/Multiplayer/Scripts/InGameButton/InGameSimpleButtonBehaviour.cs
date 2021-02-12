using System.Collections;
using Bolt;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class InGameSimpleButtonBehaviour : MonoBehaviour
    {
        [Tooltip("Lock states change for some time, avoid burst clicks")]
        public float lockChangeDelay = 1f;

        private bool _isWaitingLockStateDelay;
        
        [SerializeField] private int _id;

        [SerializeField] private bool _disabled;

        public UnityEvent onClick;

        private IEnumerator LockDelayRoutine()
        {
            yield return new WaitForSeconds(lockChangeDelay);
            _isWaitingLockStateDelay = false;
        }
        
        public void OnClick(PointerEventData eventData)
        {
            if (_disabled)
                return;

            BoltLog.Info($"{this} clicked | distance: {eventData.pointerCurrentRaycast.distance}");

            var evt =  IdBoltEvent.Create(GlobalTargets.Everyone);
            evt.Id = _id;
            evt.Send();
        }

        public void OnEnter(PointerEventData eventData)
        {
        }

        public void OnExit(PointerEventData eventData)
        {
        }
    }
