using System;

namespace UniCorn.Input
{
    public struct Input
    {
        public readonly Type Type;
        public readonly object Value;
        public readonly InputStatus Status;

        public Input(Type type, object value, InputStatus status)
        {
            Type = type;
            Value = value;
            Status = status;
        }

        public override string ToString()
        {
            return $"Action triggered: {Type.Name} {Value} {Status}";
        }
    }
}
