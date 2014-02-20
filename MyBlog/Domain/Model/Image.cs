namespace Domain.Model
{
    public class Image
    {
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string ImageDescription { get; set; }
        public string ImageName { get; set; }
        public int  PostImageId { get; set; }
        public bool IsSmall { get; set; }
    }
}