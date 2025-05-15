namespace Subsetsix.Api.Shared;

public static class ApiRoutes
{
    public static class Items
    {
        public const string GetItemsByDate = "/items/getItemsByDate";

        public static string GetItemsByDateWithQuery(DateOnly date)
        {
            return $"{GetItemsByDate}?date={date:yyyy-MM-dd}";
        }
    }
}