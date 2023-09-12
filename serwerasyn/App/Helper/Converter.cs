namespace serwer.App.Helper
{
    internal class Converter
    {
        internal static DbModels.Usser ConvertToDbUser(Models.Usser user)
        {
            return new DbModels.Usser()
            {
                Name = user.Name,
                Password = user.Password,
            };
        }

        internal static Models.Usser ConvertToUser(DbModels.Usser user)
        {
            return new Models.Usser()
            {
                Name = user.Name,
                Password = user.Password,
            };
        }
    }
}
