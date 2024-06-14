using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DPS.Models;
public class dolci
{  
    public Guid id { get; set; }

    public string nome { get; set; }

    public decimal prezzo { get; set; }

    public DateTime data { get; set; }
 
    public decimal quantita { get; set; } 

    public IEnumerable<string> ingredienti { get; set; }

    [JsonIgnore]
    [DisplayName("ingredienti")]
    public string ConcatIngredients
    { 
        get => string.Join(';', ingredienti);
    }
}