using Bogus;
using System;
using System.Collections.Generic;

namespace LBHFSSPublicAPI.Tests.TestHelpers
{
    public static class ExceptionThrower
    {
        private static int _numberOfCases = 12;
        private static int _numberOfSimpleCases = 7;
        private static Faker _faker = new Faker();

        public static Tuple<Exception, List<string>> GenerateExceptionAndCorrespondinExceptionMessages(ExceptionType options = ExceptionType.AnyException)
        {
            List<string> exception_messages = new List<string>();

            var random_exception = GenerateException(options);

            try // throw random exception
            {
                throw random_exception;
            }
            catch (Exception ex) when (ex.InnerException != null)
            {
                exception_messages.Add(ex.Message);
                exception_messages.Add(ex.InnerException.Message);
            }
            catch (Exception ex) // catch the expected exception message
            {
                exception_messages.Add(ex.Message);
            }

            return new Tuple<Exception, List<string>>(random_exception, exception_messages);
        }

        public static Exception GenerateException(ExceptionType options = ExceptionType.AnyException)
        {
            int random_exception_number; // triangulating unexpected exceptions

            switch (options)
            {
                case ExceptionType.SimpleException: random_exception_number = _faker.Random.Int(0, _numberOfSimpleCases - 1); break;
                case ExceptionType.InnerException: random_exception_number = _faker.Random.Int(_numberOfSimpleCases, _numberOfCases - 1); break;
                default: random_exception_number = _faker.Random.Int(0, _numberOfCases - 1); break;
            }

            string str = _faker.Random.Hash().ToString(); // triangulating exception messages

            switch (random_exception_number)
            {
                case 0: return new OutOfMemoryException(str);
                case 1: return new IndexOutOfRangeException(str);
                case 2: return new ArgumentOutOfRangeException(str);
                case 3: return new MissingFieldException(str);
                case 4: return new OverflowException(str);
                case 5: return new TimeoutException(str);
                case 6: return new StackOverflowException(str);
                case 7: return GenerateInnerArgumentNullException(str);
                case 8: return GenerateInnerApplicationException(str);
                case 9: return GenerateInnerInvalidCastException(str);
                case 10: return GenerateInnerSystemException(str);
                default: return GenerateInnerAggregateException(str);
            }
        }

        private static SystemException GenerateInnerSystemException(string random_message_addon)
        {
            try { throw new SystemException("Inner SystemException exception thrown." + random_message_addon); }
            catch (SystemException e) { return new SystemException("Outer SystemException exception thrown.", e); }
        }

        private static InvalidCastException GenerateInnerInvalidCastException(string random_message_addon)
        {
            try { throw new InvalidCastException("Inner InvalidCastException exception thrown." + random_message_addon); }
            catch (InvalidCastException e) { return new InvalidCastException("Outer InvalidCastException exception thrown.", e); }
        }

        private static ArgumentNullException GenerateInnerArgumentNullException(string random_message_addon)
        {
            try { throw new ArgumentNullException("Inner ArgumentNullException exception thrown." + random_message_addon); }
            catch (ArgumentNullException e) { return new ArgumentNullException("Outer ArgumentNullException exception thrown.", e); }
        }

        private static ApplicationException GenerateInnerApplicationException(string random_message_addon)
        {
            try { throw new ApplicationException("Inner ApplicationException exception thrown." + random_message_addon); }
            catch (ApplicationException e) { return new ApplicationException("Outer ApplicationException exception thrown.", e); }
        }

        private static AggregateException GenerateInnerAggregateException(string random_message_addon)
        {
            try { throw new AggregateException("Inner AggregateException exception thrown." + random_message_addon); }
            catch (AggregateException e) { return new AggregateException("Outer AggregateException exception thrown.", e); }
        }

        public enum ExceptionType
        {
            SimpleException,
            InnerException,
            AnyException
        }
    }
}
