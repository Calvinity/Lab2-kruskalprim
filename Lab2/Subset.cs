namespace Lab2
{
    public class Subset
    {
        public required int Parent { get; set; }
        public int Rank { get; set; }
        public override string ToString()
        {
            return $"Rank: {Rank}. Parent Index: {Parent}.";
        }
    }
}
