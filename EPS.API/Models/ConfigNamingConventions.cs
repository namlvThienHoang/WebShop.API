using System;
using System.Collections.Generic;
using System.Reflection;
using HotChocolate;
using HotChocolate.Language;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

namespace EPS.API.Models
{
    public class MyDefaultTypeInspector : DefaultTypeInspector
    {
        public override IEnumerable<MemberInfo> GetMembers(Type type)
        {
            var m = base.GetMembers(type);
            return m;
        }
    }
    public class MyConfigNamingConventions : DefaultNamingConventions
    {
        public MyConfigNamingConventions() : base()
        {

        }
        public MyConfigNamingConventions(IDocumentationProvider documentation) : base(documentation)
        {

        }
        // public override NameString GetArgumentName(ParameterInfo parameter){
        //     if (parameter == null)
        //         {
        //             throw new ArgumentNullException(nameof(parameter));
        //         }
        //     return parameter.Name;

        // }

        public override NameString GetMemberName(MemberInfo member, MemberKind kind)
        {

            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (member is PropertyInfo p)
            {
                return p.Name;
            }

            return base.GetMemberName(member, kind);
        }

    }
}