using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UnityEngine;

namespace UI.Core
{
    /// <summary>
    /// Essentially a section of UI, a page can be anything from an entire
    /// settings menu; down to just a simple popup panel.
    /// 
    /// It also leverages the UIAnimator tool, providing an easy way to
    /// quickly create entry and exit animations for the page.
    /// </summary>
    public class Page : MonoBehaviour, IPage
    {
        public event Action PageReady, PageClosed;
        public bool IsVisible { get; private set; }
        
        protected IPage NextPage { private get; set; }
        protected GameObject Content;
        protected List<ITransitionController> PageElements;
        protected int TransitionedElements, TotalValidElements;

        private void Awake()
        {
            Content = transform.GetChild(0).gameObject;

            PageClosed += OnPageClosed;
            
            // Temporarily enable content to grab references.
            FindReferences();
        }

        protected virtual void FindReferences()
        {
            ResetPageAnimatorData();
            
            Content.SetActive(true);
            PageElements = GetComponentsInChildren<ITransitionController>().ToList();
            foreach (var pageElement in PageElements.Where(pageElement => !pageElement.IsInManualMode()))
            {
                pageElement.TransitionComplete += OnElementTransitionComplete;

                // Determine the total amount of elements we need to wait for that are NOT in manual mode.
                TotalValidElements++;
            }
            Content.SetActive(false);
        }

        private void ResetPageAnimatorData()
        {
            TotalValidElements = 0;
            if (PageElements == null)
            {
                PageElements = new List<ITransitionController>();
            }
            else
            {
                foreach (var pageElement in PageElements.Where(pageElement => !pageElement.IsInManualMode()))
                {
                    pageElement.TransitionComplete -= OnElementTransitionComplete;
                }
                PageElements.Clear();
            }
        }
        
        protected virtual void OnDestroy()
        {
            PageClosed -= OnPageClosed;
        }

        public void Open()
        {
            Content.SetActive(true);
            foreach (var element in PageElements.Where(element => !element.IsInManualMode()))
            {
                element.StartTransition(UIAnimatorTrigger.OnEntry);
            }
        }

        public void Close()
        {
            foreach (var element in PageElements.Where(element => !element.IsInManualMode()))
            {
                element.StartTransition(UIAnimatorTrigger.OnExit);
            }
        }

        public void TriggerNextPage(IPage page)
        {
            if (page == null)
            {
                Debug.LogError($"{gameObject.name} reports it's next page is null!");

                return;
            }

            NextPage = page;
            Close();
        }
        
        private void OnElementTransitionComplete(bool isVisible)
        {
            TransitionedElements++;

            if (TransitionedElements != TotalValidElements) return;
            
            if (isVisible)
            {
                PageReady?.Invoke();
                IsVisible = true;
            }
            else
            {
                PageClosed?.Invoke();
                IsVisible = false;
                Content.SetActive(false);
            }

            TransitionedElements = 0;
        }
        
        protected virtual void OnPageClosed()
        {
            if (NextPage == null) return;
            
            NextPage.Open();
            NextPage = null;
        }
    }
}
