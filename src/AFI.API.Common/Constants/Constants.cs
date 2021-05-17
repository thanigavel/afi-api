namespace AFI.API.Common.Constants
{
    public static class Constants
    {
        public const int NAMEMINLENGTH = 3;
        public const int NAMEMAXLENGTH = 50;
        public const string POLICYREFERENCEFORMAT = @"^[A-Z]{2}-\d{6}$";
        public const int MINIMUMAGEINYEARS = 18;
        public const string EMAILADDRESSFORMAT = @"^(?i)[a-zA-Z0-9]{4,}@[a-zA-Z0-9]{2,}(?:.com|.co.uk)$";
    }
}