using System;
using System.Collections.Generic;
using System.Linq;

namespace Mimo.Dbcontext.DataTransferObjects.Mapper
{
    public class DataMapperFactory
    {

        private static IDictionary<String, Object> datatransferobjectDic = new Dictionary<String, Object>();

        public DataMapperFactory()
        {

            //this.RegisterMapper<user>(() => { return new usermapper(); });
        }

        public static void RegisterMapper<T>(Func<IDataTransferObject<T>> funcMapper) where T : class, new()
        {
            var objType = typeof(T);

            if (datatransferobjectDic == null)
            {
                datatransferobjectDic = new Dictionary<String, Object>();
            }

            var IsExist = (datatransferobjectDic.Count(dic => dic.Key == objType.FullName) > 0);
            if (IsExist)
            {
                datatransferobjectDic.Remove(objType.FullName);
            }

            datatransferobjectDic.Add(objType.FullName, funcMapper);
        }

        public static IDataTransferObject<T> GetMapper<T>() where T : class, new()
        {
            Type type = typeof(T);
            Func<Type, IDataTransferObject<T>> genericMapper = (typMapper) =>
            {
                return new GenericMapper<T>(typMapper);
            };

            var funcMapper = datatransferobjectDic
                 .DefaultIfEmpty(new KeyValuePair<String, Object>())
                 .FirstOrDefault(d => d.Key == type.FullName) as Func<IDataTransferObject<T>>;
            return funcMapper.Invoke();
        }


    }
}
