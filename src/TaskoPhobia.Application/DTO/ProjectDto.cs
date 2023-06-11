﻿namespace TaskoPhobia.Application.DTO;

public class ProjectDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}