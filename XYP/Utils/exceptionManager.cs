using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace XYP.Utils
{
    public class exceptionManager
    {
        public static void ManageException(Exception _ex_, string _tag_)
        {
            StackTrace st = new StackTrace(_ex_, true);
            StackFrame[] frames = st.GetFrames();
            LogWriter._error(_tag_, _ex_.ToString());
            foreach (StackFrame frame in frames)
            {
                string fileName = frame.GetFileName();
                string methodName = frame.GetMethod().Name;
                int line = frame.GetFileLineNumber();
                int col = frame.GetFileColumnNumber();
                string log = string.Format("FILENAME: [{0}], METHOD: [{1}], LINE: [{2}], COLUMN: [{3}], MESSAGE: [{4}]", fileName, methodName, line, col, _ex_.Message);
                string __tag = string.Format("EXCEPTION ON {0}", _tag_);
                LogWriter._error(__tag, log);
            }
        }
    }
}