﻿namespace Domain.Models;
public class Client
{
  public int Id { get; set; }
  public string Name { get; set; } = null!;
  public string? ClientImage { get; set; }
}
