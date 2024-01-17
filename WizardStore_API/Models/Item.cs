using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace WizarStore_API.Models;

public class Item 
{
    public long Id {get; set;}
    
    [Required]
    public string? Name {get; set;}

    [Required]
    public string? Description {get; set;}

    [Required]
    public int Quantity {get; set;}

    [Required]
    public int MagicPower {get; set;}

}