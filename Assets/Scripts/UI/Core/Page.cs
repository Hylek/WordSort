using System;
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
        protected GameObject Content;

        private void Awake()
        {
            Content = transform.GetChild(0).gameObject;
            
            // Temporarily enable content to grab references.
            Content.SetActive(true);
            FindReferences();
            Content.SetActive(false);
        }

        protected virtual void FindReferences()
        {
            // base class requires no implementation.
        }

        public void Open()
        {
            throw new System.NotImplementedException();
        }

        public void Close()
        {
            throw new System.NotImplementedException();
        }

        public void TriggerNextPage(IPage page)
        {
            throw new System.NotImplementedException();
        }
    }
}
