using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class CBO<T>
    {
        #region read from data set

        public static List<T> FillCollectionFromDataSet(DataSet ds)
        {
            List<T> _list_T = new List<T>();

            // get properties for type
            Hashtable objProperties = GetPropertyInfo(typeof(T));

            // get ordinal positions in datareader
            Hashtable arrOrdinals = GetOrdinalsFromDataSet(objProperties, ds);

            // iterate datareader
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // fill business object
                    T objFillObject = (T)CreateObjectFromDataSet(typeof(T), dr, objProperties, arrOrdinals);

                    // add to collection
                    _list_T.Add(objFillObject);
                }
            }
            return _list_T;

        }

        public static ObservableCollection<T> FillCollectionFromDataSet_ob(DataSet ds)
        {
            ObservableCollection<T> _ob_T = new ObservableCollection<T>();

            // get properties for type
            Hashtable objProperties = GetPropertyInfo(typeof(T));

            // get ordinal positions in datareader
            Hashtable arrOrdinals = GetOrdinalsFromDataSet(objProperties, ds);

            // iterate datareader
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // fill business object
                    T objFillObject = (T)CreateObjectFromDataSet(typeof(T), dr, objProperties, arrOrdinals);

                    // add to collection
                    _ob_T.Add(objFillObject);
                }
            }
            return _ob_T;
        }

        public static T FillObjectFromDataSet(DataSet ds)
        {

            try
            {
                // get properties for type
                Hashtable objProperties = GetPropertyInfo(typeof(T));

                // get ordinal positions in datareader
                Hashtable arrOrdinals = GetOrdinalsFromDataSet(objProperties, ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    // read datareader
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        // fill business object
                        T _objResult = (T)CreateObjectFromDataSet(typeof(T), ds.Tables[0].Rows[0], objProperties, arrOrdinals);
                        return _objResult;
                    }
                }

                return default(T);
            }
            catch
            {
                return default(T);
            }
        }
        
        #endregion


        #region Read from data table
        // return list
        public static List<T> FillCollectionFromDataTable(DataTable dt)
        {
            List<T> _list_T = new List<T>();

            // get properties for type
            Hashtable objProperties = GetPropertyInfo(typeof(T));

            // get ordinal positions in datareader
            Hashtable arrOrdinals = GetOrdinalsFromDataTable(objProperties, dt);

            // iterate datareader
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    // fill business object
                    T objFillObject = (T)CreateObjectFromDataSet(typeof(T), dr, objProperties, arrOrdinals);

                    // add to collection
                    _list_T.Add(objFillObject);
                }
            }
            return _list_T;

        }

        //return ObservableCollection
        public static ObservableCollection<T> FillCollectionFromDataTable_ob(DataTable dt)
        {
            ObservableCollection<T> _list_T = new ObservableCollection<T>();

            // get properties for type
            Hashtable objProperties = GetPropertyInfo(typeof(T));

            // get ordinal positions in datareader
            Hashtable arrOrdinals = GetOrdinalsFromDataTable(objProperties, dt);

            // iterate datareader
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    // fill business object
                    T objFillObject = (T)CreateObjectFromDataSet(typeof(T), dr, objProperties, arrOrdinals);

                    // add to collection
                    _list_T.Add(objFillObject);
                }
            }
            return _list_T;
        }
        #endregion


        #region private

        private static object CreateObjectFromDataSet(Type objType, DataRow dr, Hashtable objProperties, Hashtable arrOrdinals)
        {

            try
            {
                object objObject = Activator.CreateInstance(objType);

                string _fieldname = "";
                int _possition = -1;
                foreach (DictionaryEntry de in arrOrdinals)
                {
                    _fieldname = de.Key.ToString();
                    _possition = (int)arrOrdinals[_fieldname];
                    PropertyInfo _PropertyInfo = (PropertyInfo)objProperties[_fieldname];

                    if (_PropertyInfo.CanWrite)
                    {
                        if (_possition != -1 && dr[_possition] != System.DBNull.Value)
                        {
                            #region set value for object
                            switch (_PropertyInfo.PropertyType.FullName)
                            {
                                case "System.Enum":
                                    _PropertyInfo.SetValue(objObject, System.Enum.ToObject(_PropertyInfo.PropertyType, dr[_possition]), null);
                                    break;
                                case "System.String":
                                    _PropertyInfo.SetValue(objObject, (string)dr[_possition], null);
                                    break;
                                case "System.Boolean":
                                    _PropertyInfo.SetValue(objObject, (Boolean)dr[_possition], null);
                                    break;
                                case "System.Decimal":
                                    _PropertyInfo.SetValue(objObject, Convert.ToDecimal(dr[_possition]), null);
                                    break;
                                case "System.Int16":
                                    _PropertyInfo.SetValue(objObject, Convert.ToInt16(dr[_possition]), null);
                                    break;
                                case "System.Int32":
                                    _PropertyInfo.SetValue(objObject, Convert.ToInt32(dr[_possition]), null);
                                    break;
                                case "System.Int64":
                                    _PropertyInfo.SetValue(objObject, Convert.ToInt64(dr[_possition]), null);
                                    break;
                                case "System.DateTime":
                                    _PropertyInfo.SetValue(objObject, Convert.ToDateTime(dr[_possition]), null);
                                    break;
                                case "System.Double":
                                    _PropertyInfo.SetValue(objObject, Convert.ToDouble(dr[_possition]), null);
                                    break;
                                default:
                                    // try explicit conversion
                                    _PropertyInfo.SetValue(objObject, Convert.ChangeType(dr[_possition], _PropertyInfo.PropertyType), null);
                                    break;
                            }
                            #endregion
                        }
                    }
                }

                return objObject;
            }
            catch(Exception ex)
            {
                return Activator.CreateInstance(objType);
            }
        }

        private static Hashtable GetPropertyInfo(Type objType)
        {
            Hashtable hashProperties = new Hashtable();
            foreach (PropertyInfo objProperty in objType.GetProperties())
            {
                hashProperties[objProperty.Name] = objProperty;
            }
            return hashProperties;
        }

        private static Hashtable GetOrdinalsFromDataTable(Hashtable hashProperties, DataTable dt)
        {

            Hashtable arrOrdinals = new Hashtable();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (hashProperties.ContainsKey(dt.Columns[i].ColumnName))
                    arrOrdinals[dt.Columns[i].ColumnName] = i;
            }

            return arrOrdinals;
        }

        private static Hashtable GetOrdinalsFromDataSet(Hashtable hashProperties, DataSet dt)
        {

            Hashtable arrOrdinals = new Hashtable();

            if (dt != null)
            {

                for (int i = 0; i < dt.Tables[0].Columns.Count; i++)
                {
                    if (hashProperties.ContainsKey(dt.Tables[0].Columns[i].ColumnName))
                        arrOrdinals[dt.Tables[0].Columns[i].ColumnName] = i;
                }
            }
            return arrOrdinals;
        }

        #endregion
    }
}
