using System;

namespace Snake.Logic.Exceptions
{
    [Serializable]
    public class SnakeWallException : Exception
    {
        public SnakeWallException() { }
        public SnakeWallException(string message) : base(message) { }
        public SnakeWallException(string message, Exception inner) : base(message, inner) { }
        protected SnakeWallException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
