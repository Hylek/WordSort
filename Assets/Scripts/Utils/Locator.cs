using System;
using System.Collections.Generic;
using DylansDen.Scripts.Utils;

namespace Utils
{
    public class Locator : Singleton<Locator>
    {
        private IDictionary<Type, object> _services;

        public override void Awake()
        {
            base.Awake();

            _services = new Dictionary<Type, object>(); 

            // todo: Add new services here!
            _services.Add(typeof(TinyMessengerHub), new TinyMessengerHub());
        }

        public T Find<T>()
        {
            try
            {
                return (T)_services[typeof(T)];
            }
            catch
            {
                throw new ApplicationException("The requested service could not be found!");
            }
        }

        // Explicitly declare services for simplicity.
        public TinyMessengerHub EventHub => Find<TinyMessengerHub>();
    }
}
