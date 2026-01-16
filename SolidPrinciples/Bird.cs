using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solid
{
    // liskov substitution principle is applied here as we have created a base class Bird and derived classes FlyingBird and Ostrich
    // now we can use FlyingBird wherever Bird is expected without breaking the functionality   
    // so it is useful when we have to add new types of birds in future we can do it without affecting the existing code
    public class Bird {
   
    }
    public class FlyingBird : Bird {
      public void Fly() {
        
      }
    }

    public class Ostrich : Bird {
      
    }

    public class pigeon : FlyingBird {
      
    }
}