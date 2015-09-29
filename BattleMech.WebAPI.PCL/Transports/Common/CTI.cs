namespace BattleMech.WebAPI.PCL.Transports.Common {
    public class CTI<T> {
        public T Value { get; set; }

        public string Exception { get; set; }

        public bool HasError => !string.IsNullOrEmpty(Exception);

        public CTI() {
            Value = default(T);
            Exception = null;
        } 

        public CTI(T objectValue, string exception = null) { Value = objectValue;
            Exception = exception;
        }        
    }
}