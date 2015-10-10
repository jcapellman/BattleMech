using System;

namespace BattleMech.DataLayer.PCL.Models {
    public class BaseModel {
        public int ID { get; set; }
        
        public DateTimeOffset Modified { get; set; }

        public DateTimeOffset Created { get; set; }

        public bool Active { get; set; }

        public bool IsNew {
            get { return ID == 0 || ID == -1; }
        }
    }
}