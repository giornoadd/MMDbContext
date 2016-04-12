using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mimo.Dbcontext.DataTransferObjects;
using Mimo.Dbcontext.DataTransferObjects.Mapper;

namespace Mimo.Dbcontext.DataTransferObjects.Mapper
{
    public class DataMapperFactory
    {

        private static IDictionary<String, Object> datatransferobjectDic = new Dictionary<String, Object>();

        public DataMapperFactory()
        {

            //this.RegisterMapper<user>(() => { return new usermapper(); });
        }

        //class user
        //{

        //}

        //class usermapper : IDataTransferObject<user> { }

        public void RegisterMapper<T>(Func<IDataTransferObject<T>> funcMapper) where T : class, new()
        {
            var objType = typeof(T);
            datatransferobjectDic.Add(objType.Name, funcMapper);
        }

        public IDataTransferObject<T> GetMapper<T>() where T : class, new()
        {
            Type type = typeof(T);

            var IsExist = (datatransferobjectDic.Count(dic => dic.Key == type.Name) > 0);

            IDataTransferObject<T> dtoObject;

            if (IsExist)
                return ((Func<IDataTransferObject<T>>)datatransferobjectDic[type.Name]).Invoke();
            else 
                return new GenericMapper<T>(type);
        }


    }
}
