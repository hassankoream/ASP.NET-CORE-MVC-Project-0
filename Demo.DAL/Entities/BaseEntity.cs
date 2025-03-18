using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Entities
{
  
        public class BaseEntity/*<Tkey>*/ //Use Tkey if you have Different types of primary keys
        {
            //These are just Audit Data for Database Adminstration
            public /*Tkey*/ int Id { get; set; } //PK
            public bool IsDeleted { get; set; } //in case you delete something and want it back later, we will keep it for a while in our Database

            public int CreatedBy { get; set; } //user Id (the one who  crreated the record)

            public DateTime CreatedOn { get; set; }
            public int LastModifiedBy { get; set; }//user Id
            public DateTime LastModifiedOn { get; set; }

        }
    
}
