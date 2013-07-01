using System.Reflection;

namespace FluentReflection
{
    public class Is
    {
        public static Is Instance { get { return new Is(BindingFlags.Instance); } }
        public static Is Public { get { return new Is(BindingFlags.Public); } }
        public static Is Flagged(BindingFlags flag)
        {
            return new Is(flag);
        }

        private BindingFlags _flags;

        internal Is(BindingFlags flags)
        {
            _flags = flags;
        }

        public Is And(Is flag)
        {
            _flags = _flags | flag._flags;
            return this;
        }

        internal BindingFlags Flags { get { return _flags; } }
    }
}