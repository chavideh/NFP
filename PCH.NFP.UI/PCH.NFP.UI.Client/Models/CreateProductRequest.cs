namespace PCH.NFP.UI.Client
{
    public class CreateProductRequest
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string IranCode { get; set; }
        public string SepidarCode { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public bool Publish { get; set; }
    }
}
