using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectInfo
{
    public class Sys_Log_Tracer
    {
        public int Stt { get; set; }
        public string Function { get; set; }
        public string Content { get; set; }
        public string User_Name { get; set; }
        public int Tracer_Type { get; set; }
        public int Tracer_User_Type { get; set; }
        public decimal Ref_Id { get; set; }
        public string Client_Info { get; set; }
        public DateTime Created_Date { get; set; }
    }

    public class UserInfo
    {
        public decimal STT { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public decimal User_Id { get; set; }
        public string Created_By { get; set; }
        public DateTime Login_Time { get; set; }
        public string Description { get; set; }
        public DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public DateTime Modified_Date { get; set; }
        public decimal Deleted { get; set; }
        public string Ip_Address { get; set; }
        public string Client_Info { get; set; }
        public string Token { get; set; }
        public DateTime Last_Update_Pass { get; set; }
    }

    public class Token_Info
    {
        public string Token { get; set; }
    }

    public class TTUserInfo
    {
        public decimal totalRecord { get; set; }
        public List<UserInfo> lstUser{ get; set; }
    }

    public class User_Companies_Info
    {
        public decimal User_Id { get; set; }
        public string Code { get; set; }
        public decimal Type { get; set; }
        public string Created_By { get; set; }
        public DateTime Created_Date { get; set; }
        public string Languages_Code { get; set; }
    }
}
