namespace Second
{
    interface ICoffeeMachineState
    {
        void InsertCoin();
        void SelectDrink(string drink);
        void DispenseDrink();
        string GetState();
    }

    class WaitingForCoinState : ICoffeeMachineState
    {
        private CoffeeMachine _coffeeMachine;

        public WaitingForCoinState(CoffeeMachine coffeeMachine)
        {
            _coffeeMachine = coffeeMachine;
        }

        public void InsertCoin()
        {
            Console.WriteLine("Монета внесена.");
            _coffeeMachine.SetState(new SelectingDrinkState(_coffeeMachine));
        }

        public void SelectDrink(string drink)
        {
            Console.WriteLine("Пожалуйста, внесите монету.");
        }

        public void DispenseDrink()
        {
            Console.WriteLine("Пожалуйста, внесите монету и выберите напиток.");
        }

        public string GetState()
        {
            return "Ожидание монеты";
        }
    }

    class SelectingDrinkState : ICoffeeMachineState
    {
        private CoffeeMachine _coffeeMachine;

        public SelectingDrinkState(CoffeeMachine coffeeMachine)
        {
            _coffeeMachine = coffeeMachine;
        }

        public void InsertCoin()
        {
            Console.WriteLine("Монета уже внесена. Выберите напиток.");
        }

        public void SelectDrink(string drink)
        {
            Console.WriteLine($"{drink} выбран.");
            _coffeeMachine.SetState(new DispensingDrinkState(_coffeeMachine, drink));
        }

        public void DispenseDrink()
        {
            Console.WriteLine("Пожалуйста, выберите напиток.");
        }

        public string GetState()
        {
            return "Выбор напитка";
        }
    }

    class DispensingDrinkState : ICoffeeMachineState
    {
        private CoffeeMachine _coffeeMachine;
        private string _selectedDrink;

        public DispensingDrinkState(CoffeeMachine coffeeMachine, string selectedDrink)
        {
            _coffeeMachine = coffeeMachine;
            _selectedDrink = selectedDrink;
        }

        public void InsertCoin()
        {
            Console.WriteLine("Напиток уже готовится. Подождите.");
        }

        public void SelectDrink(string drink)
        {
            Console.WriteLine("Напиток уже выбран. Подождите.");
        }

        public void DispenseDrink()
        {
            Console.WriteLine($"{_selectedDrink} выдан.");
            _coffeeMachine.SetState(new ReadyState(_coffeeMachine));
        }

        public string GetState()
        {
            return "Выдача напитка";
        }
    }

    class ReadyState : ICoffeeMachineState
    {
        private CoffeeMachine _coffeeMachine;

        public ReadyState(CoffeeMachine coffeeMachine)
        {
            _coffeeMachine = coffeeMachine;
        }

        public void InsertCoin()
        {
            Console.WriteLine("Монета внесена.");
            _coffeeMachine.SetState(new SelectingDrinkState(_coffeeMachine));
        }

        public void SelectDrink(string drink)
        {
            Console.WriteLine("Пожалуйста, внесите монету.");
        }

        public void DispenseDrink()
        {
            Console.WriteLine("Пожалуйста, внесите монету и выберите напиток.");
        }

        public string GetState()
        {
            return "Готов";
        }
    }

    class CoffeeMachine
    {
        private ICoffeeMachineState _currentState;

        public CoffeeMachine()
        {
            _currentState = new WaitingForCoinState(this);
        }

        public void SetState(ICoffeeMachineState state)
        {
            _currentState = state;
            Console.WriteLine($"Состояние автомата изменено на: {_currentState.GetState()}");
        }

        public void InsertCoin()
        {
            _currentState.InsertCoin();
        }

        public void SelectDrink(string drink)
        {
            _currentState.SelectDrink(drink);
        }

        public void DispenseDrink()
        {
            _currentState.DispenseDrink();
        }
    }

    class Program
    {
        static void main(string[] args)
        {
            CoffeeMachine coffeeMachine = new CoffeeMachine();

            Console.WriteLine("Пользователь: ");
            coffeeMachine.InsertCoin();
            coffeeMachine.SelectDrink("Эспрессо");
            coffeeMachine.DispenseDrink();
            Console.WriteLine(": Автомат возвращается в состояние ожидания следующего клиента.");

        }
    }
}