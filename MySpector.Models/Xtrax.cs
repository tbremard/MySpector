namespace MySpector.Models
{
    public abstract class Xtrax
    {
        protected abstract IDataTruck GetOutput(IDataTruck data);
        protected Xtrax Next; // pointer to next action to perform
                              // if null the current element is the last element of chain
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

        public IDataTruck GetOutputChained(IDataTruck data)
        {
            IDataTruck ret = GetOutput(data);
            if (Next != null)
            {
                //var nextRump = new DataTruck(ret.GetText());
                ret = Next.GetOutputChained(ret);
            }
            return ret;
        }
    }
}