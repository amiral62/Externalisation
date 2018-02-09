using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Externalisation
{
    class Path
    {
        private string m_fileName;
        public string fileName
        {
            get { return m_fileName; }
            set { m_fileName = value; }
        }
        private string m_fullPath;
        public string fullPath
        {
            get { return m_fullPath; }
            set { m_fullPath = value; }
        }
        private string m_path;
        public string path
        {
            get { return m_path; }
            set { m_path = value; }
        }



        public Path() { }
    }
}
