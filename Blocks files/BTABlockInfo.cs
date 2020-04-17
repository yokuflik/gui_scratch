using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GuiScratch
{
    public class BTABlockInfo
    {
        public BTABlockInfo(List<BlockInfo> infos, MouseEventHandler click, int listIndex, ListKinds listKind)
        {
            //save the variabels
            Infos = infos;
            Click = click;
            ListIndex = listIndex;
            ListKind = listKind;
        }

        public List<BlockInfo> Infos;
        public MouseEventHandler Click;
        public int ListIndex;

        public enum ListKinds { vars, lists, funcs }
        public ListKinds ListKind;

        public void deleteTheCurIndexFromTheList(Values myValues, Action<UndoInfo> addUndoInfoFunc)
        {
            string undoText = "";
            switch (ListKind)
            {
                case ListKinds.vars:
                    undoText = myValues.vars[ListIndex].ToString();
                    myValues.vars.RemoveAt(ListIndex);
                    break;
                case ListKinds.lists:
                    undoText = myValues.lists[ListIndex].ToString();
                    myValues.lists.RemoveAt(ListIndex);
                    break;
                case ListKinds.funcs:
                    undoText = myValues.funcs[ListIndex].ToString();
                    myValues.funcs.RemoveAt(ListIndex);
                    break;
            }

            //refresh the blocks view
            myValues.callUpdate();

            //add the undo info
            addUndoInfoFunc(new UndoInfo(UndoInfo.Kinds.removeValue, (UndoInfo.RemoveValueKinds)ListKind, undoText + "\x0001"));
        }
    }
}