using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiScratch
{
    class FileSaveAndOpen
    {
        /*
         * Now saving the block index
         * My values
         * And the blocks
        */

        #region save
        public static string saveFile(decimal blockIndex, List<Block> blocks, Values myValues)
        {
            string file = "";

            //add the block index
            file += blockIndex.ToString();

            //add the values
            file += "\n" + myValues.ToString();

            //add the blocks length
            file += "\x0001\n"+blocks.Count;

            //add all the blocks to the file
            file += getBlocksText(blocks);

            return file;
        }

        private static string getBlocksText(List<Block> blocks)
        {
            string res = "";
            //move on all the blocks and make them to a string
            for (int i = 0; i <= blocks.Count - 1; i++)
            {
                res += "\n" + blocks[i].ToString();
            }

            return res;
        }

        #endregion

        #region open

        public static Values openFile(string filePath, ref decimal blockIndex, ref List<BlockFromString> blocks, Values myValues)
        {
            StreamReader sr = new StreamReader(filePath);
            string text = sr.ReadToEnd();
            sr.Close();

            //read the text
            string[] vars = text.Split(new char[] { '\x0001' });

            blockIndex = decimal.Parse(vars[0]);

            int index = 1;

            //set the values
            myValues = new Values(vars, ref index);

            //set the blocks
            int blocksCount = int.Parse(vars[index++]);

            for (int j = 0; j <= blocksCount - 1; j++)
            {
                blocks.Add(new BlockFromString(myValues, vars, ref index));
            }

            return myValues;
        }
        
        #endregion
    }
}
