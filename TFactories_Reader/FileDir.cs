using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFactories_Reader
{
    class FileDir
    {
        private String directory;
        private int count = 1;
        private String stationClass;
        private String icon;
        private String shortNameList;

        private FileDir next = null;
        private FileDir prev = null;


        /// <summary>
        /// instantiate new FileDir object set object variables by the input
        /// </summary>
        /// <param name="newDirectory"> directory value</param>
        /// <param name="newStationClass"> station class</param>
        /// <param name="newIcon"> Icon to be used</param>
        /// <param name="newShortNameList"> initail value for the short name list</param>
        public FileDir(String newDirectory, String newStationClass, String newIcon, String newShortNameList)
        {
            directory = newDirectory;
            stationClass = newStationClass;
            icon = newIcon;
            shortNameList = newShortNameList;
        }


        public String getDir()
        {
            return directory;
        }
        public void setNext(FileDir newNext)
        {
            next = newNext;
        }
        public FileDir getNext()
        {
            return next;
        }
        public void setPrev(FileDir newPrev)
        {
            prev = newPrev;
        }
        public FileDir getPrev()
        {
            return prev;
        }

        public String getShortName()
        {
            return shortNameList;
        }
        /// <summary>
        /// add short name to the short name list
        /// increment count
        /// </summary>
        /// <param name="newShortName"> short name to add</param>
        public void addShortName(String newShortName)
        {
            shortNameList += ", " + newShortName;
            count++;
        }

        public override String ToString()
        {
            String output = directory;
            output += ";" + count;
            output += ";" + stationClass;
            output += ";" + icon;
            output += ";" + shortNameList;
            return output;
        }


        public int compareTo(FileDir temp)
        {
            return String.Compare(this.getDir(), temp.getDir(),StringComparison.OrdinalIgnoreCase);
        }


    }
}
