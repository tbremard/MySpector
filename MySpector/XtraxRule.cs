namespace MySpector
{
    public abstract class XtraxRule
    {
        protected abstract string GetOutput(IRump rump);
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

        public string GetOutputChained(IRump rump)
        {
            string ret = GetOutput(rump);
            if (Next != null)
            {
                var nextRump = new Rump(ret);
                ret = Next.GetOutputChained(nextRump);
            }
            return ret;
        }
    }

}