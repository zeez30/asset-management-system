namespace AssetManagementApi.Services
{
    public class AssetService
    {
        public int CalculateAge(DateTime installationDate)
        {
            var today = DateTime.Today;
            var age = today.Year - installationDate.Year;
            if (installationDate.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}