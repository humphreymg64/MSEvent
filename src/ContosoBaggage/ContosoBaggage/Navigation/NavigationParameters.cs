
namespace ContosoBaggage.Navigation
{
    using System.Collections;
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Navigation parameters.
    /// </summary>
    public class NavigationParameters : IEnumerable
    {
        #region Private Properties

        /// <summary>
        /// The ns dictionary.
        /// </summary>
        private readonly IDictionary<string, object> _parameters;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ContosoBaggage.Navigation.NavigationParameters"/> class.
        /// </summary>
        public NavigationParameters()
        {
            _parameters = new Dictionary<string, object>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add the specified name and view.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="view">View.</param>
        public void Add(string key, object value)
        {
            _parameters.Add(key, value);
        }

        /// <summary>
        /// Get the specified key.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="key">Key.</param>
        public object Get(string key)
        {
            return _parameters[key];
        }

        /// <summary>
        /// Contains the specified key.
        /// </summary>
        /// <returns>The contains.</returns>
        /// <param name="key">Key.</param>
        public bool Contains(string key)
        {
            return _parameters.ContainsKey(key);
        }

        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_parameters).GetEnumerator();
        }

        #endregion
    }
}
