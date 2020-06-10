using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    [RequireComponent(typeof(RectTransform))]
    public class MenuScreen : MonoBehaviour
    {
        protected ScreensHandler screensHandler;

        private RectTransform myRectTransform;
        protected Vector3 originalPosition = Vector3.zero;
        protected Vector3 toMovePosition = Vector3.zero;
        private bool showing = false;

        protected virtual void Awake()
        {
            screensHandler = FindObjectOfType<ScreensHandler>();

            myRectTransform = gameObject.GetComponent<RectTransform>();
            originalPosition = myRectTransform.localPosition;

            toMovePosition = Vector3.zero;
        }
        public virtual void Show()
        {
            myRectTransform.localPosition = toMovePosition;
            //TODO add animations
        }
        public virtual void Disappear()
        {
            myRectTransform.localPosition = originalPosition;
            //TODO add animations
        }

        public virtual void Toggle()
        {
            if (showing)
                Disappear();
            else
                Show();
        }
    }
}
