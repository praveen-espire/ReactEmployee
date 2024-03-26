namespace Entities.AppEntity
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DefaultLoginEmail { get; set; }
        public int BackgroundTaskTimer { get; set; }
    }
}
