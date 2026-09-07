using BurgerX.Domain.Exceptions;

namespace BurgerX.Application.Orders.Exceptions;

public class OrderNotFoundException(Guid id) : NotFoundException($"pedido {id} não encontrado.")
{
    public override string Code => "order_not_found";
}