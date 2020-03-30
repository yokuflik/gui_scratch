using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiScratch
{
    public class MyFunc
    {
        public MyFunc(BlockInfo info, string name)
        {
            //save the vars
            Info = info.Clone(false);

            Name = name;

            //set the info kind
            Info.Kind = Block.BlockKinds.Action;

            //set the block code
            List<CodePart> parts = new List<CodePart>();
            parts.Add(new CodePart(name + "("));

            //add all the values
            for (int i = 0; i <= Info.BlockInfoEvents.Count - 1; i++)
            {
                if (Info.BlockInfoEvents[i].Kind != BlockInfoEvent.Kinds.text)
                {
                    parts.Add(new CodePart(i));

                    if (i!= Info.BlockInfoEvents.Count - 1)
                    {
                        parts.Add(new CodePart(", "));
                    }
                }
            }

            parts.Add(new CodePart(")"));

            Info.BlockCode = new BlockCode(parts);
        }

        public BlockInfo Info;

        public string Name;
    }
}
