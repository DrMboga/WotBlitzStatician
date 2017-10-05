namespace WotBlitzStatician.Logic.Dto
{
    public class ValueDelta<TValue,TDelta>
    {
        public TValue PresentValue { get; }
        public TValue PastValue { get; }
        public TDelta Delta { get; }
	    public TDelta Interval { get; }

        public bool IsNegative { get; }

        public ValueDelta(TValue presentValue, TValue pastValue, TDelta delta, TDelta interval, bool isNegative = false)
        {
            PresentValue = presentValue;
            PastValue = pastValue;
            Delta = delta;
	        Interval = interval;
            IsNegative = isNegative;
        }
    }
}
