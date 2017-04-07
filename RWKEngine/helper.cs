using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchützenVerwaltung
{
    public class helper
    {
        static private string _remoteHost = RWKEngine.Properties.Settings.Default.webFtp;
        static private string _remoteUser = RWKEngine.Properties.Settings.Default.webUser;
        static private string _remotePass = RWKEngine.Properties.Settings.Default.webPass;

        public static void FTPUploadFile(string source, string destination)
        {
            string filename = Path.GetFileName(source);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_remoteHost + destination);

            request.Method = WebRequestMethods.Ftp.UploadFile;

            request.Credentials = new NetworkCredential(_remoteUser, _remotePass);

            StreamReader sourceStream = new StreamReader(source);

            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());

            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();

            requestStream.Write(fileContents, 0, fileContents.Length);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            response.Close();

            requestStream.Close();

            sourceStream.Close();

        }

        public static void FTPDownload(string filename, string destination)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_remoteHost + filename);

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential(_remoteUser, _remotePass);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(responseStream);

            StreamWriter writer = new StreamWriter(destination + filename);

            writer.Write(reader.ReadToEnd());

            writer.Close();

            reader.Close();

            response.Close();

        }
        public static List<string> FTPDirectoryListing(string folder)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_remoteHost + folder);

            request.Method = WebRequestMethods.Ftp.ListDirectory;

            request.Credentials = new NetworkCredential(_remoteUser, _remotePass);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(responseStream);

            List<string> result = new List<string>();

            while (!reader.EndOfStream)
            {

                result.Add(reader.ReadLine());

            }

            reader.Close();

            response.Close();

            return result;
        }

        public static void FTPDeleteFileFromServer(string filename)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(_remoteHost + filename);
            request.Method = WebRequestMethods.Ftp.DeleteFile;

            request.Credentials = new NetworkCredential(_remoteUser, _remotePass);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            response.Close();

        }

        public static bool CheckFile(string fileName)
        {
            return File.Exists(fileName) ? true : false;
        }

        public static void DelFile(string fileName)
        {
            File.Delete(fileName);
        }

    }
}
