// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TaskTracker.Api.Dtos
{

    public record TaskCreateDto(
        string Title,
        string? Description,
        int ProjectId
        );

    public record TaskUpdateDto(
        string? Title,
        string? Description,
        bool IsCompleted
        );

    public record TaskSummaryDto(
       int Id,
       string Title,
       bool IsCompleted
   );

    public record TaskReadDto(
        int Id,
        string Title,
        string? Description,
        bool IsCompleted,
        DateTime CreatedDate
    );

}
