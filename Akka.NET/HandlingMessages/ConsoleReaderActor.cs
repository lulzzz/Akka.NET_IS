using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlingMessages
{
    class ConsoleReaderActor : UntypedActor
    {
        public const string ExitCommand = "exit";
        public const string StartCommand = "start";

        private IActorRef _consoleWriterActor;

        public ConsoleReaderActor(IActorRef consoleWriterActor)
        {
            _consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object message)
        {
            if(message.Equals(StartCommand))
            {
                DoPrintInstructions();
            }
            else if (message is Messages.InputError)
            {
                _consoleWriterActor.Tell(message as Messages.InputError);
            }
            GetAndValidateInput();
        }

        #region Internal methods
        private void DoPrintInstructions()
        {
            Console.WriteLine("Write whatever you want into the console!");
            Console.WriteLine("Some entries will pass validation, and some won't...\n\n");
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
        }

        /// <summary>
        /// Reads input from console, validates it, then signals appropriate response
        /// (continue processing, error, success, etc.).
        /// чтение данных из консоли, проверка, соответствующий ответ
        /// </summary>
        private void GetAndValidateInput()
        {
            var message = Console.ReadLine();
            if (string.IsNullOrEmpty(message))
            {
                // если ничего не ввели
                Self.Tell(new Messages.NullInputError("No input received."));
            }
            else if (String.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                // ввели команду выхода
                Context.System.Terminate();
            }
            else
            {
                var valid = IsValid(message);
                if (valid)
                {
                    // валидное сообщение
                    _consoleWriterActor.Tell(new Messages.InputSuccess("Thank you! Message was valid."));
        
                    // продолжаем обрабатывать сообщения
                    Self.Tell(new Messages.ContinueProcessing());
                }
                else
                {
                    Self.Tell(new Messages.ValidationError("Invalid: input had odd number of characters."));
                }
            }
        }

        // сообщение валидное если четное количество знаков
        private static bool IsValid(string message)
        {
            var valid = message.Length % 2 == 0;
            return valid;
        }
        #endregion
    }
}
