using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;


namespace TFactories_Reader
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            Console.WriteLine("Hello");
            if (result == DialogResult.OK) // Test result.
            {

                string file = openFileDialog1.FileName;

                Console.WriteLine("file: {0}", file);
                try
                {
                    FileDir head = null;
                    //read and parse file into lines and entries for each line
                    int uniqueDir = 0, repeats = 0, total = 0;
                    int k = 0;

                    string[] lines, lineTemp = null;
                    string[,] list = null;
                    lines = File.ReadAllLines(file);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        lineTemp = lines[i].Split(';');

                        if (lineTemp.Length > 3)
                        {
                            FileDir newEntry = new FileDir(lineTemp[11], lineTemp[5], lineTemp[18], lineTemp[28]);

                            if (head == null)
                            {
                                total++;
                                uniqueDir++;
                                head = newEntry;
                                Console.WriteLine("{0} as head", newEntry.getDir() );
                            }
                            else
                            {
                                FileDir temp = head;
                                while (temp != null)
                                {
                                    if ( newEntry.compareTo(temp) == 0 )
                                    {
                                        k++;
                                        temp.addShortName(newEntry.getShortName());
                                        total++;
                                        repeats++;
                                        Console.WriteLine("{0} as match", newEntry.getDir());
                                        break;
                                    }
                                    else
                                    {
                                        k++;
                                        if ( (temp == head) && newEntry.compareTo(temp) < 0 )
                                        {
                                            total++;
                                            uniqueDir++;
                                            //insert newEntry as new head and shift old head down
                                            temp.setPrev(newEntry);
                                            newEntry.setNext(temp);
                                            head = newEntry;
                                            Console.WriteLine("{0} as new head before {1}", newEntry.getDir(), temp.getDir() );
                                            break;
                                        }
                                        else if (temp.getNext() == null)
                                        {
                                            total++;
                                            uniqueDir++;
                                            //add to end
                                            temp.setNext(newEntry);
                                            newEntry.setPrev(temp);
                                            Console.WriteLine("{0} as end after {1}", newEntry.getDir(), temp.getDir());
                                            break;
                                        }
                                        else if ( (newEntry.compareTo(temp) < 0) && (newEntry.compareTo(temp.getNext() ) < 0) )
                                        {
                                            total++;
                                            uniqueDir++;
                                            //add newEntry between 
                                            newEntry.setNext(temp);
                                            newEntry.setPrev(temp.getPrev());
                                            temp.getPrev().setNext(newEntry);
                                            temp.setPrev(newEntry);
                                            Console.WriteLine("{0} between {1} and {2}", newEntry.getDir(), newEntry.getPrev().getDir(), newEntry.getNext().getDir());
                                            break;
                                        }
                                        else
                                        {
                                            temp = temp.getNext();
                                        }
                                    }
                                }
                            }
                        }
                        else if (lineTemp.Length == 3)
                        {
                            int len = Convert.ToInt32(lineTemp[1]);
                            int wid = Convert.ToInt32(lineTemp[0]);
                            list = new String[len, 30];

                        }
                    }

                    //interpret data into objects of scene file and related data

                    FileDir tempPrint = head;

                    int j = 0;
                    while (tempPrint != null)
                    {
                        Console.WriteLine("{0}", tempPrint );
                        tempPrint = tempPrint.getNext();
                        j++;
                    }


                    Console.WriteLine("Done Unique: {0}  Repeats: {1} total Processed: {2}  j: {3}", uniqueDir, repeats, total, j);

                }
                catch (IOException)
                {
                }
            }
            //Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.

        }
    }
}
