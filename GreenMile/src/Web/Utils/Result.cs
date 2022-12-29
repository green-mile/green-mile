using System.Text;

namespace Web.Utils
{
    public sealed class Result<T>
    {
        public Status Status { get; }
        public string Message { get; }
        public T? Value { get; private set; }

        private Result(Status status, string message, T value)
        {
            Status = status;
            Message = message;
            Value = value;
        }

        public static Result<T> Failure(string message)
        {
            return new Result<T>(Status.FAILURE, message, default);
        }

        public static Result<T> Success(string message, T value)
        {
            return new Result<T>(Status.SUCCESS, message, value);
        }

        public void Print()
        {
            if (Status == Status.SUCCESS)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (Status == Status.FAILURE)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine(ToString());
            Console.ResetColor();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Status == Status.SUCCESS)
            {
                sb.AppendLine("+------------+------------+");
                sb.AppendLine("|   Status   |   Value    |");
                sb.AppendLine("+------------+------------+");
                sb.AppendLine($"|   {Status,-10}   |   {Value,-10}   |");
                sb.AppendLine("+------------+------------+");
            }
            else if (Status == Status.FAILURE)
            {
                sb.AppendLine("+------------+------------+");
                sb.AppendLine("|   Status   |   Message  |");
                sb.AppendLine("+------------+------------+");
                sb.AppendLine($"|   {Status,-10}   |   {Message,-10}   |");
                sb.AppendLine("+------------+------------+");
            }

            return sb.ToString();
        }
    }
}