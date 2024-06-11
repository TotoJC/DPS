using System.ComponentModel.DataAnnotations;

namespace DPS.Models;
public class Sweet
{
    private Guid _id;
    private string _name;
    private decimal _price;
    private DateTime _date;
    private int _quantity;
    private IEnumerable<string> _ingredients;


    [Required]
    public Guid ID { get => _id; set => _id = value; }

    [Required]
    public string Name { get => _name; set => _name = value; }

    [Required]
    public decimal Price { get => _price; set => _price = value; }

    [DataType(DataType.Date)]
    [Required]
    public DateTime Date { get => _date; set => _date = value; }

    [Required]
    public int Quantity { get => _quantity; set => _quantity = value; } 

    public IEnumerable<string> Ingredients { get => _ingredients; set => _ingredients = value; }
}