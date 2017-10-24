using smaaahh_wpf.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace smaaahh_wpf.Classes
{
    public abstract class AdminFunctions : Functions
    {
        public static string verifLogin (string email, string password, string type)
        {
            string token = null;
            Task.Run(async () =>
            {
                token = await GetToken(email, password, type);
            }).Wait();
            //Session["token"] = token;
            //MessageBox.Show($"token : {token}");
            return token;
            
        }

        public async static Task<string> GetToken(string email, string password, string type)
        {
            return await CallApi<string>($"api/Account/Authenticate?email={email}&password={password}&type={type}");

        }

        public static List<Driver> GetDrivers()
        {
            List<Driver> listDrivers = new List<Driver>();

            Task.Run(async () =>
            {
                listDrivers = await GetDriversAPI();
            }).Wait();

            return listDrivers;
        }

        public async static Task<List<Driver>> GetDriversAPI()
        {
            return await CallApi<List<Driver>>($"api/Drivers");

        }

        public static List<Rider> GetRiders()
        {
            List<Rider> listRiders = new List<Rider>();

            Task.Run(async () =>
            {
                listRiders = await GetRidersAPI();
            }).Wait();

            return listRiders;
        }

        public static Params UpdateParams(Params parameters)
        {
            Params result = null;
            Task.Run(async () =>
            {
                result = await UpdateParamsAPI(parameters);
            }).Wait();

            return result;
        }
        public static Params GetParams()
        {
            Params result = null;
            Task.Run(async () =>
            {
                result = await GetParamsAPI();
            }).Wait();

            return result;
        }

        public async static Task<Params> GetParamsAPI()
        {
            return await CallApi<Params>($"api/Params");
        }
        public async static Task<Params> UpdateParamsAPI(Params parameters)
        {
            if (parameters.ParamsId == 0)
            {
                // signifie que la table params est vide, il faut la créer
                return await CreateAPIItemAsync<Params>($"api/Params", parameters);
                
            }
            else
            {
                await UpdateAPIItemAsync<Params>($"api/Params/?id={parameters.ParamsId}", parameters);
                return parameters;
            }
        }

        public async static Task<List<Rider>> GetRidersAPI()
        {
            return await CallApi<List<Rider>>($"api/Riders");

        }

        public static bool updateUser<T>(T monItem, string type, int id)
        {
            bool result=false;
            Task.Run(async () =>
            {
                result = await updateUserAPI(monItem, type, id);
            }).Wait();

            return result;
        }

        public async static Task<bool> updateUserAPI<T>(T monItem, string type, int id)
        {
            switch (type)
            {
                case "driver":
                    return await Functions.UpdateAPIItemAsync<T>($"api/Drivers/{id}", monItem );
                    break;
                case "rider":
                    return await Functions.UpdateAPIItemAsync<T>($"api/Riders/{id}", monItem);
                    break;
            }
            return false;
        }
    }
}
