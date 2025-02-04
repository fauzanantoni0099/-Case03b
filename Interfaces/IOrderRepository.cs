using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesOrder.DTOs;

namespace SalesOrder.Interfaces;

public interface IOrderRepository
{
    Task<List<OrderDto>> GetOrdersAsync(string keyword, DateTime? orderDate, DateTime? orderDateTo);
    IQueryable<OrderDto> GetOrdersQueryable(string keyword, DateTime? orderDate, DateTime? orderDateTo);
    Task<OrderDto> GetOrderByIdAsync(int id);
    Task<List<CustomerDto>> GetCustomersAsync();
    Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
    Task<OrderDto> UpdateOrderAsync(OrderDto orderDto); // Tambahkan ini
    Task DeleteOrderAsync(long id);
    Task<decimal> GetTotalOrderAmountAsync(int orderId);
}