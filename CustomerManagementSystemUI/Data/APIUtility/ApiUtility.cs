namespace CustomerManagementSystemUI.Data.APIUtility
{
    public static class ApiUtility
    {
        public static string BaseUrl { get; set; }

        public static void Configure(IConfiguration configuration)
        {
            
            BaseUrl = configuration["ApiSettings:BaseUrl"];
        }
    }

}
