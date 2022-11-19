namespace BigProjectOne.Libraries.Data.DB
{
    public class TenantContextException : System.Exception
    {
        public TenantContextException() { }
        public TenantContextException(string message) : base(message) { }
        public TenantContextException(string message, System.Exception inner) : base(message, inner) { }
        protected TenantContextException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}