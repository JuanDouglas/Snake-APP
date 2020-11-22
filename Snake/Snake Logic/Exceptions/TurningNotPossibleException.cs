using System;

namespace Snake_Logic.Exceptions
{
    [Serializable]
    public class TurningNotPossibleException : Exception
    {
        public TurningNotPossibleException() { }
        public TurningNotPossibleException(string message) : base(message) { }
        public TurningNotPossibleException(string message, Exception inner) : base(message, inner) { }
        protected TurningNotPossibleException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
