using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaploNotifier
{
    public partial class Context
    {
        public void LoadData()
        {
            try
            {
                Mayor.LoadNotes();
                Mayor.LoadChanges();
            }
            catch
            {
                RunFirstUpdate();
            }
        }
    }
}
