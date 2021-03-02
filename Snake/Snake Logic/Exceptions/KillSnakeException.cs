using System;

namespace Snake.Logic.Exceptions
{
    [Serializable]
    public abstract class KillSnakeException : Exception
    {
        public string Cause { get; set; }
        public  KillSnakeException() { }
        public KillSnakeException(string message) : base(message) { }
        public KillSnakeException(string message, Exception inner) : base(message, inner) { }
        protected KillSnakeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
