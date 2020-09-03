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
        private bool textAreaChangeRequired;

        public EditText()
        {
            data = new UndoRedo();
        }

        public bool TextAreaChangeRequired 
        {
            get
            {
                return textAreaChangeRequired;
            } set
            {
                textAreaChangeRequired = value;
            }
        }

        public string UndoIsClicked()
        {
            TextAreaChangeRequired = false;
            return data.UndoFunc();
        }

        public string RedoIsClicked()
        {
            TextAreaChangeRequired = false;
            return data.RedoFunc();
        }

        public void AddUndoRedo(string item)
        {
            data.Add(item);
        }

        public bool CanUndo()
        {
            return data.CanUndo();
        }

        public bool CanRedo()
        {
            return data.CanRedo();
        }
    }
}
