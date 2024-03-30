namespace WorkPlaceShedulesBlazor.DTO
{
    public class TokenDTO
    {
        public string? result { get; set; } = string.Empty;
        public int id { get; set; }
        public object exception { get; set; } = string.Empty;
        public int status { get; set; }
        public bool isCanceled { get; set; }
        public bool isCompleted { get; set; }
        public bool isCompletedSuccessfully { get; set; }
        public int creationOptions { get; set; }
        public object asyncState { get; set; }
        public bool isFaulted { get; set; }
        public string Rol { get; set; } = "";
    }

}
