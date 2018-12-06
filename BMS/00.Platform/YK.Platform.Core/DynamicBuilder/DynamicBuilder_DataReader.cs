using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.Emit;
using YK.Platform.Core.Model;

//http://cnblogs.com/jeffwongishandsome/archive/2010/08/01/1790057.html

namespace YK.Platform.Core
{
    /// <summary>
    ///Use DynamicMethod and ILGenerator create entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DynamicBuilder<T> where T: class, new()
    {
        public static List<T> GetList(IDataReader sdr, List<EntityPropColumnAttributes> attributeList)
        {
            List<T> list = new List<T>();
            if(sdr == null){
                return null;
            }
            while (sdr.Read())
            {
                T t = CreateBuilder(sdr, attributeList).Build(sdr);
                list.Add(t);
            }
            sdr.Close();
            sdr.Dispose();
            return list;
        }

        private static readonly MethodInfo getValueMethod = typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(int) });

        private static readonly MethodInfo isDBNullMethod = typeof(IDataRecord).GetMethod("IsDBNull", new Type[] { typeof(int) });

        //private static readonly MethodInfo getGuidValueMethod = typeof(IDataRecord).GetMethod("GetGuid", new Type[] { typeof(int) });

        private delegate T Load(IDataRecord dataRecord);

        private Load handler;//最终执行动态方法的一个委托 参数是IDataRecord接口

        private DynamicBuilder() { }//私有构造函数

        public T Build(IDataRecord dataRecord)
        {
            return handler(dataRecord);//执行CreateBuilder里创建的DynamicCreate动态方法的委托
        }

        public static DynamicBuilder<T> CreateBuilder(IDataRecord dataRecord, List<EntityPropColumnAttributes> attributeList)
        {
            DynamicBuilder<T> dynamicBuilder = new DynamicBuilder<T>();

            //定义一个名为DynamicCreate的动态方法，返回值typof(T)，参数typeof(IDataRecord)
            DynamicMethod method = new DynamicMethod("DynamicCreate", typeof(T), new Type[] { typeof(IDataRecord) }, typeof(T), true);

            ILGenerator generator = method.GetILGenerator();//创建一个MSIL生成器，为动态方法生成代码

            LocalBuilder result = generator.DeclareLocal(typeof(T));//声明指定类型的局部变量 可以T t;这么理解

            //The next piece of code instantiates the requested type of object and stores it in the local variable. 可以t=new T();这么理解
            generator.Emit(OpCodes.Newobj, typeof(T).GetConstructor(Type.EmptyTypes));
            generator.Emit(OpCodes.Stloc, result);

            for (int i = 0; i < dataRecord.FieldCount; i++)//数据集合，熟悉的for循环 要干什么你懂的 
            {
                var list = attributeList.Where(w => w.fieldName.ToLower() == dataRecord.GetName(i).ToLower());
                if (list.Count() > 0)
                {
                    T t=System.Activator.CreateInstance<T>();//typeof(T)
                    PropertyInfo propertyInfo = t.GetType().GetProperty(list.First().propName);//根据列名取属性  原则上属性和列是一一对应的关系
                    Label endIfLabel = generator.DefineLabel();

                    if (propertyInfo != null && propertyInfo.GetSetMethod() != null)//实体存在该属性 且该属性有SetMethod方法
                    {
                        /*The code then loops through the fields in the data reader, finding matching properties on the type passed in. 
                         * When a match is found, the code checks to see if the value from the data reader is null.
                         */
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, isDBNullMethod);//就知道这里要调用IsDBNull方法 如果IsDBNull==true contine
                        generator.Emit(OpCodes.Brtrue, endIfLabel);

                        /*If the value in the data reader is not null, the code sets the value on the object.*/

                        generator.Emit(OpCodes.Ldloc, result);
                        generator.Emit(OpCodes.Ldarg_0);
                        generator.Emit(OpCodes.Ldc_I4, i);
                        generator.Emit(OpCodes.Callvirt, getValueMethod);//调用get_Item方法
                        generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
                        generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());//给该属性设置对应值

                        generator.MarkLabel(endIfLabel);

                        //if (propertyInfo.PropertyType.FullName != "System.Guid")
                        //{
                        //}
                        //else
                        //{
                        //    generator.Emit(OpCodes.Ldloc, result);
                        //    generator.Emit(OpCodes.Ldarg_0);
                        //    generator.Emit(OpCodes.Ldc_I4, i);
                        //    generator.Emit(OpCodes.Callvirt, getGuidValueMethod);//调用get_Item方法
                        //    generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);
                        //    generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());//给该属性设置对应值
                        //    generator.MarkLabel(endIfLabel);
                        //    //propertyInfo.SetValue(t, Convert.ChangeType(propertyInfo.GetSetMethod(),propertyInfo.PropertyType),null);
                        //}
                    }
                }
            }

            /*The last part of the code returns the value of the local variable*/
            generator.Emit(OpCodes.Ldloc, result);
            generator.Emit(OpCodes.Ret);//方法结束，返回

            //完成动态方法的创建，并且创建执行该动态方法的委托，赋值到全局变量handler,handler在Build方法里Invoke
            dynamicBuilder.handler = (Load)method.CreateDelegate(typeof(Load));
            return dynamicBuilder;
        }
    }

}
