using Blazored.SessionStorage;
using Newtonsoft.Json;
using System.Text.Json;
namespace WorkPlaceShedulesBlazor.Storage
{
    public static class SessionStorageExtension
    {
        public static async Task SaveStorage<T>(
           this ISessionStorageService sessionStorageService,
           string key, T item
           ) where T : class
        {

          try
            {
                var itemJson = JsonConvert.SerializeObject(item);
                await sessionStorageService.SetItemAsStringAsync(key, itemJson);
            }
            catch(Exception ex ) {
                string e = ex.ToString();
                string dddd = "";
            }

        }

        public static async Task<T?> GetStorage<T>(
        this ISessionStorageService sessionStorageService,
        string key
        ) where T : class
        {
            var itemJson = await sessionStorageService.GetItemAsStringAsync(key);

            if (itemJson != null)
            {
                var item = JsonConvert.DeserializeObject<T>(itemJson);
                return item;
            }
            else
                return null;
        }

    }
}
