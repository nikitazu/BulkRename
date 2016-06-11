namespace BulkRename.Contexts
{
    public enum ActionResultType
    {
        Ok,
        Error
    }

    public struct ActionResult
    {
        public ActionResultType Type { get; set; }
        public string ErrorMessage { get; set; }

        public static readonly ActionResult Ok =
            new ActionResult { Type = ActionResultType.Ok };

        public static ActionResult Error(string message)
        {
            return new ActionResult { Type = ActionResultType.Error, ErrorMessage = message };
        }
    }
}
