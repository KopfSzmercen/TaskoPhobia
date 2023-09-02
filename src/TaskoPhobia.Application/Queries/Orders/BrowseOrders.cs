using TaskoPhobia.Application.DTO;
using TaskoPhobia.Shared.Abstractions.Queries;

namespace TaskoPhobia.Application.Queries.Orders;

public record BrowseOrders : IQuery<IEnumerable<OrderDto>>;