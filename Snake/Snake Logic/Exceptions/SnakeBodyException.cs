using System;

namespace Snake_Logic.Exceptions
{
    [Serializable]
    public class SnakeBodyException : Exception
    {
        public SnakeBodyException() { }
        public SnakeBodyException(string message) : base(message) { }
        public SnakeBodyException(string message, Exception inner) : base(message, inner) { }
        protected SnakeBodyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
