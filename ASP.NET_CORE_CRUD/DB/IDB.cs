using System.Collections;
using System.Data;

namespace ASP.NET_CORE_CRUD.DB
{
    interface IDB
    { 
        // Can use for Insert, Update and Delete
        int Save(string procName, Hashtable hashParam);        
        DataSet GetData(string procName,  Hashtable hashParam, int timeOut);        
    }
}
