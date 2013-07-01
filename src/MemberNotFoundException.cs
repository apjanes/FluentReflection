using System;
using System.Reflection;

namespace FluentReflection
{
    public class MemberNotFoundException: Exception
    {
        public MemberNotFoundException(Type type, string memberName, BindingFlags flags) :
            this(type, memberName, "member", flags)
        {
        }

        protected MemberNotFoundException(Type type, string memberName, string memberType, BindingFlags flags) :
            base(CreateMessage(type, memberName, memberType, flags))
        {
            MemberType = type;
            MemberName = memberName;
            BindingFlags = flags;
        }

        public BindingFlags BindingFlags { get; private set; }

        public Type MemberType { get; private set; }

        public string MemberName { get; private set; }

        private static string CreateMessage(Type type, string memberName, string memberType, BindingFlags flags)
        {
            var flagstr = "";
            if (flags.HasFlag(BindingFlags.Public))
            {
                flagstr += " public";
            }
            else if(flags.HasFlag(BindingFlags.NonPublic))
            {
                flagstr += " non-public";
            }

            if (flags.HasFlag(BindingFlags.Static))
            {
                if (flagstr.Length > 0) flagstr += ",";
                flagstr += " static";
            }
            else if(flags.HasFlag(BindingFlags.Instance))
            {
                if (flagstr.Length > 0) flagstr += ",";
                flagstr += " instance";
            }
            var message = "The requested" + flagstr + " " + memberType;
            if(memberName != null) message += ": '" + memberName + "'";
            if(type != null) message += " for type: '" + type.FullName + "'"; 
            message += " cannot be found.";
            return message;
        }
    }
}