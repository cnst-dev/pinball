using System;

namespace ConstantineSpace.Tools
{
    /// <summary>
    ///     Use this class to make the value observable.
    /// </summary>
    /// <typeparam name="T"> The value.</typeparam>
    public class Observer<T>
    {
        private T _currentValue;

        public Observer(T initialValue)
        {
            _currentValue = initialValue;
        }

        public T Value
        {
            get { return _currentValue; }

            set
            {
                if (_currentValue.Equals(value)) return;
                _currentValue = value;

                var args = new ChangedValueArgs<T>(_currentValue);

                if (OnValueChanged != null) OnValueChanged(this, args);
            }
        }

        /// <summary>
        ///     Uses for subscriptions.
        /// </summary>
        public event EventHandler<ChangedValueArgs<T>> OnValueChanged;

        /// <summary>
        ///     Uses for set value without messaging.
        /// </summary>
        /// <param name="value">The value.</param>
        public void ChangeValue(T value)
        {
            _currentValue = value;
        }
    }

    public class ChangedValueArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public ChangedValueArgs(T value)
        {
            Value = value;
        }
    }
}