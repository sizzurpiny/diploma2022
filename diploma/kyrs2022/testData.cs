﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace kyrs2022
{
    class testData
    {
        public static int Id_test;
        public static string name_test;
        public static string Ques_1;
        public static string Ques_2;
        public static string Ques_3;
        public static string Ques_4;
        public static int Right;

    }

    class WordHelper
    {
        private FileInfo _fileInfo;

        public WordHelper(string newfile)
        {
            if (File.Exists(newfile))
            {
                _fileInfo = new FileInfo(newfile);
            }
            else {
                throw new ArgumentException("File not found");
            }

        }

        internal bool Proccess(Dictionary<string, string> items)
        {
            Word.Application app = null;
            try
            {
                app = new Word.Application();
                Object file = _fileInfo.FullName;
                Object missing = Type.Missing;
                app.Documents.Open(file);

                foreach (var item in items)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false, MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing, Replace: replace);
                }
                Object newFileName = Path.Combine(_fileInfo.DirectoryName, DateTime.Now.ToString("yyyy|MM|dd HH|mm|ss ") + _fileInfo.Name);
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();
                app.Quit();
                
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally {

                if (app != null)
                {
                    app.Quit();
                }
            }
            return false;

        }

    }
}
