using System.Text;

namespace MySpector.Objects
{
    public abstract class Xtrax
    {
        public int? DbId { get; set; }
        protected StringBuilder ErrorMessage;
        public abstract XtraxType Type { get; }
        public abstract string JsonArg { get; }
        protected abstract IDataTruck GetOutput(IDataTruck data);
        protected Xtrax Next; // pointer to next action to perform
                              // if null the current element is the last element of chain

        public Xtrax()
        {
            ErrorMessage = new StringBuilder();

        }

        public Xtrax GetNext()
        {
            return Next;
        }

        /// <summary>
        /// set new action at end of chain
        /// </summary>
        public void SetNext(Xtrax next)
        {
            if (Next == null)
                Next = next;
            else
            {
                Next.SetNext(next); // put at the end of chain
            }
        }

        public StringBuilder GetErrorChained()
        {
            var ret = ErrorMessage;
            if(Next != null)
            {
                ret.Append(Next.GetErrorChained());
            }
            return ret;
        }

        public IDataTruck GetOutputChained(IDataTruck data)
        {
            IDataTruck ret = GetOutput(data);
            if (Next != null)
            {
                ret = Next.GetOutputChained(ret);
            }
            return ret;
        }
    }
}