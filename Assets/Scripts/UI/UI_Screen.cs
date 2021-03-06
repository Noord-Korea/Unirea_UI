﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Unirea.UI
{
    // Requirements for a screen (¡Muy Importante!) Without the animations won't work
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_Screen : MonoBehaviour
    {
        #region Varibles & Properties

        [Header("Properties")]
        public Selectable startSelectable;

        [Header("Events")]
        public UnityEvent onScreenStart = new UnityEvent();
        public UnityEvent onScreenClose = new UnityEvent();

        private Animator animator;

        #endregion


        #region Methods

        // Use this for initialization
        void Awake()
        {
            animator = GetComponent<Animator>();

            // If there is a startSelectable --> highlight the startSelectable gameobject
            if (startSelectable)
            {
                EventSystem.current.SetSelectedGameObject(startSelectable.gameObject);
            }
        }

        public virtual void StartScreen()
        {
            if (onScreenStart != null)
            {
                onScreenStart.Invoke();
                switch (gameObject.name)
                {
                    case "WorldMap_Screen":
                        EventManager.MapUpdate();
                        break;

                    case "CityOverview_Screen":
                        EventManager.TownUpdate();
                        break;

                };
            }
            HandleAnimator("show");
        }

        private void EventNotifier() { }

        public virtual void CloseScreen()
        {
            // If there are listeners --> start event
            if (onScreenClose != null)
            {
                onScreenClose.Invoke();
            }

            HandleAnimator("hide");
        }

        private void HandleAnimator(string triggerType)
        {
            if (animator)
            {
                animator.SetTrigger(triggerType);
            }
        }

        #endregion
    }
}