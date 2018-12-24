using AOISystem.Utilities.Component;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace AOISystem.Utilities.Common
{
    public class PropertyHelper
    {
        /// <summary>
        /// 屬性值複製, 將來源實體數值複製到目的實體, 主要作為繼承對象數值複製
        /// </summary>
        /// <param name="srcInstance">來源實體</param>
        /// <param name="destInstance">目的實體</param>
        public static void BlindingInstanceProperty(object srcInstance, object destInstance)
        {
            List<PropertyInfo> srcPropertyInfos = srcInstance.GetType().GetProperties().ToList();
            List<PropertyInfo> destPropertyInfos = destInstance.GetType().GetProperties().ToList();
            List<PropertyInfo> propertyInfos = new List<PropertyInfo>();
            for (int i = 0; i < srcPropertyInfos.Count; i++)
            {
                if (destPropertyInfos.Exists(p => p.PropertyType == srcPropertyInfos[i].PropertyType))
                {
                    propertyInfos.Add(srcPropertyInfos[i]);
                }
            }
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                XmlIgnoreAttribute xmlIgnoreAttribute =
                        (XmlIgnoreAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(XmlIgnoreAttribute));
                NonMemberAttribute nonMemberAttribute =
                        (NonMemberAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(NonMemberAttribute));
                if (xmlIgnoreAttribute != null || nonMemberAttribute != null)
                {
                    continue;
                }
                object value = propertyInfo.GetValue(srcInstance, null);
                propertyInfo.SetValue(destInstance, value, null);
                
            }
        }

        public static void AvoidPropertyInstanceIsNull(object instance)
        {
            List<PropertyInfo> propertyInfos = instance.GetType().GetProperties().ToList();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                XmlIgnoreAttribute xmlIgnoreAttribute =
                        (XmlIgnoreAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(XmlIgnoreAttribute));
                NonMemberAttribute nonMemberAttribute =
                        (NonMemberAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(NonMemberAttribute));
                if (xmlIgnoreAttribute != null || nonMemberAttribute != null)
                {
                    continue;
                }
                object value = propertyInfo.GetValue(instance, null);
                if (value == null)
                {
                    Type type = propertyInfo.PropertyType;
                    if (type == typeof(string))
                    {
                        value = string.Empty;
                    }
                    else
                    {
                        value = Activator.CreateInstance(type);
                    }
                    propertyInfo.SetValue(instance, value, null);
                }
            }
        }

        public static bool IsSupportPropertyInfo(PropertyInfo property)
        {
            bool isSupport = false;
            Type type = property.PropertyType;
            string typeName = property.PropertyType.FullName;
            switch (typeName)
            {
                case "System.String":
                case "System.Int16":
                case "System.UInt16":
                case "System.Int32":
                case "System.UInt32":
                case "System.Int64":
                case "System.UInt64":
                case "System.Single":
                case "System.Double":
                case "System.Boolean":
                case "System.DateTime":
                case "HalconDotNet.HTuple":
                    isSupport = true;
                    break;
                default:
                    if (type.IsEnum)
                    {
                        isSupport = true;
                        break;
                    }
                    else
                    {
                        isSupport = false;
                        break;
                    }
            }
            return isSupport;
        }

        public static void PropertyInfoSetValue(object instance, PropertyInfo property, object value)
        {
            string readValue = value.ToString();
            Type type = property.PropertyType;
            string typeName = property.PropertyType.FullName;
            switch (typeName)
            {
                case "System.String":
                    property.SetValue(instance, readValue, null);
                    break;
                case "System.Int16":
                    property.SetValue(instance, Int16.Parse(readValue), null);
                    break;
                case "System.UInt16":
                    property.SetValue(instance, UInt16.Parse(readValue), null);
                    break;
                case "System.Int32":
                    property.SetValue(instance, Int32.Parse(readValue), null);
                    break;
                case "System.UInt32":
                    property.SetValue(instance, UInt32.Parse(readValue), null);
                    break;
                case "System.Int64":
                    property.SetValue(instance, Int64.Parse(readValue), null);
                    break;
                case "System.UInt64":
                    property.SetValue(instance, UInt64.Parse(readValue), null);
                    break;
                case "System.Single":
                    property.SetValue(instance, Single.Parse(readValue), null);
                    break;
                case "System.Double":
                    property.SetValue(instance, Double.Parse(readValue), null);
                    break;
                case "System.Boolean":
                    property.SetValue(instance, Boolean.Parse(readValue), null);
                    break;
                case "System.DateTime":
                    property.SetValue(instance, DateTime.ParseExact(readValue, "yyyy/MM/dd HH:mm:ss", new CultureInfo("en-US"), DateTimeStyles.None), null);
                    break;
                case "HalconDotNet.HTuple":
                    Type typeHTupleTypeConverter = Assembly.Load("AOISystem.Halcon").GetType("AOISystem.Halcon.HPropertyType.HTupleTypeConverter");
                    MethodInfo methodHTupleStringTypeConverter = typeHTupleTypeConverter.GetMethod("HTupleStringTypeConverter", BindingFlags.Public | BindingFlags.Static);
                    object resultValue = methodHTupleStringTypeConverter.Invoke(instance, new object[] { readValue });
                    property.SetValue(instance, resultValue, null);
                    break;
                default:
                    if (type.IsEnum)
                    {
                        property.SetValue(instance, Enum.Parse(type, readValue), null);
                        break;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(string.Format("PropertyInfoSetValue don't define {0} convet rule.", typeName));
                    }
            }
        }

        public static string PropertyInfoGetValue(object instance, PropertyInfo property)
        {
            string readValue = string.Empty;
            Type type = property.PropertyType;
            string typeName = property.PropertyType.FullName;
            switch (typeName)
            {
                case "System.String":
                case "System.Int16":
                case "System.UInt16":
                case "System.Int32":
                case "System.UInt32":
                case "System.Int64":
                case "System.UInt64":
                case "System.Single":
                case "System.Double":
                case "System.Boolean":
                case "HalconDotNet.HTuple":
                    readValue = property.GetValue(instance, null).ToString().Replace("\"", "");
                    break;
                case "HalconDotNet.HObject":
                    readValue = null;
                    break;
                case "System.DateTime":
                    readValue = ((DateTime)property.GetValue(instance, null)).ToString("yyyy/MM/dd HH:mm:ss");
                    break;
                default:
                    if (type.IsEnum)
                    {
                        readValue = property.GetValue(instance, null).ToString();
                        break;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(string.Format("PropertyInfoGetValue don't define {0} convet rule.", typeName));
                    }
            }
            return readValue;
        }

        public static void SetValue(object inputObject, string propertyName, object propertyVal)
        {
            //find out the type
            Type type = inputObject.GetType();

            //get the property information based on the type
            System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);

            //find the property type
            Type propertyType = propertyInfo.PropertyType;

            //Convert.ChangeType does not handle conversion to nullable types
            //if the property type is nullable, we need to get the underlying type of the property
            var targetType = IsNullableType(propertyInfo.PropertyType) ? Nullable.GetUnderlyingType(propertyInfo.PropertyType) : propertyInfo.PropertyType;

            //Returns an System.Object with the specified System.Type and whose value is
            //equivalent to the specified object.
            propertyVal = Convert.ChangeType(propertyVal, targetType);

            //Set the value of the property
            propertyInfo.SetValue(inputObject, propertyVal, null);

        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }
    }
}
