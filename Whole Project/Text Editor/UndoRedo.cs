using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Editor
{
    class UndoRedo
    {
        private Stack<string> Undo;
        private Stack<string> Redo;

        public UndoRedo()
        {
            Undo = new Stack<string>();
            Redo = new Stack<string>();
        }

        public void Clear()
        {
            Undo.Clear();
            Redo.Clear();
        }

        public void Add(string item)
        {
            Undo.Push(item);
        }

        public string UndoFunc()
        {
            string item = Undo.Pop();
            Redo.Push(item);

            return Undo.First();
        }

        public string RedoFunc()
        {
            if (Redo.Count == 0)
            {
                return Undo.First();
            }

            string item = Redo.Pop();
            Undo.Push(item);

            return Undo.First();
        }

        public bool CanUndo()
        {
            return Undo.Count > 1;
        }

        public bool CanRedo()
        {
            return Redo.Count > 0;
        }

        public List<string> UndoElems()
        {
            return Undo.ToList();
        }

        public List<string> RedoElems()
        {
            return Redo.ToList();
        }
    }
}
