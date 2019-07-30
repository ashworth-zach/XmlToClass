using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace DynamicXmlCasting.Utilities
{
     public static class ExpandoHelper
    {
        public static dynamic ParseDictionary(IDictionary<string, object> dict, Type explicitType)
        {

            if (dict == null)
            {
                throw new ArgumentNullException("dict", "Dictionary was null, cannot parse a null dictionary");
            }

            object target;

            if (explicitType.IsArray)
            {
                var length = dict.Keys.Count();
                target = (Array)Activator.CreateInstance(explicitType, new object[] { length });
            }
            else
            {
                target = Activator.CreateInstance(explicitType);
            }

            foreach (var property in target.GetType().GetProperties())
            {
                var propertyName = property.Name;
                object val;

                if (dict.TryGetValue(propertyName, out val) && val != null)
                {
                    var propertyVal = explicitType.GetProperty(propertyName);
                    var expectedType = property.PropertyType;
                    var valType = val.GetType();

                    if (valType == expectedType)
                    {
                        propertyVal.SetValue(target, val);
                    }
                    else if (val is IConvertible)
                    {
                        Type safeType = Nullable.GetUnderlyingType(expectedType) ?? expectedType;

                        if (val == null)
                        {
                            propertyVal.SetValue(target, null, null);
                        }
                        else
                        {
                            propertyVal.SetValue(target, Convert.ChangeType(val, safeType), null);
                        }
                    }
                    else if (val is IDictionary<string, object>)
                    {
                        //Parse non-simple object
                        var propType = propertyVal.PropertyType;
                        object explicitVal = ParseDictionary(val as IDictionary<string, object>, propType);

                        propertyVal.SetValue(target, explicitVal);
                    }
                    else if (val is IList)
                    {
                        //Parse list/enumeration/array

                        if (!(expectedType.IsArray || expectedType.IsGenericType))
                        {
                            //Not sure how we'd get here if we're neither an array nor generic, but we can't really do much
                            continue;
                        }

                        //Create the necessary List implementation that we need
                        var explicitList = ParseAsList(val, expectedType, property);

                        if (expectedType.IsArray)
                        {
                            //Convert from list to array if necessary
                            var arrayType = expectedType.GetElementType().MakeArrayType();
                            var array = (Array)Activator.CreateInstance(arrayType, new object[] { explicitList.Count });
                            explicitList.CopyTo(array, 0);
                            propertyVal.SetValue(target, array);
                        }
                        else
                        {
                            propertyVal.SetValue(target, explicitList);
                        }
                    }
                    else
                    {
                        //Attempt to set it - will error if not compatible and all other checks are bypassed
                        propertyVal.SetValue(target, val);
                    }
                }
            }
            return target;
        }
        private static IList ParseAsList(object val, Type expectedType, PropertyInfo property)
        {
            Type elementType = null;
            if (expectedType.IsArray) //Array type is explicitly included with GetElementType
            {
                elementType = expectedType.GetElementType();
            }
            else if (expectedType.IsGenericType) //Get List type by inspecting generic argument
            {
                elementType = expectedType.GetGenericArguments()[0];
            }

            var listType = typeof(List<>).MakeGenericType(elementType);
            var explicitList = (IList)Activator.CreateInstance(listType);

            foreach (var element in val as IList<object>)
            {
                object explicitElement = ParseDictionary(element as IDictionary<string, object>, elementType);
                explicitList.Add(explicitElement);
            }

            return explicitList;
        }
    }
}