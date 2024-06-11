public class dolci
{ 
    public Guid id { get; set; }

    public string nome { get; set; }

    public decimal prezzo { get; set; }

    public DateTime data { get; set; }
 
    public int quantità { get; set; } 

    public IEnumerable<string> ingredienti { get; set; }
}