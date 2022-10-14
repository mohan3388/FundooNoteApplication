using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Model
{
    public class Login
    {
    // public long UserId { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }

        //[NotMapped]
        //public string DecryptedPassword
        //{
        //    get { return Decrypt_Password(Password); }
        //    set { Password = Decrypt_Password(value); }
        //}
        //private string Decrypt_Password(string encryptpassword)
        //{
        //    string pswstr = string.Empty;
        //    System.Text.UTF8Encoding encode_psw = new System.Text.UTF8Encoding();
        //    System.Text.Decoder Decode = encode_psw.GetDecoder();
        //    byte[] todecode_byte = Convert.FromBase64String(encryptpassword);
        //    int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        //    char[] decoded_char = new char[charCount];
        //    Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        //    pswstr = new String(decoded_char);
        //    return pswstr;
        //}
    }
}
