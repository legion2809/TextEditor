using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Text_Editor
{
    class EditText
    {
        private UndoRedo data;
        private bool textBoxChangeRequired = true;

        public EditText()
        {
            data = new UndoRedo();
        }

        public bool TextBoxChangeRequired { get => textBoxChangeRequired; set => textBoxChangeRequired = value; }

        public string UndoIsClicked()
        {
            TextBoxChangeRequired = false;
            return data.UndoFunc();
        }

        public string RedoIsClicked()
        {
            TextBoxChangeRequired = false;
            return data.RedoFunc();
        }

        public void AddUndoRedo(string item)
        {
            data.Add(item);
        }

        public bool CanUndo()
        {
            return data.CanIUndo();
        }

        public bool CanRedo()
        {
            return data.CanIRedo();
        }
    }
}
