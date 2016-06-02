using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;

/// <summary>
/// Descripción breve de WebEncryptar
/// </summary>
public sealed class CryptoRijndaelSimple
{
    #region "Constructor"
    private static CryptoRijndaelSimple instancia = null;
    public static CryptoRijndaelSimple getInstance()
    {
        if (instancia == null)
        {
            instancia = new CryptoRijndaelSimple();
        }
        return instancia;
    }
    private CryptoRijndaelSimple()
    {
    }
    #endregion

    #region Encriptar

    /// <summary>
    /// Método para encriptar un texto plano usando el algoritmo (Rijndael).
    /// Este es el mas simple posible, muchos de los datos necesarios los
    /// definimos como constantes.
    /// </summary>
    /// <param name="textoQueEncriptaremos">texto a encriptar</param>
    /// <returns>Texto encriptado</returns>
    public string Encriptar(string textoQueEncriptaremos)
    {
        return Encriptar(textoQueEncriptaremos, "pass75dc@avz10", "s@lAvz", "MD5", 1, "@1B2c3D4e5F6g7H8", 128);
    }

    /// <summary>
    /// Método para encriptar un texto plano usando el algoritmo (Rijndael)
    /// </summary>
    /// <returns>Texto encriptado</returns>
    private string Encriptar(string textoQueEncriptaremos, string passBase,
                                   string saltValue, string hashAlgorithm, int passwordIterations,
                                   string initVector, int keySize)
    {

        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(textoQueEncriptaremos);

        PasswordDeriveBytes password = new PasswordDeriveBytes(passBase, saltValueBytes, hashAlgorithm, passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        RijndaelManaged symmetricKey = new RijndaelManaged()
        {
            Mode = CipherMode.CBC
        };

        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();

        string cipherText = Convert.ToBase64String(cipherTextBytes);
        return cipherText;
    }

    #endregion

    #region Desencriptar

    /// <summary>
    /// Método para desencriptar un texto encriptado.
    /// </summary>
    /// <returns>Texto desencriptado</returns>
    public string Desencriptar(string textoEncriptado)
    {
        return Desencriptar(textoEncriptado, "pass75dc@avz10", "s@lAvz", "MD5", 1, "@1B2c3D4e5F6g7H8", 128);
    }

    /// <summary>
    /// Método para desencriptar un texto encriptado (Rijndael)
    /// </summary>
    /// <returns>Texto desencriptado</returns>
    private string Desencriptar(string textoEncriptado, string passBase,
                                      string saltValue, string hashAlgorithm,
                                      int passwordIterations, string initVector,
                                      int keySize)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
        byte[] cipherTextBytes = Convert.FromBase64String(textoEncriptado);

        PasswordDeriveBytes password = new PasswordDeriveBytes(passBase, saltValueBytes, hashAlgorithm, passwordIterations);

        byte[] keyBytes = password.GetBytes(keySize / 8);

        RijndaelManaged symmetricKey = new RijndaelManaged()
        {
            Mode = CipherMode.CBC
        };

        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

        byte[] plainTextBytes = new byte[cipherTextBytes.Length];

        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

        memoryStream.Close();
        cryptoStream.Close();

        string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        return plainText;
    }
    #endregion

}

/// <summary>
/// Clase que encripta una cadena con el algoritmo Rijndael en forma compleja
/// </summary>
public sealed class CryptoRijndaelComplex
{
    #region "Constructor"
    private static CryptoRijndaelComplex instancia = null;
    public static CryptoRijndaelComplex getInstance()
    {
        if (instancia == null)
        {
            instancia = new CryptoRijndaelComplex();
        }
        return instancia;
    }
    private CryptoRijndaelComplex()
    {
    }
    #endregion

    #region "Propiedades"
    private string CryptoKey = "gDn_)1jdM64&¡LdA1%$}";
    private string CryptoKeySaltk = ")rZ1¿:>~$_";
    private readonly string DummySaltk = "Def@ultKey";
    #endregion

    #region "Procedimientos"
    private string ConvertFromHex(string Data)
    {
        string Data1 = "";
        string sData = "";
        while ((Data.Length > 0))
        {
            Data1 = System.Convert.ToChar(System.Convert.ToUInt32(Data.Substring(0, 2), 16)).ToString();
            sData = (sData + Data1);
            Data = Data.Substring(2, (Data.Length - 2));
        }
        return sData;
    }

    private string ConvertToHex(string asciiString)
    {
        string hex = "";
        foreach (char c in asciiString)
        {
            int tmp = c;
            hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
        }
        return hex;
    }

    private Dictionary<KeyValuePair<string, string>, byte[]> cachedKeys = new Dictionary<KeyValuePair<string, string>, byte[]>();

    private byte[] GetCachedCryptoKey(string cryptoKey, string saltk)
    {
        KeyValuePair<string, string> k = new KeyValuePair<string, string>(cryptoKey, saltk);
        byte[] output = null;
        if (!cachedKeys.ContainsKey(k))
        {
            cachedKeys.Add(k, new Rfc2898DeriveBytes(cryptoKey, UTF8Encoding.UTF8.GetBytes(saltk)).GetBytes(32));
        }
        output = cachedKeys[k];
        return output;
    }

    private string EncryptString(string source, string saltk, String invalidString, Boolean invariant)
    {
        string output = "";
        byte[] transformTempResult = null;
        byte[] transformResult = null;
        byte[] sourceAsByte = null;
        byte[] crytoKeyAsBytes = null;

        if (saltk == null) saltk = CryptoKeySaltk;

        if (invariant)
        {
            crytoKeyAsBytes = UTF8Encoding.UTF8.GetBytes(saltk.PadLeft(31, '1'));
        }
        else
        {
            crytoKeyAsBytes = new Rfc2898DeriveBytes(CryptoKey, UTF8Encoding.UTF8.GetBytes(saltk)).GetBytes(32);
        }

        try
        {
            //Get the byte array from the input string 
            sourceAsByte = UTF8Encoding.UTF8.GetBytes(source);

            using (RijndaelManaged rj = new RijndaelManaged())
            {
                //Assign the key 
                rj.Key = crytoKeyAsBytes;

                if (invariant)
                {
                    rj.IV = UTF8Encoding.UTF8.GetBytes(saltk.PadLeft(15, '1'));
                }
                else
                {
                    rj.GenerateIV();
                }
                //Generate a random IV (Initial Vector) 


                //Éncrypt the input value into the temp result 
                transformTempResult = rj.CreateEncryptor().TransformFinalBlock(sourceAsByte, 0, sourceAsByte.Length);

                //transformResult will stored the encrypted result and the IV 
                transformResult = (byte[])Array.CreateInstance(typeof(byte), transformTempResult.Length + rj.IV.Length);

                //storing the encrypted result 
                Array.Copy(transformTempResult, 0, transformResult, 0, transformTempResult.Length);

                //storing the IV 
                Array.Copy(rj.IV, 0, transformResult, transformTempResult.Length, rj.IV.Length);

                //converting to an string in Base64 

                output = Convert.ToBase64String(transformResult);

                if (invariant)
                {
                    output = ConvertToHex(output);
                }
            }
        }
        catch
        {
            output = "";
        }

        //For querystring uses, we need to get the encryted output without the '+' char 
        if (!string.IsNullOrEmpty(invalidString))
        {
            while (!(output.IndexOf(invalidString) == -1))
            {
                output = EncryptString(source, saltk, "+", false);
            }
        }
        return output;
    }

    private string DecryptString(string source, string saltk, Boolean invariant)
    {
        byte[] sourceIV = null;
        byte[] sourceAsByte = null;
        byte[] sourceValue = null;
        string output = "";

        byte[] crytoKeyAsBytes = null;

        if (saltk == null) saltk = CryptoKeySaltk;

        if (invariant)
        {
            source = ConvertFromHex(source);
            crytoKeyAsBytes = UTF8Encoding.UTF8.GetBytes(saltk.PadLeft(31, '1'));
        }
        else
        {
            crytoKeyAsBytes = GetCachedCryptoKey(CryptoKey, saltk);
        }

        try
        {
            //Get the byte array from the input string 
            sourceAsByte = Convert.FromBase64String(source);

            //Create a byte array for storing ONLY the IV 
            sourceIV = (byte[])Array.CreateInstance(typeof(byte), 16);

            //Create a byte array for storing ONLY the encrypted data without the IV 
            sourceValue = (byte[])Array.CreateInstance(typeof(byte), sourceAsByte.Length - sourceIV.Length);

            //Get the encrypted input byte array 
            Array.Copy(sourceAsByte, 0, sourceValue, 0, sourceValue.Length);

            //Get the IV 
            Array.Copy(sourceAsByte, sourceValue.Length, sourceIV, 0, sourceIV.Length);

            using (RijndaelManaged rj = new RijndaelManaged())
            {
                //Assign the Key 
                rj.Key = crytoKeyAsBytes;

                //Assign the IV 
                rj.IV = sourceIV;

                //Decrypt the encrypted input byte array 
                output = UTF8Encoding.UTF8.GetString(rj.CreateDecryptor().TransformFinalBlock(sourceValue, 0, sourceValue.Length));
            }
        }
        catch
        {
            output = "";
        }

        return output;
    }

    public string EncryptString(string source)
    {
        return EncryptString(source, CryptoKeySaltk, "+", false);
    }

    public string DecryptString(string source)
    {
        return DecryptString(source, CryptoKeySaltk, false);
    }
    #endregion
}

/// <summary>
/// Clase que encripta una cadena con el algoritmo MD5
/// </summary>
public sealed class CryptoMPD5
{
    public static byte[] EncriptarDatos(string Data)
    {
        MD5CryptoServiceProvider cryptoProvider = new MD5CryptoServiceProvider();
        UTF8Encoding encoding = new UTF8Encoding();
        byte[] dataHash = cryptoProvider.ComputeHash(encoding.GetBytes(Data));
        cryptoProvider.Clear();
        return dataHash;
    }
}
