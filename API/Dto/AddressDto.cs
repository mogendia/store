namespace API.Dto
{
    public class AddressDto
    {
        public  string Line1 { get; set; } = string.Empty;
        public string? Line2 { get; set; }
        public  string City { get; set; } = string.Empty;
        public  string State { get; set; } = string.Empty;
        public  string ZipCode { get; set; } = string.Empty;
        public  string Country { get; set; } = string.Empty;
    }
}
