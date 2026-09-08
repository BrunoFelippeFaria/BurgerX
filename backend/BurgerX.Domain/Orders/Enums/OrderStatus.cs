namespace BurgerX.Domain.Orders.Enums;

public enum OrderStatus : byte
{
    Ordered = 1,
    InPreparation = 2,
    OutForDelivery = 3,
    Finished = 4,
    Canceled = 5
}