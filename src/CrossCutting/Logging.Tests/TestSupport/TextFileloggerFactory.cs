using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mame.Doci.CrossCutting.Logging.Loggers;

namespace Logging.Tests.TestSupports
{
    static public class TextFileLoggerFactory
    {
        public static TextFileLogger CreateParameterless ()
        {
            return new TextFileLogger ();
        }
        public static TextFileLogger CreateWithNullReferenceTargetFile ()
        {
            FileInfo nullReferenceTargetFile = null;
            return new TextFileLogger (nullReferenceTargetFile);
        }

        public static TextFileLogger CreateWithExistingWriteableTargetFile ()
        {
            string TargetFileName = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\TextFileLoggerTesting.Log";
            FileInfo WriteableTargetFile = new FileInfo (TargetFileName);
            File.WriteAllText (TargetFileName, "ExampleRow1\r\nExampleRow2\r\n");
            return new TextFileLogger (WriteableTargetFile);
        }

        public static TextFileLogger CreateWithNotExistingWriteableTargetFile ()
        {
            string TargetFileName = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\TextFileLoggerTesting.Log";
            FileInfo WriteableTargetFile = new FileInfo(TargetFileName);
            if (WriteableTargetFile.Exists) WriteableTargetFile.Delete ();
            return new TextFileLogger (WriteableTargetFile);
        }

        public static TextFileLogger CreateWithReadOnlyTargetFile ()
        {
            string TargetFileName = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + "\\TextFileLoggerTesting.Log";
            FileInfo TargetFile = new FileInfo (TargetFileName);
            TargetFile.Attributes = FileAttributes.ReadOnly;
            return new TextFileLogger (TargetFile);
        }

        public static TextFileLogger CreateWithFileInfo (FileInfo TargetFilename)
        {
           return new TextFileLogger (TargetFilename);
        }
    }
}
