namespace MySpector
{
    public abstract class XtraxRule
    {
        protected abstract IInputData GetOutput(IInputData data);
        protected XtraxRule Next; // pointer to next action to perform
                                  // if null the current element is the last element of chain
        /// <summary>
        /// set new action at end of chain
        /// </summary>
        public void SetNext(XtraxRule next)
        {
            if (Next == null)
                Next = next;
            else
            {
                Next.SetNext(next); // put at the end of chain
            }
        }

        public IInputData GetOutputChained(IInputData data)
        {
            IInputData ret = GetOutput(data);
            if (Next != null)
            {
                var nextRump = new InputData(ret.GetText());
                ret = Next.GetOutputChained(nextRump);
            }
            return ret;
        }
    }

}