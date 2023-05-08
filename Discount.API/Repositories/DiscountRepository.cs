using Dapper;
using Discount.API.Entities;
using Npgsql;
using System.Data;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IDbConnection _connection;

    public DiscountRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        var affected = await _connection.ExecuteAsync(
            @"insert into Coupon (ProductName, Description, Amount)
            values (@ProductName, @Description, @Amount)", coupon);

        return affected > 0 ? true : false;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        var affected = await _connection.ExecuteAsync(
         @"delete from Coupon where ProductName = @ProductName");

        return affected > 0 ? true : false;
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        var coupon = await _connection.QueryFirstOrDefaultAsync<Coupon>(
            "select * from Coupon where ProductName = @ProductName", new { ProductName = productName });

        if (coupon == default)
        {
            return new Coupon { ProductName = "No discount", Amount= 0 };
        }

        return coupon;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        var affected = await _connection.ExecuteAsync(
          @"update Coupon set 
            ProductName = @ProductName, 
            Description = @Description, 
            Amount = @Amount)
          where Id = @Id)", coupon);

        return affected > 0 ? true : false;
    }
}
