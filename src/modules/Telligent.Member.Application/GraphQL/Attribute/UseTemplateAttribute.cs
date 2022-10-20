using AutoMapper;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Telligent.Member.Application.GraphQL.Attribute
{
    public class UseTemplateAttribute : ObjectFieldDescriptorAttribute
    {
        public UseTemplateAttribute([CallerLineNumber] int order = 0)
        {
            Order = order;
        }

        public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
        {
            descriptor.Use<TemplateMiddleware>();
        }
    }

    public class TemplateMiddleware
    {
        private readonly FieldDelegate _next;

        public TemplateMiddleware(FieldDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(IMiddlewareContext context, IMapper mapper)
        {
            await _next(context);

            var result = context.Result;
        }
    }
}
