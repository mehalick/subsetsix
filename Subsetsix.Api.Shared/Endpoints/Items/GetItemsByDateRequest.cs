using JetBrains.Annotations;

namespace Subsetsix.Api.Shared.Endpoints.Items;

[PublicAPI]
public record GetItemsByDateRequest(DateOnly Date);