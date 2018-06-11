using System;
using System.IO;
using System.Text;
using System.IO.Compression;
using System.Diagnostics;

namespace AngryWasp.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// takes a virtual file path and returns a virtual file path with the uniquely named path
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <returns></returns>
        public static string RenameDuplicateFile(string p)
        {
            //todo: changed to accept full file path. check p to make sure it is absolute
            Debugger.Break();
            if (!File.Exists(p))
                return p;

            string fileName = Path.GetFileNameWithoutExtension(p);
            string dirName = Path.GetDirectoryName(p);
            string ext = Path.GetExtension(p);

            string format = Path.Combine(dirName, fileName) + "_{0}" + ext;

            for (int n = 0; n < 1000; n++)
            {
                string v = n.ToString();
                if (n < 10)
                    v = "0" + v;
                if (n < 100)
                    v = "0" + v;

                string fn = string.Format(format, v);

                if (!File.Exists(fn))
                    return Path.Combine(dirName, Path.GetFileName(fn));
            }

            return p;
        }

        public static string RenameDuplicateFolder(string p)
        {
            //todo: changed to accept full file path. check p to make sure it is absolute
            Debugger.Break();
            if (!Directory.Exists(p))
                return p;

            string format = p + "_{0}";

            for (int n = 0; n < 1000; n++)
            {
                string v = n.ToString();
                if (n < 10)
                    v = "0" + v;
                if (n < 100)
                    v = "0" + v;

                string fn = string.Format(format, v);

                if (!Directory.Exists(p))
                    return fn;
            }

            return p;
        }

        /// <summary>
        /// Converts a path to a relative path
        /// </summary>
        /// <param name="path1">The path to make relative</param>
        /// <param name="path2">The path to make Path1 relative to</param>
        /// <returns>the relative path equivalent of Path1</returns>
        public static string MakeRelative(string path1, string path2)
        {
            if (path1 == path2)
                return string.Empty;
            string[] path1Parts = SplitPath(path1);
            string[] path2Parts = SplitPath(path2);

            int counter = 0;
            while ((counter < path2Parts.Length) && (counter < path1Parts.Length) && path2Parts[counter].Equals(path1Parts[counter], StringComparison.InvariantCultureIgnoreCase))
                counter++;

            if (counter == 0)
                return path1; // There is no relative link.

            StringBuilder sb = new StringBuilder();
            for (int i = counter; i < path2Parts.Length; i++)
                sb.Append(".." + Path.DirectorySeparatorChar);

            for (int i = counter; i < path1Parts.Length; i++)
                sb.Append(path1Parts[i] + Path.DirectorySeparatorChar);

            //remove end seperator char
            sb.Length--;

            return sb.ToString();
        }
            
        public static string MoveUpDirectories(string path, int levels)
        {
            string[] pathParts = SplitPath(path);

            //return 
            if (pathParts.Length < levels)
                return null;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < pathParts.Length - levels; i++)
                sb.Append(pathParts[i] + Path.DirectorySeparatorChar);

            //remove end seperator char
            sb.Length--;

            return sb.ToString();
        }

        private static string[] SplitPath(string path)
        {
            //convert to uri and get local name in case file path is in uri format (i.e. File:// prefix)
            //user Path.GetFullPath to convert to a full file path
            //trim seperator char
            //finally split
            return Path.GetFullPath(new Uri(path).LocalPath).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
        }

        public static byte[] Compress(byte[] b)
        {
            MemoryStream ms = new MemoryStream();
            GZipStream sw = new GZipStream(ms, CompressionMode.Compress);

            //get length of array to compress
            //bit shift it into bytes
            //and write it to the first 8 bytes (long) of the compressed stream

            sw.Write(b, 0, b.Length);
            sw.Close();
            b = ms.ToArray();
            ms.Close();
            sw.Dispose();
            ms.Dispose();
            return b;
        }

        public static void Compress(string FileToCompress, string CompressedFile)
        {
            byte[] buffer = new byte[1024 * 1024]; // 1MB

            using (System.IO.FileStream sourceFile = System.IO.File.OpenRead(FileToCompress))
            {

                using (System.IO.FileStream destinationFile = System.IO.File.Create(CompressedFile))
                {

                    using (System.IO.Compression.GZipStream output = new System.IO.Compression.GZipStream(destinationFile,
                        System.IO.Compression.CompressionMode.Compress))
                    {
                        int bytesRead = 0;
                        while (bytesRead < sourceFile.Length)
                        {
                            int ReadLength = sourceFile.Read(buffer, 0, buffer.Length);
                            output.Write(buffer, 0, ReadLength);
                            output.Flush();
                            bytesRead += ReadLength;
                        } // Whend

                        destinationFile.Flush();
                    } // End Using System.IO.Compression.GZipStream output

                    destinationFile.Close();
                } // End Using System.IO.FileStream destinationFile 

                // Close the files.
                sourceFile.Close();
            } // End Using System.IO.FileStream sourceFile

        } // End Sub CompressFile

        /// <summary>
        /// Decompresses a byte[]
        /// </summary>
        /// <param name="b">the byte[] to decompress</param>
        /// <param name="i">the original (uncompressed) length of the array</param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] b, int i)
        {
            //Prepare for decompress
            MemoryStream ms = new MemoryStream(b);
            GZipStream sr = new GZipStream(ms, CompressionMode.Decompress);

            //Reset variable to collect uncompressed result
            b = new byte[i];

            //Decompress
            sr.Read(b, 0, i);
            sr.Close();
            ms.Close();
            sr.Dispose();
            ms.Dispose();
            return b;
        }

        public static byte[] Decompress(FileStream fs, int i)
        {
            //Prepare for decompress
            GZipStream sr = new GZipStream(fs, CompressionMode.Decompress);

            //Reset variable to collect uncompressed result
            byte[] b = new byte[i];

            //Decompress
            sr.Read(b, 0, i);
            sr.Close();
            fs.Close();

            sr.Dispose();
            fs.Dispose();
            return b;
        }

        public static byte[] Decompress(ref GZipStream sr, int i)
        {
            //Reset variable to collect uncompressed result
            byte[] b = new byte[i];
            //Decompress
            sr.Read(b, 0, i);
            return b;
        }
    }
}
