using JetBrains.Annotations;

namespace Subsetsix.Api.Shared.Endpoints.Items;

[PublicAPI]
public record GetItemsByDateResponse(List<GetItemsByDateResponseItem> Items);