namespace GuiScratch
{
    public class UndoInfo
    {
        public UndoInfo(Kinds kind, string infoText)
        {
            Kind = kind;

            InfoText = infoText;
        }

        public UndoInfo(Kinds kind, RemoveValueKinds removeValueKind, string infoText)
        {
            Kind = kind;

            InfoText = infoText;

            RemoveValueKind = removeValueKind;
        }

        public enum Kinds { removeBlock, removeValue }
        public Kinds Kind;

        public enum RemoveValueKinds { variabel, list, func}
        public RemoveValueKinds RemoveValueKind;

        public string InfoText;

        public int InfoIndex;
    }
}