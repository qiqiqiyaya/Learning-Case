namespace Customize_Router.Data
{
    public class DataContext
    {
        private static List<Api> _apiList = new List<Api>();

        public Api Get(string name)
        {
            return _apiList.Find(x => x.Name == name);
        }

        public void Set(Api api)
        {
            _apiList.Add(api);
        }

        public List<Api> GetList()
        {
            return _apiList;
        }
    }
}
