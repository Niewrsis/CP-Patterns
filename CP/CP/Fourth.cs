namespace Fourth
{
    public interface IOrder
    {
        double GetPrice();
        string GetDescription();
    }

    public class BaseOrder : IOrder
    {
        protected double _basePrice;

        public BaseOrder(double basePrice)
        {
            _basePrice = basePrice;
        }

        public virtual double GetPrice()
        {
            return _basePrice;
        }

        public virtual string GetDescription()
        {
            return "Базовый заказ";
        }
    }

    public class ExpressDeliveryDecorator : BaseOrder
    {
        private IOrder _order;
        private double _expressDeliveryCost;

        public ExpressDeliveryDecorator(IOrder order, double expressDeliveryCost) : base(0)
        {
            _order = order;
            _expressDeliveryCost = expressDeliveryCost;
        }

        public override double GetPrice()
        {
            return _order.GetPrice() + _expressDeliveryCost;
        }

        public override string GetDescription()
        {
            return $"{_order.GetDescription()}, оперативная доставка";
        }
    }

    public class GiftWrappingDecorator : BaseOrder
    {
        private IOrder _order;
        private double _giftWrappingCost;

        public GiftWrappingDecorator(IOrder order, double giftWrappingCost) : base(0)
        {
            _order = order;
            _giftWrappingCost = giftWrappingCost;
        }

        public override double GetPrice()
        {
            return _order.GetPrice() + _giftWrappingCost;
        }

        public override string GetDescription()
        {
            return $"{_order.GetDescription()}, упаковка подарков";
        }
    }

    public class DrinksDecorator : BaseOrder
    {
        private IOrder _order;
        private double _drinksCost;

        public DrinksDecorator(IOrder order, double drinksCost) : base(0)
        {
            _order = order;
            _drinksCost = drinksCost;
        }

        public override double GetPrice()
        {
            return _order.GetPrice() + _drinksCost;
        }

        public override string GetDescription()
        {
            return $"{_order.GetDescription()}, добавление напитков";
        }
    }
    public class Program
    {
        public static void main(string[] args)
        {
            IOrder baseOrder = new BaseOrder(100);
            Console.WriteLine($"Цена: {baseOrder.GetPrice()}, Описание: {baseOrder.GetDescription()}");

            IOrder expressOrder = new ExpressDeliveryDecorator(baseOrder, 30);
            Console.WriteLine($"Цена: {expressOrder.GetPrice()}, Описание: {expressOrder.GetDescription()}");

            IOrder giftOrder = new GiftWrappingDecorator(expressOrder, 20);
            Console.WriteLine($"Цена: {giftOrder.GetPrice()}, Описание: {giftOrder.GetDescription()}");

            IOrder finalOrder = new DrinksDecorator(giftOrder, 15);
            Console.WriteLine($"Цена: {finalOrder.GetPrice()}, Описание: {finalOrder.GetDescription()}");

        }
    }
}