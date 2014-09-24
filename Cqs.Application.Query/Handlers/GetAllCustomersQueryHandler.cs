using System.Collections.Generic;

namespace Cqs.Application.Query.Handlers
{
    using System.Linq;

    using Cqs.Application.Query.Queries;
    using Cqs.Infrastructure.Dapper;
    using Cqs.Infrastructure.Dto;

    using DapperExtensions;

    public class GetAllCustomersQueryHandler : IQueryHandler<CustomersDto, GetAllCustomersQuery>
    {
        private readonly IDapperConnectionFactory dapperConnectionFactory;

        public GetAllCustomersQueryHandler(IDapperConnectionFactory dapperConnectionFactory)
        {
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public CustomersDto Handle(GetAllCustomersQuery query)
        {
            using (var dapperConnection = this.dapperConnectionFactory.CreateConnection())
            {
                dapperConnection.Open();
                
                var customers = dapperConnection.GetPage<CustomerDto>(
                        GetPredicate(),
                        new List<ISort> { Predicates.Sort<CustomerDto>(s => s.LastName) },
                        query.Page - 1,
                        query.ResultsPerPage)
                        .ToList();

                return new CustomersDto { Customers = customers };
            }
        }

        private static IFieldPredicate GetPredicate()
        {
            return Predicates.Field<CustomerDto>(f => f.IsActive, Operator.Eq, true);
        }
    }
}
