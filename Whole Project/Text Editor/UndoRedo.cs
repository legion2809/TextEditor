using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Editor
{
    class UndoRedo
    {
        private Stack<string> UndoSt;
        private Stack<string> RedoSt;

        public UndoRedo()
        {
            UndoSt = new Stack<string>();
            RedoSt = new Stack<string>();
        }

        public void Clear()
        {
            UndoSt.Clear();
            RedoSt.Clear();
        }

        public void Add(string item)
        {
            UndoSt.Push(item);
        }

        public string UndoFunc()
        {
            string item = UndoSt.Pop();
            RedoSt.Push(item);

            return UndoSt.First();
        }

        public string RedoFunc()
        {
            if (RedoSt.Count == 0)
            {
                return UndoSt.First();
            }
            string item = RedoSt.Pop();
            UndoSt.Push(item);

            return UndoSt.First();
        }

        public bool CanIUndo()
        {
            return UndoSt.Count > 1;
        }

        public bool CanIRedo()
        {
            return RedoSt.Count > 0;
        }

        public List<string> UndoElems()
        {
            return UndoSt.ToList();
        }

        public List<string> RedoElems()
        {
            return RedoSt.ToList();
        }
    }
}
