using NFeDownload.NFe.SchemaXML200;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NFeDownload.NFe
{
    public class Util
    {
        private static XmlSerializer serializer;

        #region Serialize/Deserialize

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(TNfeProc));
                }
                return serializer;
            }
        }

        /// <summary>
        /// Serializes current TNfeProc object into an XML document
        /// </summary>
        /// <returns>string XML value</returns>
        public static string Serialize(TNfeProc nfe, System.Text.Encoding encoding)
        {
            System.IO.StreamReader streamReader = null;
            System.IO.MemoryStream memoryStream = null;
            try
            {
                memoryStream = new System.IO.MemoryStream();
                System.Xml.XmlWriterSettings xmlWriterSettings = new System.Xml.XmlWriterSettings();
                xmlWriterSettings.Encoding = encoding;
                System.Xml.XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
                Serializer.Serialize(xmlWriter, nfe);
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                streamReader = new System.IO.StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if ((streamReader != null))
                {
                    streamReader.Dispose();
                }
                if ((memoryStream != null))
                {
                    memoryStream.Dispose();
                }
            }
        }

        public static string Serialize(TNfeProc nfe)
        {
            return Serialize(nfe, Encoding.UTF8);
        }

        /// <summary>
        /// Deserializes workflow markup into an TNfeProc object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output TNfeProc object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out TNfeProc obj, out System.Exception exception)
        {
            exception = null;
            obj = default(TNfeProc);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out TNfeProc obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static TNfeProc Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((TNfeProc)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }

        /// <summary>
        /// Serializes current TNfeProc object into file
        /// </summary>
        /// <param name="fileName">full path of outupt xml file</param>
        /// <param name="exception">output Exception value if failed</param>
        /// <returns>true if can serialize and save into file; otherwise, false</returns>
        public virtual bool SaveToFile(string fileName, TNfeProc nfe, System.Text.Encoding encoding, out System.Exception exception)
        {
            exception = null;
            try
            {
                SaveToFile(fileName, nfe, encoding);
                return true;
            }
            catch (System.Exception e)
            {
                exception = e;
                return false;
            }
        }

        public virtual bool SaveToFile(string fileName, TNfeProc nfe, out System.Exception exception)
        {
            return SaveToFile(fileName, nfe, Encoding.UTF8, out exception);
        }

        public virtual void SaveToFile(string fileName, TNfeProc nfe)
        {
            SaveToFile(fileName, nfe, Encoding.UTF8);
        }

        public virtual void SaveToFile(string fileName, TNfeProc nfe, System.Text.Encoding encoding)
        {
            System.IO.StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize(nfe, encoding);
                streamWriter = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if ((streamWriter != null))
                {
                    streamWriter.Dispose();
                }
            }
        }

        /// <summary>
        /// Deserializes xml markup from file into an TNfeProc object
        /// </summary>
        /// <param name="fileName">string xml file to load and deserialize</param>
        /// <param name="obj">Output TNfeProc object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool LoadFromFile(string fileName, System.Text.Encoding encoding, out TNfeProc obj, out System.Exception exception)
        {
            exception = null;
            obj = default(TNfeProc);
            try
            {
                obj = LoadFromFile(fileName, encoding);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, out TNfeProc obj, out System.Exception exception)
        {
            return LoadFromFile(fileName, Encoding.UTF8, out obj, out exception);
        }

        public static bool LoadFromFile(string fileName, out TNfeProc obj)
        {
            System.Exception exception = null;
            return LoadFromFile(fileName, out obj, out exception);
        }

        public static TNfeProc LoadFromFile(string fileName)
        {
            TNfeProc nfeProc;
            nfeProc = LoadFromFile(fileName, Encoding.UTF8);            
            return nfeProc;
        }

        public static TNfeProc LoadFromFile(string fileName, System.Text.Encoding encoding)
        {
            System.IO.FileStream file = null;
            System.IO.StreamReader sr = null;
            try
            {
                file = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new System.IO.StreamReader(file, encoding);
                string xmlString = sr.ReadToEnd();
                sr.Close();
                file.Close();
                return Deserialize(xmlString);
            }
            finally
            {
                if ((file != null))
                {
                    file.Dispose();
                }
                if ((sr != null))
                {
                    sr.Dispose();
                }
            }
        }
        #endregion

        #region Converting a string to byte-array

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        #endregion
    }
}
