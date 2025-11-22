// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaskTracker.Api.Dtos
{
    public record ProjectCreateDto(
    string Name,
    string? Description
    );

    public record ProjectReadDto(
    int Id,
    string Name,
    string? Description
    );

    public record ProjectUpdateDto(
    int Id,
    string Name,
    string? Description
    );

}
