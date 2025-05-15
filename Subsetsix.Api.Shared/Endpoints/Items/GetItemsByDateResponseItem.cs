using JetBrains.Annotations;

namespace Subsetsix.Api.Shared.Endpoints.Items;

[PublicAPI]
public record GetItemsByDateResponseItem(
    string ItemId,
    string UserId,
    string Title,
    List<string> Tags);