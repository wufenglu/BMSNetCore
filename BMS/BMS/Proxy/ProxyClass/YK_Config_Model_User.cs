using System;

namespace Proxy.GenerateClass.YK_Config_Model
{
    /// <summary>
    /// 生成代码类
    /// </summary>
    public class User:YK.Config.Model.User
    {
		public override Int32 ID 
{
get { if (ChanageProperty.ContainsKey("ID") == false) { return default(Int32); } else { return (Int32)ChanageProperty["ID"]; } } 
set { ChanageProperty.Add("ID",value); } 
}
public override String UserName 
{
get { if (ChanageProperty.ContainsKey("UserName") == false) { return default(String); } else { return (String)ChanageProperty["UserName"]; } } 
set { ChanageProperty.Add("UserName",value); } 
}
public override String UserCode 
{
get { if (ChanageProperty.ContainsKey("UserCode") == false) { return default(String); } else { return (String)ChanageProperty["UserCode"]; } } 
set { ChanageProperty.Add("UserCode",value); } 
}
public override String Password 
{
get { if (ChanageProperty.ContainsKey("Password") == false) { return default(String); } else { return (String)ChanageProperty["Password"]; } } 
set { ChanageProperty.Add("Password",value); } 
}
public override Nullable<Boolean> IsEnable 
{
get { if (ChanageProperty.ContainsKey("IsEnable") == false) { return default(Nullable<Boolean>); } else { return (Nullable<Boolean>)ChanageProperty["IsEnable"]; } } 
set { ChanageProperty.Add("IsEnable",value); } 
}
public override Nullable<Int32> CreaterID 
{
get { if (ChanageProperty.ContainsKey("CreaterID") == false) { return default(Nullable<Int32>); } else { return (Nullable<Int32>)ChanageProperty["CreaterID"]; } } 
set { ChanageProperty.Add("CreaterID",value); } 
}
public override String Creater 
{
get { if (ChanageProperty.ContainsKey("Creater") == false) { return default(String); } else { return (String)ChanageProperty["Creater"]; } } 
set { ChanageProperty.Add("Creater",value); } 
}
public override Nullable<DateTime> CreatedOn 
{
get { if (ChanageProperty.ContainsKey("CreatedOn") == false) { return default(Nullable<DateTime>); } else { return (Nullable<DateTime>)ChanageProperty["CreatedOn"]; } } 
set { ChanageProperty.Add("CreatedOn",value); } 
}
public override Nullable<Int32> ModifierID 
{
get { if (ChanageProperty.ContainsKey("ModifierID") == false) { return default(Nullable<Int32>); } else { return (Nullable<Int32>)ChanageProperty["ModifierID"]; } } 
set { ChanageProperty.Add("ModifierID",value); } 
}
public override String Modifier 
{
get { if (ChanageProperty.ContainsKey("Modifier") == false) { return default(String); } else { return (String)ChanageProperty["Modifier"]; } } 
set { ChanageProperty.Add("Modifier",value); } 
}
public override Nullable<DateTime> ModifyOn 
{
get { if (ChanageProperty.ContainsKey("ModifyOn") == false) { return default(Nullable<DateTime>); } else { return (Nullable<DateTime>)ChanageProperty["ModifyOn"]; } } 
set { ChanageProperty.Add("ModifyOn",value); } 
}

    }
}
