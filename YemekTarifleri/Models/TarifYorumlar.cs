namespace Yemektarifleri.Models
{
    public class TarifYorumlar
    {
        public YemekTarifleri tarif {  get; set; }
        public List<Yorumlar> yorumlar { get; set; }
    }
}
