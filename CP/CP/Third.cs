namespace Third
{
    public interface IChatMember
    {
        string Name { get; }
        void ReceiveMessage(string message, string sender);
    }


    public interface IChatMediator
    {
        void RegisterMember(IChatMember member);
        void UnregisterMember(IChatMember member);
        void SendMessage(string message, string sender);
    }


    public class ChatMediator : IChatMediator
    {
        private readonly List<IChatMember> _members = new List<IChatMember>();

        public void RegisterMember(IChatMember member)
        {
            _members.Add(member);
            Console.WriteLine($"{member.Name} присоединился к чату.");
        }

        public void UnregisterMember(IChatMember member)
        {
            _members.Remove(member);
            Console.WriteLine($"{member.Name} покинул чат.");
        }

        public void SendMessage(string message, string sender)
        {
            if (!_members.Any(m => m.Name == sender))
            {
                Console.WriteLine($"Пользователь {sender} не найден в чате.");
                return;
            }

            var messageToSend = $"{sender}: {message}";
            foreach (var member in _members)
            {
                if (member.Name != sender)
                {
                    member.ReceiveMessage(message, sender);
                }
            }
        }
    }

    public class ChatMember : IChatMember
    {
        public string Name { get; }

        public ChatMember(string name)
        {
            Name = name;
        }

        public void ReceiveMessage(string message, string sender)
        {
            Console.WriteLine($"{Name}: {message}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var mediator = new ChatMediator();

            var member1 = new ChatMember("Alice");
            var member2 = new ChatMember("Bob");
            var member3 = new ChatMember("Charlie");

            mediator.RegisterMember(member1);
            mediator.RegisterMember(member2);
            mediator.RegisterMember(member3);

            mediator.SendMessage("Привет, все!", "Alice");
            mediator.SendMessage("Как дела?", "Bob");
            mediator.SendMessage("Все хорошо!", "Charlie");


            mediator.UnregisterMember(member2);
            mediator.SendMessage("Пока!", "Alice");

        }
    }
}