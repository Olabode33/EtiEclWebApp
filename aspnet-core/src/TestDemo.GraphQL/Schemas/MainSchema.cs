using Abp.Dependency;
using GraphQL;
using GraphQL.Types;
using TestDemo.Queries.Container;

namespace TestDemo.Schemas
{
    public class MainSchema : Schema, ITransientDependency
    {
        public MainSchema(IDependencyResolver resolver) :
            base(resolver)
        {
            Query = resolver.Resolve<QueryContainer>();
        }
    }
}